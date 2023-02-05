namespace AssemblyCSharp.AssetsData.Data.State
{
	public sealed class GameState
	{
		public CharacterData Character { get; set; } = new();

		public float GameTime { get; set; }

		public Stage Stage { get; set; }
	}
}
