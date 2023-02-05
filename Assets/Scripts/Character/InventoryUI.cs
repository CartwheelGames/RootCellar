using AssemblyCSharp.AssetsData.Data.Config;
using AssemblyCSharp.AssetsData.Data.State;
using UnityEngine;

namespace AssemblyCSharp.Assets.Scripts
{
	public sealed class InventoryUI : MonoBehaviour
	{
		private Character _character;
		private CharacterConfig _characterConfig;
		private GameManager _gameManager;
		private int _currentInventoryLength = 0;
		[SerializeField] private RectTransform _inventoryItemParent;
		[SerializeField] private SpriteRenderer _inventoryItemPrefab;

		// tODO: call
		public void Initialize(CharacterConfig characterConfig, Character character, GameManager gameManager)
		{
			_characterConfig = characterConfig;
			_character = character;
			_gameManager = gameManager;
		}

		public void Update()
		{
			// Script hasn't been initialized yet
			if (_character == null)
			{
				return;
			}
			
			// Inventory hasn't changed
			if (_currentInventoryLength == _character.Inventory.Count)
			{
				return;
			}
			
			ClearInventoryUI();
			CreateNewInventoryUI();

			_currentInventoryLength = _character.Inventory.Count;
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
				var cropConfig = _gameManager.GetCropConfigFromCropId(inventoryItemId);

				// if for some reason, the crop id doesn't correspond to a Crop Config,
				// don't display the inventory UI for that object
				if (cropConfig == null)
				{
					continue;
				}

				// Create a new inventory item
				SpriteRenderer newInventoryItem = Instantiate(_inventoryItemPrefab, _inventoryItemParent);
				
				// Customize the color
				newInventoryItem.color = cropConfig.color;
			}
		}
	}
}
