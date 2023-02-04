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
		[field: SerializeField]
		public CharacterMovement Character { get; set; }

		public GameState gameState;

		private AppStateManager appStateManager = new();

		private GameConfig gameConfig;

		public void Start()
		{
			TextAsset gameConfigData = Resources.Load<TextAsset>("gameConfig");
			gameConfig = JsonUtility.FromJson<GameConfig>(gameConfigData.text);
			gameState = GenerateStage(gameConfig);
			appStateManager.ChangeState(AppState.Title);
		}

		private static GameState GenerateStage(GameConfig gameConfig)
		{
			Stage stage = StageGenerator.Generate(gameConfig);
			Character player = new()
			{
				BaseSpeed = gameConfig.PlayerCharacter.BaseSpeed,
				Image = gameConfig.PlayerCharacter.Image,
				Name = "Player",
				X = GetPlayerStartX(gameConfig, stage)
			};
			return new GameState()
			{
				Stage = stage,
				Player = player
			};
		}

		private static int GetPlayerStartX(GameConfig gameConfig, Stage stage)
		{
			HashSet<string> homeIds = gameConfig.Structures
				.Where(s => s.Value.Type == StructureType.Home)
				.Select(k => k.Key)
				.ToHashSet();
			for (int x = 0; x < stage.Tiles.Length; x++)
			{
				Tile tile = stage.Tiles[x];
				if (tile.Structure?.StructureConfigId != null
					&& homeIds.Contains(tile.Structure.StructureConfigId))
				{
					return x;
				}
			}
			return 0;
		}
	}
}
