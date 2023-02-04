using System.Collections.Generic;

namespace AssemblyCSharp.AssetsData.Data.State
{
	public class Character
	{
		public Dictionary<string, int> Inventory { get; set; } = new();

		public bool IsFacingLeft { get; set; }

		public float X { get; set; }
	}
}
