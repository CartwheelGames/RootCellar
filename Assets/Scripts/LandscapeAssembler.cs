using AssemblyCSharp.Assets.Data;
using AssemblyCSharp.AssetsData.Data.Config;
using AssemblyCSharp.AssetsData.Data.State;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AssemblyCSharp.Assets.Scripts
{
	public class LandscapeAssembler : MonoBehaviour
	{
		public GameObject tilePrefab;

		private Stage stage;

		private GameObject[] tiles;

		private List<TileSetConfig> tileSets;

		public void Initialize(AppStateManager appStateManager, Stage stage, List<TileSetConfig> tileSets)
		{
			this.stage = stage;
			this.tileSets = tileSets;

			appStateManager.AddEnterListener(AppState.TitleToGame, Generate);
			appStateManager.AddLeaveListener(AppState.Game, Cleanup);
		}

		private void Cleanup()
		{
			// Clear Sub objects here
		}

		private void Generate()
		{
			tiles = new GameObject[stage.Tiles.Length];
            TileSetConfig tileSet = tileSets.SingleOrDefault(t=>t.id == stage.TileSetId);
			for (int x = 0; x < stage.Tiles.Length; x++)
            {
				tiles[x] = Instantiate(tilePrefab, new Vector3(x, -1, 0), Quaternion.identity, transform);
                SpriteRenderer spriteRenderer = tiles[x].GetComponent<SpriteRenderer>();
                string tileID = stage.Tiles[x].TileConfigId;
                spriteRenderer.sprite = tileSet.tiles.SingleOrDefault(t=>t.id == tileID)?.sprite;                
			}
		}
	}
}
