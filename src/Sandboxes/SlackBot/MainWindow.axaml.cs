using Avalonia;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Threading;
using KyoshinEewViewer.Core;
using KyoshinEewViewer.Core.Models;
using KyoshinEewViewer.Core.Models.Events;
using KyoshinEewViewer.CustomControl;
using KyoshinEewViewer.Map;
using KyoshinEewViewer.Map.Data;
using KyoshinEewViewer.Map.Layers;
using KyoshinEewViewer.Series;
using KyoshinEewViewer.Series.Earthquake;
using KyoshinEewViewer.Series.Earthquake.Events;
using KyoshinEewViewer.Series.KyoshinMonitor;
using KyoshinEewViewer.Series.KyoshinMonitor.Events;
using KyoshinEewViewer.Services;
using KyoshinMonitorLib;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ILogger = Splat.ILogger;

namespace SlackBot
{
	public partial class MainWindow : Window
	{
		private ILogger Logger { get; }

		private LandLayer LandLayer { get; } = new();
		private LandBorderLayer LandBorderLayer { get; } = new();
		private GridLayer GridLayer { get; } = new();

		public KyoshinMonitorSeries KyoshinMonitorSeries { get; }
		public EarthquakeSeries EarthquakeSeries { get; }

		public MapLayer[]? BaseMapLayers => SelectedSeries?.BaseLayers;

		public MapLayer[]? OverlayMapLayers => SelectedSeries?.OverlayLayers;

		public SlackUploader Uploader { get; } = new();

		private void UpdateMapLayers()
		{
			var layers = new List<MapLayer>();
			if (LandLayer != null)
				layers.Add(LandLayer);
			if (BaseMapLayers != null)
				layers.AddRange(BaseMapLayers);
			if (LandBorderLayer != null)
				layers.Add(LandBorderLayer);
			if (OverlayMapLayers != null)
				layers.AddRange(OverlayMapLayers);
			if (Locator.Current.RequireService<KyoshinEewViewerConfiguration>().Map.ShowGrid && GridLayer != null)
				layers.Add(GridLayer);
			Map.Layers = layers.ToArray();
		}

		public MainWindow()
		{
			Logger = Locator.Current.RequireService<ILogManager>().GetLogger<MainWindow>();
			Logger.LogInfo("初期化中…");
			InitializeComponent();
			Config = Locator.Current.RequireService<KyoshinEewViewerConfiguration>();

			KyoshinMonitorSeries = Locator.Current.RequireService<KyoshinMonitorSeries>();
			EarthquakeSeries = Locator.Current.RequireService<EarthquakeSeries>();
		}

		private ManualResetEventSlim Mres { get; } = new(true);
		private KyoshinEewViewerConfiguration Config { get; }

