namespace VerdantVibes.Data.Config
{
	public sealed class TileConfig
	{
		public string Image { get; set; } = string.Empty;

		public TileType Type { get; set; }

		public int Weight { get; set; }
	}
}
