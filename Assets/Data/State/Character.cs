using System.Collections.Generic;
using System.Linq;

namespace AssemblyCSharp.AssetsData.Data.State
{
	public class Character
	{
		public int Money = 0;

		public float Stamina = 50f;

		public string CurrentItemId { get; set; } = string.Empty;

		public Dictionary<string, int> Inventory { get; set; } = new();

		// out of 100
		public bool IsFacingLeft { get; set; }

		/// <remarks> The x index of the current tile the characterMovement is at </remarks>
		public int X { get; set; }

		public int GetCountOfItem(string itemId) => Inventory.ContainsKey(itemId) ? Inventory[itemId] : 0;

		public void AddItem(string itemId)
		{
			if (Inventory.ContainsKey(itemId))
			{
				Inventory[itemId]++;
			}
			else
			{
				Inventory.Add(itemId, 1);
			}
			if (string.IsNullOrEmpty(CurrentItemId))
			{
				CurrentItemId = itemId;
			}
		}

		public bool RemoveItem(string itemId)
		{
			int count = GetCountOfItem(itemId);
			if (count == 1)
			{
				Inventory.Remove(itemId);
				if (Inventory.Count > 0)
				{
					CurrentItemId = Inventory.Keys.FirstOrDefault() ?? string.Empty;
				}
				else
				{
					CurrentItemId = string.Empty;
				}
				return true;
			}
			else if (count > 1)
			{
				Inventory[itemId]--;
				return true;
			}
			return false;
		}
	}
}
