using AssemblyCSharp.Assets.Data;
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
		public TileRenderer tilePrefab;

		private Stage stage;

		private List<TileSetConfig> tileSets;

		public TileRenderer[] Tiles { get; private set; } = Array.Empty<TileRenderer>();

		public void Initialize(AppStateManager appStateManager, Stage stage, List<TileSetConfig> tileSets)
		{
			this.stage = stage;
			this.tileSets = tileSets;

			appStateManager.AddEnterListener(AppState.TitleToGame, Generate);
			appStateManager.AddLeaveListener(AppState.Game, Cleanup);
		}

		private void Cleanup()
		{
			for (int i = 0; i < Tiles.Length; i++)
			{
				Destroy(Tiles[i].gameObject);
			}
			Tiles = Array.Empty<TileRenderer>();
		}

		private void Generate()
		{
			Tiles = new TileRenderer[stage.Tiles.Length];
			TileSetConfig tileSet = tileSets.SingleOrDefault(t => t.id == stage.TileSetId);
			for (int x = 0; x < stage.Tiles.Length; x++)
			{
				Tiles[x] = Instantiate(tilePrefab, new Vector3(x, -1, -1), Quaternion.identity, transform);
				string tileID = stage.Tiles[x].TileConfigId;
				Tiles[x].mainRenderer.sprite = tileSet.tiles.SingleOrDefault(t => t.id == tileID)?.sprite;
			}
		}
	}
}
