﻿using System;

namespace AssemblyCSharp.AssetsData.Data.State
{
	public sealed class Stage
	{
		public string stageConfigId;

		public Tile[] Tiles { get; set; } = Array.Empty<Tile>();

		public string TileSetId { get; set; } = string.Empty;

		public float lastMoundPlacementTime;
	}
}