		protected override void OnOpened(EventArgs e)
		{
			base.OnOpened(e);

			if (Design.IsDesignMode)
				return;

			ClientSize = new Size(1280, 720);

			Task.Run(async () =>
			{
				LandBorderLayer.Map = LandLayer.Map = await MapData.LoadDefaultMapAsync();
				await Dispatcher.UIThread.InvokeAsync(() => SelectedSeries = KyoshinMonitorSeries);
				await Task.Delay(500);
				MessageBus.Current.SendMessage(new MapNavigationRequested(SelectedSeries?.FocusBound));
				Logger.LogInfo("初期化完了");
			});

			MessageBus.Current.Listen<MapNavigationRequested>().Subscribe(x =>
			{
				if (!Config.Map.AutoFocus)
					return;
				if (x.Bound is { } rect)
				{
					if (x.MustBound is { } mustBound)
						Map.Navigate(rect, TimeSpan.Zero, mustBound);
					else
						Map.Navigate(rect, TimeSpan.Zero);
				}
				else
					NavigateToHome();
			});

			MessageBus.Current.Listen<KyoshinShakeDetected>().Subscribe(async x =>
			{
				// 震度1未満の揺れは処理しない
				if (x.Event.Level <= KyoshinEventLevel.Weak)
					return;

				if (!Mres.IsSet)
					await Task.Run(() => Mres.Wait());
				Mres.Reset();
				try
				{
					await Dispatcher.UIThread.InvokeAsync(() => SelectedSeries = KyoshinMonitorSeries);

					var topPoint = x.Event.Points.OrderByDescending(p => p.LatestIntensity).First();
					var mrkdwn = new StringBuilder($"*最大{topPoint.LatestIntensity.ToJmaIntensity().ToLongString()}* ({topPoint.LatestIntensity:0.0})");
					var prefGroups = x.Event.Points.OrderByDescending(p => p.LatestIntensity).GroupBy(p => p.Region);
					foreach (var group in prefGroups)
						mrkdwn.Append($"\n  {group.Key}: {group.First().LatestIntensity.ToJmaIntensity().ToLongString()}({group.First().LatestIntensity:0.0})");

					var msg = x.Event.Level switch
					{
						KyoshinEventLevel.Weaker => "微弱な",
						KyoshinEventLevel.Weak => "弱い",
						KyoshinEventLevel.Medium => "",
						KyoshinEventLevel.Strong => "強い",
						KyoshinEventLevel.Stronger => "非常に強い",
						_ => "",
					} + "揺れを検知しました。";

					await Uploader.Upload(
						x.Event.Id.ToString(),
						"#" + topPoint.LatestColor?.ToString()[3..] ?? "FFF",
						":warning: " + msg,
						"【地震情報】" + msg,
						mrkdwn: mrkdwn.ToString(),
						//headerKvp: headerKvp,
						//contentKvp: new()
						//{
						//	{ "でかいタイトル", "内容" },
						//},
						imageCuptureLogic: () => CaptureImage()
					);
				}
				catch (Exception ex)
				{
					Logger.LogError(ex, "揺れ検知情報Slack投稿時に例外が発生しました");
				}
				finally
				{
					Mres.Set();
				}
			});

			MessageBus.Current.Listen<EarthquakeInformationUpdated>().Subscribe(async x =>
			{
				if (!Mres.IsSet)
					await Task.Run(() => Mres.Wait());
				Mres.Reset();
				try
				{
					await Dispatcher.UIThread.InvokeAsync(() => SelectedSeries = EarthquakeSeries);

					var headerKvp = new Dictionary<string, string>();

					if (x.Earthquake.IsHypocenterAvailable)
					{
						headerKvp.Add("震央", x.Earthquake.Place ?? "不明");

						if (!x.Earthquake.IsNoDepthData)
						{
							if (x.Earthquake.IsVeryShallow)
								headerKvp.Add("震源の深さ", "ごく浅い");
							else
								headerKvp.Add("震源の深さ", x.Earthquake.Depth + "km");
						}

						headerKvp.Add("規模", x.Earthquake.MagnitudeAlternativeText ?? $"M{x.Earthquake.Magnitude:0.0}");
					}

					await Uploader.Upload(
						x.Earthquake.Id,
						$"#{FixedObjectRenderer.IntensityPaintCache[x.Earthquake.Intensity].b.Color.ToString()[3..]}",
						$":information_source: {x.Earthquake.Title} 最大{x.Earthquake.Intensity.ToLongString()}",
						$"【{x.Earthquake.Title}】{x.Earthquake.GetNotificationMessage()}",
						mrkdwn: x.Earthquake.HeadlineText,
						headerKvp: headerKvp,
						//contentKvp: new()
						//{
						//	{ "でかいタイトル", "内容" },
						//},
						footerMrkdwn: x.Earthquake.Comment,
						imageCuptureLogic: () => CaptureImage()
					);
				}
				catch (Exception ex)
				{
					Logger.LogError(ex, "地震情報Slack投稿時に例外が発生しました");
				}
				finally
				{
					Mres.Set();
				}
			});

			Locator.Current.RequireService<TelegramProvideService>().StartAsync().ConfigureAwait(false);

#if DEBUG
			Task.Run(async () =>
			{
				await Task.Delay(5000);
				await Uploader.Upload(
					null,
					"#FFF",
					"テスト",
					"テストメッセージ",
					imageCuptureLogic: () => CaptureImage()
				);
			});
#endif
		}

