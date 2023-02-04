using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp.AssetsData.Data.Config;
using AssemblyCSharp.AssetsData.Data.State;
using UnityEngine;
using AssemblyCSharp.Assets.Data;

namespace AssemblyCSharp.Assets.Scripts
{
    public class LandscapeAssembler : MonoBehaviour
    {
        private Stage stage;
        private List<TileSetConfig> tileSets;
        private GameObject[] tiles;

        public GameObject tilePrefab;

        public void Initialize(AppStateManager appStateManager, Stage stage, List<TileSetConfig> tileSets)
        {
            this.stage = stage;
            this.tileSets = tileSets;

            appStateManager.AddEnterListener(AppState.Game, Generate);
            appStateManager.AddLeaveListener(AppState.Game, Cleanup);

        }

        private void Generate() {
            Debug.Log("whaaaaat");
            // Draw here
            tiles = new GameObject[stage.Tiles.Length];
            for (int x = 0; x < stage.Tiles.Length; x++) {
                Debug.Log(stage.Tiles.Length);
                tiles[x] = Instantiate<GameObject> (tilePrefab, new Vector3(x, 0, 0),  Quaternion.identity);
            }
        }


        private void Cleanup() {
            // Clear Sub objects here
        }
    }
}