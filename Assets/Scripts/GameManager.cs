using AssemblyCSharp.Assets.Data;
using AssemblyCSharp.AssetsData.Data;
using AssemblyCSharp.AssetsData.Data.Config;
using AssemblyCSharp.AssetsData.Data.State;
using AssemblyCSharp.AssetsData.Logic;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AssemblyCSharp.Assets.Scripts
{
	public sealed class GameManager : MonoBehaviour
	{
		[SerializeField]
		public CharacterMovement character;

		public GameState gameState;

		[SerializeField]
		public StartScreenController startScreen;

		[SerializeField]
		public StaminaBar staminaBar;

		private AppStateManager appStateManager = new();

		[SerializeField]
		private CameraMovement cameraMovement;

		[SerializeField]
		private GameConfig gameConfig;

		[SerializeField]
		private TileManager tileManager;

		public void Start()
		{
			gameState = GenerateStage(gameConfig);
			startScreen.Initialize(appStateManager);
			staminaBar.Initialize(gameConfig.playerCharacter, gameState.Character);
			appStateManager.ChangeState(AppState.Title);
			tileManager.Initialize(appStateManager, gameState.Stage, gameConfig.tileSets);
			cameraMovement.Initialize(gameState.Character, gameConfig.camera);
			character.Initialize(gameConfig.playerCharacter, gameState.Character, tileManager);
		}

		private static GameState GenerateStage(GameConfig gameConfig)
		{
			Stage stage = StageGenerator.Generate(gameConfig);
			Character player = new()
			{
				X = GetPlayerStartX(gameConfig, stage)
			};
			return new GameState()
			{
				Stage = stage,
				Character = player
			};
		}

		private static int GetPlayerStartX(GameConfig gameConfig, Stage stage)
		{
			HashSet<string> homeIds = gameConfig.structures
				.Where(s => s.type == StructureType.Home)
				.Select(k => k.id)
				.ToHashSet();
			for (int x = 0; x < stage.Tiles.Length; x++)
			{
				Tile tile = stage.Tiles[x];
				if (tile?.Structure?.StructureConfigId != null
					&& homeIds.Contains(tile.Structure.StructureConfigId))
				{
					return x;
				}
			}
			return 0;
		}
	}
}
