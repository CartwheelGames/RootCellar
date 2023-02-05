using AssemblyCSharp.Assets.Data;
using AssemblyCSharp.AssetsData.Data;
using AssemblyCSharp.AssetsData.Data.Config;
using AssemblyCSharp.AssetsData.Data.State;
using System.Linq;
using UnityEngine;

namespace AssemblyCSharp.Assets.Scripts.Character
{
	public sealed class CharacterHandler : MonoBehaviour
	{
		[SerializeField]
		private Animator animator;

		private AppStateManager appStateManager;

		private CharacterStateManager characterStateManager = new();

		private GameConfig gameConfig;

		private GameState gameState;

		private TileManager tileManager;

		private CharacterConfig CharacterConfig => gameConfig != null ? gameConfig.playerCharacter : null;

		private CharacterData CharacterData => gameState.Character;

		private float Speed => CharacterConfig != null ? CharacterConfig.baseSpeed : 1f;

		public void Initialize(
			AppStateManager appStateManager,
			GameConfig gameConfig,
			GameState gameState,
			TileManager tileManager)
		{
			this.appStateManager = appStateManager;
			this.gameState = gameState;
			this.gameConfig = gameConfig;
			this.tileManager = tileManager;
			transform.position = new Vector3(
				gameState.Character.X,
				transform.position.y,
				transform.position.z);
			void animate(string animationId) => animator.Play(animationId);
			characterStateManager.AddEnterListener(CharacterState.Idle, () => animate("Idle"));
			characterStateManager.AddEnterListener(CharacterState.Plow, () => animate("Idle"));
			characterStateManager.AddEnterListener(CharacterState.Chop, () => animate("Idle"));
			characterStateManager.AddEnterListener(CharacterState.Dig, () => animate("Idle"));
			characterStateManager.AddEnterListener(CharacterState.Forage, () => animate("Idle"));
			characterStateManager.AddEnterListener(CharacterState.Harvest, () => animate("Idle"));
			characterStateManager.AddEnterListener(CharacterState.Hit, () => animate("Idle"));
			characterStateManager.AddEnterListener(CharacterState.Plant, () => animate("Idle"));
			characterStateManager.AddEnterListener(CharacterState.Walk, () =>
			{
				animate(gameState.Character.IsFacingLeft ? "WalkLeft" : "WalkRight");
			});
		}

		public void Update()
		{
			if (gameState != null && tileManager != null && appStateManager.CurrentState == AppState.Game)
			{
				float horizontalInput = Input.GetAxisRaw("Horizontal");
				float verticalInput = Input.GetAxisRaw("Vertical");
				if (verticalInput < -float.Epsilon)
				{
					AssignCharacterX();
					TileHandler tile = tileManager.TileHandlers[gameState.Character.TileX];
					string tileConfigId = tile.data.TileConfigId;
					TileConfig tileConfig = tileManager.GetTileConfig(tileConfigId);
					if (tileConfig != null)
					{
						ProcessTileAction(tile, tileConfig.type);
					}
				}
				else if (horizontalInput < -float.Epsilon)
				{
					transform.Translate(Vector2.left * Speed * Time.deltaTime);
					if (transform.position.x < 0)
					{
						transform.position = new Vector3(0, transform.position.y, transform.position.z);
					}
					AssignCharacterX();
					CharacterData.IsFacingLeft = true;
					EnterState(CharacterState.Walk);
				}
				else if (horizontalInput > float.Epsilon)
				{
					transform.Translate(Vector2.right * Speed * Time.deltaTime);
					if (transform.position.x > gameState.Stage.Tiles.Length - 1)
					{
						transform.position = new Vector3(gameState.Stage.Tiles.Length - 1, transform.position.y, transform.position.z);
					}
					AssignCharacterX();
					CharacterData.IsFacingLeft = false;
					EnterState(CharacterState.Walk);
				}
				else
				{
					EnterIdleState();
				}
			}
		}

