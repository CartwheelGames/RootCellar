using System.Collections.Generic;

namespace VerdantVibes.Data.Config
{
	public sealed class TileSetConfig
	{
		public string Image { get; set; } = string.Empty;

		public Dictionary<string, TileConfig> Tiles { get; set; } = new();
	}
}
