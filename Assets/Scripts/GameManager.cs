using AssemblyCSharp.Assets.Data;
using AssemblyCSharp.Assets.Scripts.Character;
using AssemblyCSharp.Assets.Scripts.UI;
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
		private AppStateManager appStateManager = new();

		[SerializeField]
		private AppStateTransitioner appStateTransitioner;

		[SerializeField]
		private CameraMovement cameraMovement;

		[SerializeField]
		private CharacterHandler characterActions;

		[SerializeField]
		private GameConfig gameConfig;

		private GameState gameState;

		[SerializeField]
		private InventoryUI inventoryUI;

		[SerializeField]
		private MoneyCounter moneyCounter;

		[SerializeField]
		private MoundManager moundManager;

		[SerializeField]
		private OverlayManager overlayManager;

		[SerializeField]
		private ParallaxManager parallaxManager;

		[SerializeField]
		private StaminaBar staminaBar;

		[SerializeField]
		private TileManager tileManager;

		[SerializeField]
		private TimeManager timeManager;

		public void Start()
		{
			appStateTransitioner.Initialize(appStateManager);
			gameState = GenerateStage(gameConfig);
			overlayManager.Initialize(appStateManager);
			staminaBar.Initialize(appStateManager, gameConfig.playerCharacter, gameState.Character);
			inventoryUI.Initialize(gameState.Character, gameConfig);
			moneyCounter.Initialize(gameConfig.playerCharacter, gameState.Character);

			appStateManager.ChangeState(AppState.Title);
			tileManager.Initialize(appStateManager, gameState.Stage, gameConfig);
			cameraMovement.Initialize(gameState.Character, gameConfig.camera);
			characterActions.Initialize(appStateManager, gameConfig, gameState, tileManager);
			timeManager.Initialize(appStateManager, gameState);
			moundManager.Initialize(appStateManager, gameConfig, gameState, tileManager);
			parallaxManager.Initialize(appStateManager, cameraMovement.GetComponent<Camera>(), gameConfig.parallaxLayers);
		}

		private static GameState GenerateStage(GameConfig gameConfig)
		{
			Stage stage = StageGenerator.Generate(gameConfig);
			int startX = GetPlayerStartX(gameConfig, stage);
			return new GameState()
			{
				Stage = stage,
				Character = new CharacterData()
				{
					TileX = startX,
					X = startX
				}
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
