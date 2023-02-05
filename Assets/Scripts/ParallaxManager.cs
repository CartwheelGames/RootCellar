using System;
using AssemblyCSharp.Assets.Data;
using AssemblyCSharp.AssetsData.Data.Config;
using AssemblyCSharp.AssetsData.Data.State;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace AssemblyCSharp.Assets.Scripts
{
	public class ParallaxManager: MonoBehaviour
	{
		public GameObject parallaxLayerPrefab;

		private Camera localCamera;

		private GameObject[] layers; 

		private ParallaxConfig[] parallaxConfigs;

		public void Initialize(AppStateManager appStateManager, Camera localCamera, ParallaxConfig[] parallaxConfigs)
		{
			this.parallaxConfigs = parallaxConfigs;
			this.localCamera = localCamera;

			appStateManager.AddEnterListener(AppState.TitleToGame, Generate);
			appStateManager.AddLeaveListener(AppState.Game, Cleanup);
		}

		private void Cleanup()
		{
			// Clear Sub objects here
		}

		private void Generate()
		{
			// foreach (ParallaxConfig config in parallaxConfigs)
			// sprites = new GameObject[][];
			layers = new GameObject[parallaxConfigs.Length];
			for (int z = 0; z < parallaxConfigs.Length; z++)
			{
				float zDepth = parallaxConfigs[z].parallaxAmount >= 0 ? (z + 1) * 10 : (z + 1) * -10;
				layers[z] = Instantiate(parallaxLayerPrefab, new Vector3(0, 0, zDepth), Quaternion.identity, transform);

				layers[z].GetComponent<ParallaxLayer>().Initialize(parallaxConfigs[z], this.localCamera, zDepth);
				// int numItems = parallaxConfigs[z].numberOnscreen + 1;
				// sprites[z] = new GameObject[numItems];
				// for (int i = 0; i < numItems; i++)
				// {
				// 	List<TileConfig> tileConfigs = parallaxConfigs[z].tiles;
				// 	sprites[z][i].GetComponent<SpriteRenderer>().sprite = tileConfigs[UnityEngine.Random.Range(0,tileConfigs.Count)].sprite;
				// }
			}
		}

		private void Update()
		{
			// Clear Sub objects here
		}
	}
}
