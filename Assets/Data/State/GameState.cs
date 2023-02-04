namespace VerdantVibes.Data.State
{
	public sealed class GameState
	{
		public Character Player { get; set; } = new();

		public Stage Stage { get; set; }
	}
}
