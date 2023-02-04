using System.Collections.Generic;

namespace VerdantVibes.Data.Config
{
	public sealed class StageConfig
	{
		public int BaseWidth { get; set; }

		public Dictionary<int, string> Structures { get; set; } = new();

		public string TileSet { get; set; } = string.Empty;

		public int Width { get; set; }
	}
}
