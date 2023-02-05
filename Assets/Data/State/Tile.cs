namespace AssemblyCSharp.AssetsData.Data.State
{
	public class Tile
	{
		public float ActionProgress { get; set; }

		public int GrowthStep { get; set; }

		public string CropConfigId { get; set; }

		public Structure Structure { get; set; }

		public string TileConfigId { get; set; } = string.Empty;

		public float Water { get; set; }
	}
}
