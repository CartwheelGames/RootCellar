using AssemblyCSharp.AssetsData.Data;
using AssemblyCSharp.AssetsData.Data.Config;
using AssemblyCSharp.AssetsData.Data.State;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace AssemblyCSharp.Assets.Scripts
{
	public sealed class MoundManager : MonoBehaviour
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

		private void SetRandomDirtTileAsMound()
		{
			System.Random random = new();
			TileSetConfig tileSet = gameConfig.tileSets.SingleOrDefault(t => t.id == Stage.TileSetId);
			int randomIndex = random.Next(tileManager.TileHandlers.Length);
			for (int i = randomIndex; i < tileManager.TileHandlers.Length; i++)
			{
				TileHandler tileHandler = tileManager.TileHandlers[i];
				string tileConfigId = tileHandler.data.TileConfigId;
				TileConfig tileConfig = tileSet.tiles.SingleOrDefault(t => t.id == tileConfigId);
				if (tileConfig != null && tileConfig.type == TileType.Dirt)
				{
					tileManager.SetTileType(tileHandler, TileType.Mound);
					return;
				}
			}
		}

		private void Update()
		{
			if (appStateManager != null && appStateManager.CurrentAppState == Data.AppState.Game)
			{
				if (stageConfig == null || Stage.stageConfigId != stageConfig.id)
				{
					stageConfig = gameConfig.stages.SingleOrDefault(s => s.id == Stage.stageConfigId);
				}
				if (gameState.GameTime - Stage.lastMoundPlacementTime > stageConfig.moundPlaceInterval)
				{
					Stage.lastMoundPlacementTime = gameState.GameTime;
					SetRandomDirtTileAsMound();
				}
			}
		}
	}
}
