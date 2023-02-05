using AssemblyCSharp.Assets.Data;
using AssemblyCSharp.AssetsData.Data;
using AssemblyCSharp.AssetsData.Data.Config;
using AssemblyCSharp.AssetsData.Data.State;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AssemblyCSharp.Assets.Scripts
{
	public class TileManager : MonoBehaviour
	{
		public TileHandler tilePrefab;

		private Stage stage;

		private TileSetConfig tileSet;

		private List<TileSetConfig> tileSets;

		public TileHandler[] Tiles { get; private set; } = Array.Empty<TileHandler>();

		public TileConfig GetTileConfig(string tileConfigId) => tileSet.tiles.SingleOrDefault(t => t.id == tileConfigId);

		public void Initialize(AppStateManager appStateManager, Stage stage, List<TileSetConfig> tileSets)
		{
			this.stage = stage;
			this.tileSets = tileSets;

			appStateManager.AddEnterListener(AppState.TitleToGame, Generate);
			appStateManager.AddLeaveListener(AppState.Game, Cleanup);
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
					tileHandler.data.TileConfigId = tileConfigId;
					tileHandler.mainRenderer.sprite = tileSet.tiles.SingleOrDefault(t => t.id == tileConfigId)?.sprite;
				}
			}
		}

		private void Cleanup()
		{
			for (int i = 0; i < Tiles.Length; i++)
			{
				Destroy(Tiles[i].gameObject);
			}
			Tiles = Array.Empty<TileHandler>();
			tileSet = null;
		}

		private void Generate()
		{
			Tiles = new TileHandler[stage.Tiles.Length];
			tileSet = tileSets.SingleOrDefault(t => t.id == stage.TileSetId);
			for (int x = 0; x < stage.Tiles.Length; x++)
			{
				Tiles[x] = Instantiate(tilePrefab, new Vector3(x, -1, -1), Quaternion.identity, transform);
				string tileID = stage.Tiles[x].TileConfigId;
				Tiles[x].mainRenderer.sprite = tileSet.tiles.SingleOrDefault(t => t.id == tileID)?.sprite;
				Tiles[x].data = stage.Tiles[x];
			}
		}
	}
}
