﻿using KyoshinEewViewer.Core.Models;
using KyoshinEewViewer.Core.Models.Events;
using KyoshinEewViewer.Services;
using KyoshinMonitorLib;
using Microsoft.Extensions.Logging;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace KyoshinEewViewer.ViewModels
{
	public class SettingWindowViewModel : ViewModelBase
	{
		public KyoshinEewViewerConfiguration Config { get; }

		public SettingWindowViewModel()
		{
			Config = ConfigurationService.Default;
			//Config = new KyoshinEewViewerConfiguration();
			//Config.Timer.Offset = 2500;
			//Config.Theme.WindowThemeName = "Light";
			//Config.Theme.IntensityThemeName = "Standard";

			AvailableDmdataBillingInfo = true;
			DmdataTotalBillingAmount = 5000;
			DmdataUnpaidAmount = 20000;
			DmdataBillingStatusUpdatedTime = DateTime.Now;
			DmdataBillingStatusTargetMonth = DateTime.Now;

			Config.Timer.WhenAnyValue(c => c.TimeshiftSeconds).Subscribe(x => UpdateTimeshiftString());

			//WindowThemes = App.Selector?.WindowThemes?.Select(t => t.Name).ToArray();
		}

		[Reactive]
		public string Title { get; set; } = "設定 - KyoshinEewViewer for ingen";

		//private ICommand applyDmdataApiKeyCommand;
		//public ICommand ApplyDmdataApiKeyCommand => applyDmdataApiKeyCommand ??= new DelegateCommand(() => Config.Dmdata.ApiKey = DmdataApiKey);

		public List<JmaIntensity> Ints { get; } = new List<JmaIntensity> {
				JmaIntensity.Unknown,
				JmaIntensity.Int0,
				JmaIntensity.Int1,
				JmaIntensity.Int2,
				JmaIntensity.Int3,
				JmaIntensity.Int4,
				JmaIntensity.Int5Lower,
				JmaIntensity.Int5Upper,
				JmaIntensity.Int6Lower,
				JmaIntensity.Int6Upper,
				JmaIntensity.Int7,
				JmaIntensity.Error,
			};

		//private ICommand _registMapPositionCommand;
		//public ICommand RegistMapPositionCommand => _registMapPositionCommand ??= new DelegateCommand(() => Aggregator.GetEvent<RegistMapPositionRequested>().Publish());

		//private ICommand _resetMapPositionCommand;
		//public ICommand ResetMapPositionCommand => _resetMapPositionCommand ??= new DelegateCommand(() =>
		//{
		//	Config.Map.Location1 = new Location(24.058240f, 123.046875f);
		//	Config.Map.Location2 = new Location(45.706479f, 146.293945f);
		//});

		//[Reactive]
		//public string[]? WindowThemes { get; set; }


		[Reactive]
		public string TimeshiftSecondsString { get; set; } = "リアルタイム";
		private void UpdateTimeshiftString()
		{
			if (Config.Timer.TimeshiftSeconds == 0)
			{
				TimeshiftSecondsString = "リアルタイム";
				return;
			}

			var sb = new StringBuilder();
			var time = TimeSpan.FromSeconds(-Config.Timer.TimeshiftSeconds);
			if (time.TotalHours >= 1)
				sb.Append((int)time.TotalHours + "時間");
			if (time.Minutes > 0)
				sb.Append(time.Minutes + "分");
			if (time.Seconds > 0)
				sb.Append(time.Seconds + "秒");
			sb.Append('前');

			TimeshiftSecondsString = sb.ToString();
		}

		[Reactive]
		public string DmdataStatusString { get; set; } = "未実装です";
		[Reactive]
		public bool AvailableDmdataBillingInfo { get; set; } = false;
		[Reactive]
		public int DmdataTotalBillingAmount { get; set; } = 0;
		[Reactive]
		public int DmdataUnpaidAmount { get; set; } = 0;

		[Reactive]
		public DateTime DmdataBillingStatusUpdatedTime { get; set; }
		[Reactive]
		public DateTime DmdataBillingStatusTargetMonth { get; set; }

		[Reactive]
		public bool IsVisibleLinuxOptions { get; set; } = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

		public void RegistMapPosition()
		{
			MessageBus.Current.SendMessage(new RegistMapPositionRequested());
		}
		public void ResetMapPosition()
		{
			Config.Map.Location1 = new Location(24.058240f, 123.046875f);
			Config.Map.Location2 = new Location(45.706479f, 146.293945f);
		}
		public void OpenUrl(string url)
			=> UrlOpener.OpenUrl(url);
	}
}
