using AssemblyCSharp.AssetsData.Data;
using AssemblyCSharp.AssetsData.Data.Config;
using AssemblyCSharp.AssetsData.Data.State;
using System.Linq;
using UnityEngine;

namespace AssemblyCSharp.Assets.Scripts
{
	public sealed class CharacterMovement : MonoBehaviour
	{
		private Character character;

		private GameConfig gameConfig;

		private TileManager tileManager;

		private CharacterConfig CharacterConfig => gameConfig?.playerCharacter;

		private float Speed => gameConfig != null && gameConfig.playerCharacter != null
			? gameConfig.playerCharacter.baseSpeed
			: 1f;

		public void Initialize(
			GameConfig gameConfig,
			Character character,
			TileManager tileManager)
		{
			this.character = character;
			this.gameConfig = gameConfig;
			this.tileManager = tileManager;
			transform.position = new Vector3(character.X, transform.position.y, transform.position.z);
		}

		public void Update()
		{
			if (character != null && tileManager != null)
			{
				float horizontalInput = Input.GetAxis("Horizontal");
				float verticalInput = Input.GetAxis("Vertical");
				if (horizontalInput < -float.Epsilon)
				{
					AssignCharacterX();
					transform.Translate(Vector2.left * Speed);
					character.IsFacingLeft = true;
				}
				else if (horizontalInput > float.Epsilon)
				{
					AssignCharacterX();
					transform.Translate(Vector2.right * Speed);
					character.IsFacingLeft = false;
				}
				else if (verticalInput < -float.Epsilon)
				{
					AssignCharacterX();
					TileHandler tile = tileManager.TileHandlers[character.X];
					string tileConfigId = tile.data.TileConfigId;
					TileConfig tileConfig = tileManager.GetTileConfig(tileConfigId);
					if (tileConfig != null)
					{
						ProcessTileAction(tile, tileConfig.type);
					}
				}
			}
		}

		private void AssignCharacterX()
		{
			if (character != null)
			{
				int x = Mathf.RoundToInt(transform.position.x);
				character.X = Mathf.Clamp(x, 0, tileManager.TileHandlers.Length + 1);
			}
		}

		/// <remarks> Ignore for MVP </remarks>
		private void ChopTree(TileHandler tile)
		{
			if (tile.data.ActionProgress == 0)
			{
				Debug.Log("Chopping Tree");
			}
			tile.data.ActionProgress += Time.deltaTime * CharacterConfig.chopTreeSpeed;
			if (tile.data.ActionProgress >= 1f)
			{
				tileManager.SetTileType(tile, TileType.Dirt);
				tile.data.ActionProgress = 0f;
				Debug.Log("Tree chopped into dirt");
			}
		}

		private void DigMound(TileHandler tileHandler)
		{
			if (tileHandler.data.ActionProgress == 0)
			{
				Debug.Log("Digging mound");
			}
			tileHandler.data.ActionProgress += Time.deltaTime * CharacterConfig.digMoundSpeed;
			if (tileHandler.data.ActionProgress >= 1f)
			{
				tileManager.SetTileType(tileHandler, TileType.Dirt);
				character.AddItem(tileHandler.data.CropConfigId);
				tileHandler.data.ActionProgress = 0f;
				Debug.Log("Mound dug into dirt");
			}
		}

		/// <remarks> Ignore for MVP </remarks>
		private void ForageBush(TileHandler tile)
		{
			if (tile.data.ActionProgress == 0)
			{
				Debug.Log("Foraging bush");
			}
			tile.data.ActionProgress += Time.deltaTime * CharacterConfig.forageBushSpeed;
			if (tile.data.ActionProgress >= 1f)
			{
				tileManager.SetTileType(tile, TileType.Dirt);
				tile.data.ActionProgress = 0f;
				Debug.Log("Bush foraged into Dirt");
			}
		}

