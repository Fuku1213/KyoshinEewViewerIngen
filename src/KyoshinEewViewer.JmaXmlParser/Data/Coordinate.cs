using U8Xml;

namespace KyoshinEewViewer.JmaXmlParser.Data;

public struct Coordinate
{
	private XmlNode Node { get; set; }

	public Coordinate(XmlNode node)
	{
		Node = node;
	}

	private string? _description = null;
	/// <summary>
	/// 文字列表現<br/>
	/// 例: 北緯３９．０度 東経１４０．９度 深さ １０ｋｍ、震源要素不明
	/// </summary>
	public string Description => _description ??= (Node.TryFindStringAttribute(Literals.AttrDescription(), out var n) ? n : throw new JmaXmlParseException("description 属性が存在しません"));

	private string? _datum = null;
	/// <summary>
	/// 座標系 そもそも座標が不明の場合は null<br/>
	/// 例: 日本測地系
	/// </summary>
	public string? Datum => _datum ??= (Node.TryFindStringAttribute(Literals.AttrDatum(), out var n) ? n : null);

	private string? _type = null;
	/// <summary>
	/// 世界測地系 の場合 震源位置（度分） が入る
	/// </summary>
	public string? Type => _type ??= (Node.TryFindStringAttribute(Literals.AttrType(), out var n) ? n : null);

	private string? _value = null;
	/// <summary>
	/// ISO6709 に準拠した座標表記<br/>
	/// 座標が不明な場合空になる<br/>
	/// 例: +39.0+140.9-10000/
	/// </summary>
	public string? Value => _value ??= Node.InnerText.ToString();
}
