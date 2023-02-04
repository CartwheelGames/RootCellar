using System.Collections.Generic;

namespace RootCellar.Data.Config
{
	public sealed class StageConfig
	{
		public int BaseWidth { get; set; }

		public Dictionary<int, string> Structures { get; set; } = new();

		public string TileSet { get; set; } = string.Empty;

		public int Width { get; set; }
	}
}
