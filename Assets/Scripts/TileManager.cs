using AssemblyCSharp.Assets.Data;
using AssemblyCSharp.AssetsData.Data;
using AssemblyCSharp.AssetsData.Data.Config;
using AssemblyCSharp.AssetsData.Data.State;
using System;
using System.Linq;
using UnityEngine;

namespace AssemblyCSharp.Assets.Scripts
{
	public class TileManager : MonoBehaviour
	{
		public GameObject structurePrefab;

		public TileHandler tilePrefab;

		public int tileZ = 1;

		public int treeZ = 2;

		private GameConfig gameConfig;

		private Stage stage;

		private TileSetConfig tileSet;

		public TileHandler[] TileHandlers { get; private set; } = Array.Empty<TileHandler>();

		public void AssignRandomCropToTile(TileHandler tileHandler)
		{
			System.Random random = new();
			int totalWeight = gameConfig.crops.Sum(x => x.weight);
			int randomValue = random.Next(totalWeight);
			for (int c = 0; c < gameConfig.crops.Count; c++)
			{
				CropConfig cropConfig = gameConfig.crops[c];
				if (randomValue < cropConfig.weight)
				{
					tileHandler.data.CropConfigId = cropConfig.id;
					break;
				}
				randomValue -= cropConfig.weight;
			}
		}

		public TileConfig GetTileConfig(string tileConfigId) => tileSet.tiles.SingleOrDefault(t => t.id == tileConfigId);

		public void Initialize(AppStateManager appStateManager, Stage stage, GameConfig gameConfig)
		{
			this.stage = stage;
			this.gameConfig = gameConfig;
			appStateManager.AddEnterListener(AppState.TitleToGame, Generate);
			appStateManager.AddLeaveListener(AppState.GameToLose, Cleanup);
			appStateManager.AddLeaveListener(AppState.GameToWin, Cleanup);
		}

		public void SetTileType(TileHandler tileHandler, TileType type)
		{
			if (tileSet != null)
			{
				TileConfig[] tiles = tileSet.tiles.Where(t => t.type == type).ToArray();
				if (tiles.Length > 0)
				{
					int randomIndex = 0;
					if (tiles.Length > 1)
					{
						System.Random random = new();
						randomIndex = random.Next(tiles.Length);
					}
					string tileConfigId = tiles[randomIndex].id;
					if (type == TileType.Mound)
					{
						AssignRandomCropToTile(tileHandler);
					}
					tileHandler.data.TileConfigId = tileConfigId;
					tileHandler.mainRenderer.sprite = tileSet.tiles.SingleOrDefault(t => t.id == tileConfigId)?.sprite;
				}
			}
		}

		private void Cleanup()
		{
			for (int i = 0; i < TileHandlers.Length; i++)
			{
				Destroy(TileHandlers[i].gameObject);
			}
			TileHandlers = Array.Empty<TileHandler>();
			tileSet = null;
		}

		private void Generate()
		{
			TileHandlers = new TileHandler[stage.Tiles.Length];
			tileSet = gameConfig.tileSets.SingleOrDefault(t => t.id == stage.TileSetId);
			for (int x = 0; x < stage.Tiles.Length; x++)
			{
				TileHandler tileHandler = Instantiate(tilePrefab, new Vector3(x, -1, tileZ), Quaternion.identity, transform);
				string tileID = stage.Tiles[x].TileConfigId;
				tileHandler.mainRenderer.sprite = tileSet.tiles.SingleOrDefault(t => t.id == tileID)?.sprite;
				tileHandler.data = stage.Tiles[x];
				if (tileHandler.data.Structure != null)
				{
					GameObject structure = Instantiate(structurePrefab, new Vector3(x, -1, treeZ), Quaternion.identity, tileHandler.transform);
					SpriteRenderer spriteRenderer = structure.GetComponent<SpriteRenderer>();
					StructureConfig structureConfig = gameConfig.structures.SingleOrDefault(s => s.id == tileHandler.data.Structure.StructureConfigId);
					spriteRenderer.sprite = structureConfig?.sprite;
				}
				TileHandlers[x] = tileHandler;
			}
		}
	}
}
