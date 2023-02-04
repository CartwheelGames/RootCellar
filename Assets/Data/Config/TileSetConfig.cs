using System;
using System.Collections.Generic;

namespace AssemblyCSharp.AssetsData.Data.Config
{
	[Serializable]
	public sealed class TileSetConfig
	{
		public string Id { get; set; }

		public string Image { get; set; } = string.Empty;

		public List<TileConfig> Tiles { get; set; } = new();
	}
}
