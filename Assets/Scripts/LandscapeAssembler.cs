using AssemblyCSharp.Assets.Data;
using AssemblyCSharp.AssetsData.Data.Config;
using AssemblyCSharp.AssetsData.Data.State;
using System.Collections.Generic;
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

			appStateManager.AddEnterListener(AppState.Game, Generate);
			appStateManager.AddLeaveListener(AppState.Game, Cleanup);
		}

		private void Cleanup()
		{
			// Clear Sub objects here
		}

		private void Generate()
		{
			// Draw here
			tiles = new GameObject[stage.Tiles.Length];
			for (int x = 0; x < stage.Tiles.Length; x++)
			{
				tiles[x] = Instantiate(tilePrefab, new Vector3(x, 0), Quaternion.identity);
			}
		}
	}
}