		private void NavigateToHome()
			=> Map.Navigate(new RectD(Config.Map.Location1.CastPoint(), Config.Map.Location2.CastPoint()), TimeSpan.Zero);

		protected override void OnClosed(EventArgs e)
		{
			KyoshinMonitorSeries?.Deactivated();
			KyoshinMonitorSeries?.Dispose();
			base.OnClosed(e);
		}

		private IDisposable? MapPaddingListener { get; set; }
		private IDisposable? BaseMapLayersListener { get; set; }
		private IDisposable? OverlayMapLayersListener { get; set; }
		private IDisposable? CustomColorMapListener { get; set; }
		private IDisposable? FocusPointListener { get; set; }

		private readonly object _switchSelectLocker = new();
		private SeriesBase? _selectedSeries;
		public SeriesBase? SelectedSeries
		{
			get => _selectedSeries;
			set {
				if (_selectedSeries == value)
					return;

				lock (_switchSelectLocker)
				{
					// �f�^�b�`
					MapPaddingListener?.Dispose();
					MapPaddingListener = null;

					BaseMapLayersListener?.Dispose();
					BaseMapLayersListener = null;

					OverlayMapLayersListener?.Dispose();
					OverlayMapLayersListener = null;

					CustomColorMapListener?.Dispose();
					CustomColorMapListener = null;

					FocusPointListener?.Dispose();
					FocusPointListener = null;

					if (_selectedSeries != null)
					{
						_selectedSeries.MapNavigationRequested -= OnMapNavigationRequested;
						_selectedSeries.Deactivated();
					}

					value?.Activating();
					_selectedSeries = value;

					// �A�^�b�`
					if (_selectedSeries != null)
					{
						MapPaddingListener = _selectedSeries.WhenAnyValue(x => x.MapPadding).Subscribe(x => Map.Padding = x);
						Map.Padding = _selectedSeries.MapPadding;

						BaseMapLayersListener = _selectedSeries.WhenAnyValue(x => x.BaseLayers).Subscribe(x => UpdateMapLayers());

						OverlayMapLayersListener = _selectedSeries.WhenAnyValue(x => x.OverlayLayers).Subscribe(x => UpdateMapLayers());

						CustomColorMapListener = _selectedSeries.WhenAnyValue(x => x.CustomColorMap).Subscribe(x => LandLayer.CustomColorMap = x);
						LandLayer.CustomColorMap = _selectedSeries.CustomColorMap;

						FocusPointListener = _selectedSeries.WhenAnyValue(x => x.FocusBound).Subscribe(x => MessageBus.Current.SendMessage(new MapNavigationRequested(x)));
						MessageBus.Current.SendMessage(new MapNavigationRequested(_selectedSeries.FocusBound));

						_selectedSeries.MapNavigationRequested += OnMapNavigationRequested;

						UpdateMapLayers();
					}
					SeriesContent.Content = _selectedSeries?.DisplayControl;
				}
			}
		}
		private void OnMapNavigationRequested(MapNavigationRequested? e) => MessageBus.Current.SendMessage(e);


		private byte[] CaptureImage()
		{
			if (!Dispatcher.UIThread.CheckAccess())
				return Dispatcher.UIThread.InvokeAsync(CaptureImage).Result;

			var stream = new MemoryStream();
			var pixelSize = new PixelSize((int)(ClientSize.Width * Config.WindowScale), (int)(ClientSize.Height * Config.WindowScale));
			var size = new Size(ClientSize.Width, ClientSize.Height);
			var dpiVector = new Vector(96 * Config.WindowScale, 96 * Config.WindowScale);
			using (var renderBitmap = new RenderTargetBitmap(pixelSize, dpiVector))
			{
				Measure(size);
				Arrange(new Rect(size));
				renderBitmap.Render(this);
				renderBitmap.Save(stream);
			}
			return stream.ToArray();
		}
	}
}
