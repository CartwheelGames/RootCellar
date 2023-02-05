namespace AssemblyCSharp.AssetsData.Data.State
{
	public class Tile
	{
		public Structure Structure { get; set; }

		public string TileConfigId { get; set; } = string.Empty;

		public float ActionProgress { get; set; }

		public float Water { get; set; }
	}
}
