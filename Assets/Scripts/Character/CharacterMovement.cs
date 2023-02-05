using AssemblyCSharp.AssetsData.Data;
using AssemblyCSharp.AssetsData.Data.Config;
using AssemblyCSharp.AssetsData.Data.State;
using UnityEngine;

namespace AssemblyCSharp.Assets.Scripts
{
	public sealed class CharacterMovement : MonoBehaviour
	{
		private Character character;

		private CharacterConfig characterConfig;

		private TileManager tileManager;

		private float Speed => characterConfig != null ? characterConfig.baseSpeed : 1f;

		public void Initialize(
			CharacterConfig characterConfig,
			Character character,
			TileManager tileManager)
		{
			this.character = character;
			this.characterConfig = characterConfig;
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

		private void ChopTree(TileHandler tile)
		{
			tile.data.ActionProgress += Time.deltaTime * characterConfig.chopTreeSpeed;
			if (tile.data.ActionProgress >= 1f)
			{
				tileManager.SetTileType(tile, TileType.Dirt);
				tile.data.ActionProgress = 0f;
			}
		}

		private void DigMound(TileHandler tile)
		{
			tile.data.ActionProgress += Time.deltaTime * characterConfig.digMoundSpeed;
			if (tile.data.ActionProgress >= 1f)
			{
				tileManager.SetTileType(tile, TileType.Dirt);
				tile.data.ActionProgress = 0f;
			}
		}

		private void ForageBush(TileHandler tile)
		{
			tile.data.ActionProgress += Time.deltaTime * characterConfig.forageBushSpeed;
			if (tile.data.ActionProgress >= 1f)
			{
				tileManager.SetTileType(tile, TileType.Dirt);
				tile.data.ActionProgress = 0f;
			}
		}

		private void HitRock(TileHandler tile)
		{
			tile.data.ActionProgress += Time.deltaTime * characterConfig.hitRockSpeed;
			if (tile.data.ActionProgress >= 1f)
			{
				tileManager.SetTileType(tile, TileType.Dirt);
				tile.data.ActionProgress = 0f;
			}
		}

		private void PlowDirt(TileHandler tile)
		{
			tile.data.ActionProgress += Time.deltaTime * characterConfig.plowDirtSpeed;
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
				case TileType.Growing:
				case TileType.Plot:
					WaterPlot(tile);
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
				default:
				case TileType.None:
					break;
			}
		}

		private void WaterPlot(TileHandler tile)
		{
			tile.data.ActionProgress += Time.deltaTime * characterConfig.waterPlotSpeed;
			if (tile.data.ActionProgress >= 1f)
			{
				tile.data.Water = 1f;
				tile.data.ActionProgress = 0f;
			}
		}
	}
}
