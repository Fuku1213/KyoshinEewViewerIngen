using Android.App;
using Android.Content;
using Android.OS;
using Avalonia;
using Avalonia.Android;
using Avalonia.ReactiveUI;
using CustomRenderItemTest;
using Application = Android.App.Application;

namespace AvaloniaWebTest.Android;

[Activity(Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true)]
public class SplashActivity : AvaloniaSplashActivity<App>
{
	protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
	{
		return base.CustomizeAppBuilder(builder)
			.UseSkia()
			.UseReactiveUI();
	}

	protected override void OnCreate(Bundle? savedInstanceState)
	{
		base.OnCreate(savedInstanceState);
	}

	protected override void OnResume()
	{
		base.OnResume();

		StartActivity(new Intent(Application.Context, typeof(MainActivity)));
	}
}
