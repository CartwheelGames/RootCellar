using System.Collections.Generic;

namespace AssemblyCSharp.AssetsData.Data.State
{
	public class Character
	{
		public Dictionary<string, int> Inventory { get; set; } = new();

		public string CurrentItemId { get; set; } = string.Empty;

		public float Stamina = 50f; // out of 100

		public int Money = 0;

		public bool IsFacingLeft { get; set; }

		/// <remarks> The x index of the current tile the characterMovement is at </remarks>
		public int X { get; set; }
	}
}
