using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Media;
using Avalonia.Threading;
using KyoshinMonitorLib;
using System;
using System.Globalization;

namespace KyoshinEewViewer.Series.KyoshinMonitor.Converters;

public class IntensityToColorConverter : IValueConverter
{
	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		if (!Dispatcher.UIThread.CheckAccess())
			return Dispatcher.UIThread.InvokeAsync(() => Convert(value, targetType, parameter, culture)).Result;

		var attr = (parameter as string) ?? "Foreground";
		if (value is not JmaIntensity intensity)
			return new SolidColorBrush((Color)(App.MainWindow?.FindResource($"Unknown{attr}") ?? throw new NullReferenceException("震度色リソースを取得できません")));
		return new SolidColorBrush((Color)(App.MainWindow?.FindResource($"{intensity}{attr}") ?? throw new NullReferenceException("震度色リソースを取得できません")));
	}

	public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
		=> throw new NotImplementedException();
}
