using KyoshinMonitorLib;
using System;
using System.Collections.Generic;

namespace KyoshinEewViewer.Series.KyoshinMonitor.Models;

public interface IEew
{
	/// <summary>
	/// 地震ID
	/// </summary>
	string Id { get; }

	/// <summary>
	/// 表示する情報元
	/// </summary>
	string SourceDisplay { get; }

	/// <summary>
	/// キャンセル報か 受信範囲外の可能性もある
	/// </summary>
	bool IsCancelled { get; }
	/// <summary>
	/// 確実にキャンセル報であることがわかっているか
	/// </summary>
	bool IsTrueCancelled { get; }
	/// <summary>
	/// 受信時刻
	/// </summary>
	DateTime ReceiveTime { get; }
	/// <summary>
	/// 最大震度
	/// </summary>
	JmaIntensity Intensity { get; }
	/// <summary>
	/// 最大震度に 以上 がつくかどうか
	/// </summary>
	bool IsIntensityOver { get; }
	/// <summary>
	/// 地震の発生時間
	/// </summary>
	DateTime OccurrenceTime { get; }
	/// <summary>
	/// 震央地名
	/// </summary>
	string? Place { get; }
	/// <summary>
	/// 震央座標
	/// </summary>
	Location? Location { get; }
	/// <summary>
	/// マグニチュード
	/// </summary>
	float? Magnitude { get; }
	/// <summary>
	/// 震源の深さ
	/// </summary>
	int Depth { get; }
	/// <summary>
	/// 報数
	/// </summary>
	int Count { get; }
	/// <summary>
	/// 警報状態か
	/// </summary>
	bool IsWarning { get; }
	/// <summary>
	/// 最終報か
	/// </summary>
	bool IsFinal { get; }

	/// <summary>
	/// 精度情報が存在するかどうか
	/// </summary>
	public bool IsAccuracyFound { get; }
	#region 精度値 上書きすることがあるためseterも実装させる
	/// <summary>
	/// 震央の確からしさフラグ
	/// </summary>
	int? LocationAccuracy { get; set; }
	/// <summary>
	/// 深さの確からしさフラグ
	/// </summary>
	int? DepthAccuracy { get; set; }
	/// <summary>
	/// マグニチュードの確からしさフラグ
	/// </summary>
	int? MagnitudeAccuracy { get; set; }
	#endregion

	/// <summary>
	/// 仮定震源要素か？
	/// </summary>
	public bool IsTemporaryEpicenter { get; }

	/// <summary>
	/// 震源要素が最終報レベルの精度か
	/// </summary>
	bool? IsLocked { get; }

	/// <summary>
	/// 予想震度一覧
	/// </summary>
	Dictionary<int, JmaIntensity>? ForecastIntensityMap { get; set; }

	/// <summary>
	/// 警報地域コード一覧
	/// </summary>
	int[]? WarningAreaCodes { get; set; }

	/// <summary>
	/// 警報地域名一覧
	/// </summary>
	string[]? WarningAreaNames { get; set; }

	/// <summary>
	/// EEWを処理する優先度
	/// </summary>
	int Priority { get; }

	/// <summary>
	/// このソフトで更新した時刻
	/// </summary>
	DateTime UpdatedTime { get; set; }

	public string ToDetailString()
		=> $"source:{SourceDisplay} id:{Id} count:{Count} isFinal:{IsFinal} isCanceled:{IsCancelled} isWarning:{IsWarning} receiveTime:{ReceiveTime:yyyy/MM/dd HH:mm:ss.fff} updatedTime:{UpdatedTime:yyyy/MM/dd HH:mm:ss.fff}";
}
