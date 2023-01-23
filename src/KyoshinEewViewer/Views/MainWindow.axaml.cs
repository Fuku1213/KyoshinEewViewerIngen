using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using KyoshinEewViewer.Services;
using KyoshinEewViewer.ViewModels;
using ReactiveUI;
using Splat;
using System;
using System.Reactive.Linq;
using System.Threading;

namespace KyoshinEewViewer.Views;

public partial class MainWindow : Window
{
	private bool IsFullScreen { get; set; }
	private WindowState LatestWindowState { get; set; }

	/// <summary>
	/// クラッシュしたときにウィンドウ位置を記録しておくようのタイマー
	/// </summary>
	public Timer SaveTimer { get; }

	public MainWindow()
	{
		InitializeComponent();

		if (ConfigurationService.Current.WindowSize is Core.Models.KyoshinEewViewerConfiguration.Point2D size)
			ClientSize = new Size(size.X, size.Y);
		WindowState = ConfigurationService.Current.WindowState;
		if (ConfigurationService.Current.WindowLocation is Core.Models.KyoshinEewViewerConfiguration.Point2D position && position.X != -32000 && position.Y != -32000)
		{
			WindowStartupLocation = WindowStartupLocation.Manual;
			Position = new PixelPoint((int)position.X, (int)position.Y);
		}

		// フルスクリーンモード
		KeyDown += (s, e) =>
		{
			if (e.Key != Key.F11)
				return;

			if (IsFullScreen)
			{
				WindowState = WindowState.Normal;
				IsFullScreen = false;
				return;
			}
			WindowState = WindowState.FullScreen;
			IsFullScreen = true;
		};
		Closing += (s, e) =>
		{
			if (ConfigurationService.Current.Notification.HideWhenClosingWindow && (Locator.Current.GetService<NotificationService>()?.TrayIconAvailable ?? false))
			{
				Hide();
				if (!IsHideAnnounced)
				{
					Locator.Current.GetService<NotificationService>()?.Notify("タスクトレイに格納しました", "アプリケーションは実行中です");
					IsHideAnnounced = true;
				}
				e.Cancel = true;
				return;
			}
			SaveConfig();
		};
		this.WhenAnyValue(w => w.WindowState).Subscribe(s => 
		{
			if (s == WindowState.Minimized && ConfigurationService.Current.Notification.HideWhenMinimizeWindow && (Locator.Current.GetService<NotificationService>()?.TrayIconAvailable ?? false))
			{
				Hide();
				if (!IsHideAnnounced)
				{
					Locator.Current.GetService<NotificationService>()?.Notify("タスクトレイに格納しました", "アプリケーションは実行中です");
					IsHideAnnounced = true;
				}
				return;
			}
			LatestWindowState = s;
		});

		MessageBus.Current.Listen<Core.Models.Events.ShowSettingWindowRequested>().Subscribe(x => SubWindowsService.Default.ShowSettingWindow());
		MessageBus.Current.Listen<Core.Models.Events.ShowMainWindowRequested>().Subscribe(x =>
		{
			Topmost = true;
			Show();
			WindowState = LatestWindowState;
			Topmost = false;
		});

		SaveTimer = new Timer(_ => Dispatcher.UIThread.InvokeAsync(SaveConfig), null, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(10));
	}

	private bool IsHideAnnounced { get; set; }

	public new void Close()
	{
		SaveConfig();
		base.Close();
	}

	private void SaveConfig()
	{
		ConfigurationService.Current.WindowState = WindowState;
		if (WindowState != WindowState.Minimized)
		{
			ConfigurationService.Current.WindowLocation = new(Position.X, Position.Y);
			if (WindowState != WindowState.Maximized)
				ConfigurationService.Current.WindowSize = new(ClientSize.Width, ClientSize.Height);
		}
		if (DataContext is MainWindowViewModel vm && !StartupOptions.IsStandalone)
			ConfigurationService.Current.SelectedTabName = vm.SelectedSeries?.Name;
		ConfigurationService.Save();
	}
}
