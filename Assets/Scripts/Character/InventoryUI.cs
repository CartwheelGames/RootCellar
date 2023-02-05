using AssemblyCSharp.AssetsData.Data.Config;
using AssemblyCSharp.AssetsData.Data.State;
using System.Linq;
using UnityEngine;

namespace AssemblyCSharp.Assets.Scripts.Character
{
	public sealed class InventoryUI : MonoBehaviour
	{
		private CharacterData character;

		[SerializeField]
		private SpriteRenderer inventoryDisplay;

		private GameConfig gameConfig;

		public CropConfig GetCropConfigFromCropId(string cropId) =>
			gameConfig.crops.SingleOrDefault(c => c.id == cropId);

		public void Initialize(CharacterData character, GameConfig gameConfig)
		{
			this.character = character;
			this.gameConfig = gameConfig;
		}

		private CropConfig currentCropConfig;

		public void Update()
		{
			if (character != null && !string.IsNullOrEmpty(character.CurrentItemId))
			{
				if (currentCropConfig == null || currentCropConfig.id != character.CurrentItemId || inventoryDisplay.sprite == null)
				{
					currentCropConfig = gameConfig.crops.SingleOrDefault(c => c.id == character.CurrentItemId);
					if (currentCropConfig != null)
					{
						inventoryDisplay.sprite = currentCropConfig.seedImage;
						inventoryDisplay.enabled = true;
						inventoryDisplay.color = currentCropConfig.color;
					}
					else
					{
						inventoryDisplay.sprite = null;
						inventoryDisplay.enabled = false;
					}
				}
			}
			else if (inventoryDisplay.enabled)
			{
				inventoryDisplay.sprite = null;
				inventoryDisplay.enabled = false;
			}
		}
	}
}
