using System.Collections.Generic;

namespace AssemblyCSharp.AssetsData.Data.State
{
	public class Character
	{
		public Dictionary<string, int> Inventory { get; set; } = new();

		public float Stamina = 50f; // out of 100

		public bool IsFacingLeft { get; set; }

		public float X { get; set; }
	}
}
