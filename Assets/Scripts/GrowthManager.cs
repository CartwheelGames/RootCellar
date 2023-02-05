using AssemblyCSharp.AssetsData.Data;
using AssemblyCSharp.AssetsData.Data.Config;
using AssemblyCSharp.AssetsData.Data.State;
using System.Linq;
using UnityEngine;

namespace AssemblyCSharp.Assets.Scripts
{
	public class GrowthManager : MonoBehaviour
	{
		private AppStateManager appStateManager;

		private GameConfig gameConfig;

		private GameState gameState;

		private StageConfig stageConfig;

		private TileManager tileManager;

		private Stage Stage => gameState.Stage;

		public void Initialize(
			AppStateManager appStateManager,
			GameConfig gameConfig,
			GameState gameState,
			TileManager tileManager)
		{
			this.appStateManager = appStateManager;
			this.gameConfig = gameConfig;
			this.gameState = gameState;
			this.tileManager = tileManager;
		}

		private void Update()
		{
			if (appStateManager != null && appStateManager.CurrentState == Data.AppState.Game)
			{
				foreach (TileHandler tile in tileManager.TileHandlers)
				{
					if (!string.IsNullOrEmpty(tile.data.CropConfigId))
					{
						TileConfig tileConfig = tileManager.GetTileConfig(tile.data.TileConfigId);
						if (tileConfig.type == TileType.Growing)
						{
							CropConfig cropConfig = gameConfig.crops.SingleOrDefault(c => c.id == tile.data.CropConfigId);
							tile.data.ActionProgress += Time.deltaTime * cropConfig.growSpeed;
							if (tile.data.ActionProgress >= 1f)
							{
								tile.data.GrowthStep++;
								tile.TopSprite = cropConfig.stepSprites[tile.data.GrowthStep];
								if (tile.data.GrowthStep >= cropConfig.stepSprites.Count - 1)
								{
									tileManager.SetTileType(tile, TileType.Crop);
									tile.MainSprite = cropConfig.rootImage;
									tile.data.GrowthStep = 0;
								}
								tile.data.ActionProgress = 0;
							}
						}
					}
				}
			}
		}
	}
}
