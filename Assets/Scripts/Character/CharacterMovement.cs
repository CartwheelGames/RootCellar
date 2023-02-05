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

		private float Speed => gameConfig?.playerCharacter != null
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
					TileHandler tile = tileManager.Tiles[character.X];
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
				character.X = Mathf.Clamp(x, 0, tileManager.Tiles.Length + 1);
			}
		}

		/// <remarks> Ignore for MVP </remarks>
		private void ChopTree(TileHandler tile)
		{
			tile.data.ActionProgress += Time.deltaTime * CharacterConfig.chopTreeSpeed;
			if (tile.data.ActionProgress >= 1f)
			{
				tileManager.SetTileType(tile, TileType.Dirt);
				tile.data.ActionProgress = 0f;
			}
		}

		private void DigMound(TileHandler tile)
		{
			tile.data.ActionProgress += Time.deltaTime * CharacterConfig.digMoundSpeed;
			if (tile.data.ActionProgress >= 1f)
			{
				tileManager.SetTileType(tile, TileType.Dirt);
				System.Random random = new();
				int totalWeight = gameConfig.crops.Sum(x => x.weight);
				int randomValue = random.Next(totalWeight);
				for (int c = 0; c < gameConfig.crops.Count; c++)
				{
					CropConfig cropConfig = gameConfig.crops[c];
					if (randomValue < cropConfig.weight)
					{
						character.Inventory[cropConfig.id]++;
						break;
					}
					randomValue -= cropConfig.weight;
				}
				tile.data.ActionProgress = 0f;
			}
		}

		/// <remarks> Ignore for MVP </remarks>
		private void ForageBush(TileHandler tile)
		{
			tile.data.ActionProgress += Time.deltaTime * CharacterConfig.forageBushSpeed;
			if (tile.data.ActionProgress >= 1f)
			{
				tileManager.SetTileType(tile, TileType.Dirt);
				tile.data.ActionProgress = 0f;
			}
		}

		private void HarvestCrop(TileHandler tile)
		{
			tile.data.ActionProgress += Time.deltaTime * CharacterConfig.digMoundSpeed;
			if (tile.data.ActionProgress >= 1f)
			{
				tile.data.ActionProgress = 0f;
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
				}
			}
		}

		/// <remarks> Ignore for MVP </remarks>
		private void HitRock(TileHandler tile)
		{
			tile.data.ActionProgress += Time.deltaTime * CharacterConfig.hitRockSpeed;
			if (tile.data.ActionProgress >= 1f)
			{
				tileManager.SetTileType(tile, TileType.Dirt);
				tile.data.ActionProgress = 0f;
			}
		}

		private void PlantSeed(TileHandler tile)
		{
			tile.data.ActionProgress += Time.deltaTime * CharacterConfig.digMoundSpeed;
			if (tile.data.ActionProgress >= 1f)
			{
				tileManager.SetTileType(tile, TileType.Growing);
				tile.data.ActionProgress = 0f;
				if (!string.IsNullOrEmpty(character.CurrentItemId)
					&& character.Inventory.ContainsKey(character.CurrentItemId))
				{
					CropConfig cropConfig = gameConfig.crops.SingleOrDefault(c => c.id == character.CurrentItemId);
					tile.data.CropConfigId = cropConfig.id;
					if (cropConfig.images.Count > 0)
					{
						tile.topRenderer.sprite = cropConfig.images[0];
					}
				}
			}
		}

		/// <remarks> Ignore for MVP </remarks>
		private void PlowDirt(TileHandler tile)
		{
			tile.data.ActionProgress += Time.deltaTime * CharacterConfig.plowDirtSpeed;
			if (tile.data.ActionProgress >= 1f)
			{
				tileManager.SetTileType(tile, TileType.Plot);
				tile.data.ActionProgress = 0f;
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
			tile.data.ActionProgress += Time.deltaTime * CharacterConfig.waterPlotSpeed;
			if (tile.data.ActionProgress >= 1f)
			{
				tile.data.Water = 1f;
				tile.data.ActionProgress = 0f;
			}
		}
	}
}
