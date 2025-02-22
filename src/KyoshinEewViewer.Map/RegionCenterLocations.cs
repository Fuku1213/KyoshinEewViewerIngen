﻿using KyoshinMonitorLib;
using MessagePack;
using System.Collections.Generic;

namespace KyoshinEewViewer.Map;

public class RegionCenterLocations
{
	private static RegionCenterLocations? _default;
	public static RegionCenterLocations Default => _default ??= new RegionCenterLocations();

	private RegionCenterLocations()
	{
		CenterLocations = MessagePackSerializer.Deserialize<Dictionary<LandLayerType, Dictionary<int, FloatVector>>>(Properties.Resources.CenterLocations, MessagePackSerializerOptions.Standard.WithCompression(MessagePackCompression.Lz4BlockArray));
	}

	public Location? GetLocation(LandLayerType layerType, int code)
	{
		if (!CenterLocations.TryGetValue(layerType, out var dic))
			return null;
		if (!dic.TryGetValue(code, out var location))
			return null;
		return new Location(location.X, location.Y);
	}

	private Dictionary<LandLayerType, Dictionary<int, FloatVector>> CenterLocations { get; }
}

[MessagePackObject]
public struct FloatVector
{
	public FloatVector(float x, float y)
	{
		X = x;
		Y = y;
	}

	[Key(0)]
	public float X { get; set; }
	[Key(1)]
	public float Y { get; set; }
}
