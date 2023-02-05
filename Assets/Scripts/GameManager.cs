﻿using AssemblyCSharp.Assets.Data;
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
		private CharacterMovement characterMovement;

		private GameState gameState;

		[SerializeField]
		private InventoryUI inventoryUI;

		[SerializeField]
		private MoneyCounter moneyCounter;

		[SerializeField]
		private StaminaBar staminaBar;

		[SerializeField]
		private StartScreenController startScreen;

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
			staminaBar.Initialize(appStateManager, gameConfig.playerCharacter, gameState.Character);
			inventoryUI.Initialize(gameState.Character, gameConfig);
			moneyCounter.Initialize(gameConfig.playerCharacter, gameState.Character);

			appStateManager.ChangeState(AppState.Title);
			tileManager.Initialize(appStateManager, gameState.Stage, gameConfig);
			cameraMovement.Initialize(gameState.Character, gameConfig.camera);
			characterMovement.Initialize(gameConfig, gameState.Character, tileManager);
		}

		private static GameState GenerateStage(GameConfig gameConfig)
		{
			Stage stage = StageGenerator.Generate(gameConfig);
			Character player = new()
			{
				X = GetPlayerStartX(gameConfig, stage),
				// TODO: REMOVE - just for testing inventory
				// For now, set inventory to a random collection
				Inventory = new Dictionary<string, int>()
				{
					{ "tempRedBerry", 1 },
					{ "tempBlueBerry", 1 },
					{ "tempPinkBerry", 1 }
				}
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
