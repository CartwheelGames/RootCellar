using System.Collections.Generic;

namespace AssemblyCSharp.AssetsData.Data.Config
{
	public sealed class TileSetConfig
	{
		public string Image { get; set; } = string.Empty;

		public Dictionary<string, TileConfig> Tiles { get; set; } = new();
	}
}
