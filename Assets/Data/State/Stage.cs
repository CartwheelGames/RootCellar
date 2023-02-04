using System;

namespace RootCellar.Data.State
{
	public sealed class Stage
	{
		public Tile[] Tiles { get; set; } = Array.Empty<Tile>();

		public string TileSetId { get; set; } = string.Empty;
	}
}