		private void HarvestCrop(TileHandler tile)
		{
			if (tile.data.ActionProgress == 0)
			{
				Debug.Log("Harvesting crop");
			}
			tile.data.ActionProgress += Time.deltaTime * CharacterConfig.digMoundSpeed;
			if (tile.data.ActionProgress >= 1f)
			{
				CropConfig cropConfig = gameConfig.crops.SingleOrDefault(c => c.id == tile.data.CropConfigId);
				if (cropConfig != null)
				{
					character.Inventory[cropConfig.id] += cropConfig.yield;
					tile.data.CropConfigId = string.Empty;
					tileManager.SetTileType(tile, TileType.Plot);
					tile.topRenderer.sprite = null;
					tile.frontRenderer.sprite = null;
					System.Random random = new();
					if (random.Next(100) > cropConfig.seedChance)
					{
						character.Inventory[cropConfig.id]++;
					}
					Debug.Log("Harvested crop, tileHandler replaced with Plot");
				}
				else
				{
					Debug.LogWarning($"No crop was found with the ID: {tile.data.CropConfigId}");
				}
				tile.data.ActionProgress = 0f;
			}
		}

		/// <remarks> Ignore for MVP </remarks>
		private void HitRock(TileHandler tile)
		{
			if (tile.data.ActionProgress == 0)
			{
				Debug.Log("Rock hit");
			}
			tile.data.ActionProgress += Time.deltaTime * CharacterConfig.hitRockSpeed;
			if (tile.data.ActionProgress >= 1f)
			{
				tileManager.SetTileType(tile, TileType.Dirt);
				tile.data.ActionProgress = 0f;
				Debug.Log("Hit rock into dirt");
			}
		}

		private void PlantSeed(TileHandler tile)
		{
			if (!string.IsNullOrEmpty(character.CurrentItemId))
			{
				if (tile.data.ActionProgress == 0)
				{
					Debug.Log("Planging seed");
				}
				tile.data.ActionProgress += Time.deltaTime * CharacterConfig.digMoundSpeed;
				if (tile.data.ActionProgress >= 1f)
				{
					tileManager.SetTileType(tile, TileType.Growing);
					int count = character.GetCountOfItem(character.CurrentItemId);
					if (count > 0)
					{
						character.RemoveItem(character.CurrentItemId);
						CropConfig cropConfig = gameConfig.crops.SingleOrDefault(c => c.id == character.CurrentItemId);
						tile.data.CropConfigId = cropConfig.id;
						if (cropConfig.stepSprites.Count > 0)
						{
							tile.topRenderer.sprite = cropConfig.stepSprites[0];
						}
						Debug.Log("Seed planted");
					}
					tile.data.ActionProgress = 0f;
				}
			}
			else
			{
				Debug.Log("Not carrying any seeds to plant");
			}
		}

		/// <remarks> Ignore for MVP </remarks>
		private void PlowDirt(TileHandler tile)
		{
			if (tile.data.ActionProgress == 0)
			{
				Debug.Log("Plowing Dirt");
			}
			tile.data.ActionProgress += Time.deltaTime * CharacterConfig.plowDirtSpeed;
			if (tile.data.ActionProgress >= 1f)
			{
				tileManager.SetTileType(tile, TileType.Plot);
				tile.data.ActionProgress = 0f;
				Debug.Log("Dirt Plowed into a Plot");
			}
		}

		private void ProcessTileAction(TileHandler tile, TileType tileType)
		{
			switch (tileType)
			{
				case TileType.Dirt:
					PlowDirt(tile);
					break;

				case TileType.Crop:
					HarvestCrop(tile);
					break;

				case TileType.Growing:
					WaterPlot(tile);
					break;

				case TileType.Plot:
					PlantSeed(tile);
					break;

				case TileType.Bush:
					ForageBush(tile);
					break;

				case TileType.Rock:
					HitRock(tile);
					break;

				case TileType.Tree:
					ChopTree(tile);
					break;

				case TileType.Mound:
					DigMound(tile);
					break;

				case TileType.Path:
				case TileType.None:
				default:
					break;
			}
		}

		/// <remarks> Ignore for MVP </remarks>
		private void WaterPlot(TileHandler tile)
		{
			if (tile.data.ActionProgress == 0)
			{
				Debug.Log("Watering plot");
			}
			tile.data.ActionProgress += Time.deltaTime * CharacterConfig.waterPlotSpeed;
			if (tile.data.ActionProgress >= 1f)
			{
				tile.data.Water = 1f;
				tile.data.ActionProgress = 0f;
				Debug.Log("Plot watered");
			}
		}
	}
}