		private void AssignCharacterX()
		{
			if (CharacterData != null)
			{
				int x = Mathf.RoundToInt(transform.position.x);
				CharacterData.TileX = Mathf.Clamp(x, 0, tileManager.TileHandlers.Length + 1);
				CharacterData.X = transform.position.x;
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
				EnterIdleState();
				Debug.Log("Tree chopped into dirt");
			}
			else
			{
				EnterState(CharacterState.Chop);
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
				CharacterData.AddItem(tileHandler.data.CropConfigId);
				tileHandler.data.ActionProgress = 0f;
				EnterIdleState();
				Debug.Log("Mound dug into dirt");
			}
			else
			{
				EnterState(CharacterState.Dig);
			}
		}

		private void EnterIdleState() => EnterState(CharacterState.Idle);

		private void EnterState(CharacterState characterState) =>
			characterStateManager.ChangeState(characterState);

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
				EnterIdleState();
				Debug.Log("Bush foraged into Dirt");
			}
			else
			{
				EnterState(CharacterState.Forage);
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
					for (int i = 0; i < cropConfig.yield; i++)
					{
						if (CharacterData.Stamina < 100f)
						{
							CharacterData.Stamina += gameConfig.playerCharacter.staminaGrowth;
							if (CharacterData.Stamina >= 100f)
							{
								CharacterData.Stamina = 100f;
								CharacterData.Money++;
							}
						}
						else
						{
							CharacterData.Money++;
						}
					}
					tile.data.CropConfigId = string.Empty;
					tileManager.SetTileType(tile, TileType.Plot);
					tile.TopSprite = null;
					System.Random random = new();
					if (random.Next(100) > cropConfig.seedChance)
					{
						CharacterData.AddItem(cropConfig.id);
					}
					Debug.Log("Harvested crop, tileHandler replaced with Plot");
				}
				else
				{
					Debug.LogWarning($"No crop was found with the ID: {tile.data.CropConfigId}");
				}
				tile.data.ActionProgress = 0f;
				tile.data.GrowthStep = 0;
				EnterIdleState();
			}
			else
			{
				EnterState(CharacterState.Harvest);
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
				EnterIdleState();
				Debug.Log("Hit rock into dirt");
			}
			else
			{
				EnterState(CharacterState.Hit);
			}
		}

		private void PlantSeed(TileHandler tile)
		{
			if (!string.IsNullOrEmpty(CharacterData.CurrentItemId))
			{
				if (tile.data.ActionProgress == 0)
				{
					Debug.Log("Planting seed");
				}
				tile.data.ActionProgress += Time.deltaTime * CharacterConfig.digMoundSpeed;
				if (tile.data.ActionProgress >= 1f && !string.IsNullOrEmpty(CharacterData.CurrentItemId))
				{
					tileManager.SetTileType(tile, TileType.Growing);
					int count = CharacterData.GetCountOfItem(CharacterData.CurrentItemId);
					if (count > 0)
					{
						CropConfig cropConfig = gameConfig.crops.SingleOrDefault(c => c.id == CharacterData.CurrentItemId);
						CharacterData.RemoveItem(CharacterData.CurrentItemId);
						if (cropConfig != null)
						{
							tile.data.CropConfigId = cropConfig.id;
							if (cropConfig.stepSprites.Count > 0)
							{
								tile.TopSprite = cropConfig.stepSprites[0];
							}
							if (cropConfig.rootImage != null)
							{
								tile.MainSprite = cropConfig.rootImage;
							}
							Debug.Log("Seed planted");
						}
					}
					tile.data.ActionProgress = 0f;
					EnterIdleState();
				}
				else
				{
					EnterState(CharacterState.Plant);
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
				EnterIdleState();
				Debug.Log("Dirt Plowed into a Plot");
			}
			else
			{
				EnterState(CharacterState.Plow);
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
				case TileType.Growing:
				default:
					EnterIdleState();
					break;
			}
		}
	}
}
