using AssemblyCSharp.AssetsData.Data.Config;
using AssemblyCSharp.AssetsData.Data.State;
using System.Linq;
using UnityEngine;

namespace AssemblyCSharp.Assets.Scripts
{
	public sealed class InventoryUI : MonoBehaviour
	{
		private Character _character;

		private int _currentInventoryLength = 0;

		[SerializeField]
		private RectTransform _inventoryItemParent;

		[SerializeField]
		private SpriteRenderer _inventoryItemPrefab;

		private GameConfig gameConfig;

		public CropConfig GetCropConfigFromCropId(string cropId) =>
			gameConfig.crops.SingleOrDefault(c => c.id == cropId);

		// tODO: call
		public void Initialize(Character character, GameConfig gameConfig)
		{
			_character = character;
			this.gameConfig = gameConfig;
		}

		public void Update()
		{
			if (_character != null && _currentInventoryLength != _character.Inventory.Count)
			{
				ClearInventoryUI();
				CreateNewInventoryUI();

				_currentInventoryLength = _character.Inventory.Count;
			}
		}

		private void ClearInventoryUI()
		{
			foreach (Transform child in _inventoryItemParent.transform)
			{
				Destroy(child.gameObject);
			}
		}

		private void CreateNewInventoryUI()
		{
			foreach (string inventoryItemId in _character.Inventory.Keys)
			{
				CropConfig cropConfig = GetCropConfigFromCropId(inventoryItemId);

				// if for some reason, the crop stageConfigId doesn't correspond to a Crop Config,
				// don't display the inventory UI for that object
				if (cropConfig != null)
				{
					// Create a new inventory item
					SpriteRenderer newInventoryItem = Instantiate(_inventoryItemPrefab, _inventoryItemParent);

					// Customize the color
					newInventoryItem.color = cropConfig.color;
				}
			}
		}
	}
}
