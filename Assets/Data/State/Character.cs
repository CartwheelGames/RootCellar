using System.Collections.Generic;

namespace VerdantVibes.Data.State
{
	public class Character
	{
		public float BaseSpeed { get; set; }

		public string Image { get; set; } = string.Empty;

		public Dictionary<string, int> Inventory { get; set; } = new();

		public bool IsFacingLeft { get; set; }

		public string Name { get; set; } = string.Empty;

		public float X { get; set; }
	}
}
