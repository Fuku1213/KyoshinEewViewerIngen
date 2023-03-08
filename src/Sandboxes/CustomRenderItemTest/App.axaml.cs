using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Platform;
using Avalonia.Rendering;
using Avalonia.Threading;
using CustomRenderItemTest.ViewModels;
using CustomRenderItemTest.Views;
using KyoshinEewViewer.Core;
using KyoshinEewViewer.Core.Models.Events;
using KyoshinEewViewer.CustomControl;
using ReactiveUI;
using Splat;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using static CustomRenderItemTest.NativeMethods;

namespace CustomRenderItemTest;

public class App : Application
{
	public static ThemeSelector? Selector { get; private set; }

	public override void Initialize() => AvaloniaXamlLoader.Load(this);

	public override void OnFrameworkInitializationCompleted()
	{
		if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
		{
			var splashWindow = new SplashWindow();
			splashWindow.Show();

			Selector = ThemeSelector.Create(".");
			Selector.EnableThemes(this);

			Dispatcher.UIThread.InvokeAsync(() =>
			{
				desktop.MainWindow = new MainWindow
				{
					DataContext = new MainWindowViewModel(),
				};
				Selector.WhenAnyValue(x => x.SelectedIntensityTheme).Where(x => x != null)
					.Subscribe(x => FixedObjectRenderer.UpdateIntensityPaintCache(desktop.MainWindow));
				Selector.WhenAnyValue(x => x.SelectedWindowTheme).Where(x => x != null).Subscribe(x =>
				{
					if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && desktop.MainWindow.PlatformImpl is not null)
					{
						Avalonia.Media.Color FindColorResource(string name)
							=> (Avalonia.Media.Color)(desktop.MainWindow.FindResource(name) ?? throw new Exception($"マップリソース {name} が見つかりませんでした"));
						bool FindBoolResource(string name)
							=> (bool)(desktop.MainWindow.FindResource(name) ?? throw new Exception($"リソース {name} が見つかりませんでした"));

						var isDarkTheme = FindBoolResource("IsDarkTheme");
						var USE_DARK_MODE = isDarkTheme ? 1 : 0;
						DwmSetWindowAttribute(
							desktop.MainWindow.PlatformImpl.Handle.Handle,
							DWMWINDOWATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE,
							ref USE_DARK_MODE,
							Marshal.SizeOf(USE_DARK_MODE));

						var color = FindColorResource("TitleBackgroundColor");
						var colord = color.R | color.G << 8 | color.B << 16;
						DwmSetWindowAttribute(
							desktop.MainWindow.PlatformImpl.Handle.Handle,
							DWMWINDOWATTRIBUTE.DWMWA_CAPTION_COLOR,
							ref colord,
							Marshal.SizeOf(colord));

						//var color2 = FindColorResource("SubForegroundColor");
						//var colord2 = color2.R | color2.G << 8 | color2.B << 16;
						//DwmSetWindowAttribute(
						//	desktop.MainWindow.PlatformImpl.Handle.Handle,
						//	DWMWINDOWATTRIBUTE.DWMWA_BORDER_COLOR,
						//	ref colord2,
						//	Marshal.SizeOf(colord2));
					}
				});
				desktop.MainWindow.Show();
				desktop.MainWindow.Activate();
				splashWindow.Close();
			});

			desktop.Exit += (s, e) => MessageBus.Current.SendMessage(new ApplicationClosing());
		}
		else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
		{
			Selector = ThemeSelector.Create(null);
			Selector.EnableThemes(this);

			singleViewPlatform.MainView = new MainView
			{
				DataContext = new MainWindowViewModel()
			};
		}
		base.OnFrameworkInitializationCompleted();
	}

	/// <summary>
	/// override RegisterServices register custom service
	/// </summary>
	public override void RegisterServices()
	{
		AvaloniaLocator.CurrentMutable.Bind<IFontManagerImpl>().ToConstant(new CustomFontManagerImpl());

		base.RegisterServices();
	}
}
