using AssemblyCSharp.AssetsData.Data.Config;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AssemblyCSharp.Assets.Scripts
{
	public class ParallaxLayer : MonoBehaviour
	{
		public GameObject parallaxSpritePrefab;

		private Camera localCamera;

		private ParallaxConfig parallaxConfig;

		private float screenWidth;

		private GameObject[] sprites;

		private TileConfig[] tileConfigArray;

		public void Initialize(ParallaxConfig parallaxConfig, Camera localCamera, float zDepth)
		{
			this.parallaxConfig = parallaxConfig;
			this.localCamera = localCamera;
			screenWidth = 2 * localCamera.orthographicSize * localCamera.aspect;
			int numItems = parallaxConfig.numberOnscreen + 1;
			sprites = new GameObject[numItems];
			List<TileConfig> tileConfigs = parallaxConfig.tiles;
			tileConfigArray = new TileConfig[numItems];
			for (int i = 0; i < numItems; i++)
			{
				sprites[i] = Instantiate(
					parallaxSpritePrefab,
					new Vector3(i * screenWidth / parallaxConfig.numberOnscreen,
					parallaxConfig.yOffset, zDepth),
					Quaternion.identity, transform);
				System.Random random = new();
				int randomIndex = random.Next(tileConfigs.Count);
				tileConfigArray[i] = tileConfigs[randomIndex];
				SpriteRenderer spriteRenderer = sprites[i].GetComponent<SpriteRenderer>();
				spriteRenderer.sprite = tileConfigArray[i].sprite;
				float width = spriteRenderer.bounds.size.x;
				sprites[i].transform.localScale *= screenWidth / parallaxConfig.numberOnscreen / width;
			}
		}

		private void Update()
		{
			float cameraPos = localCamera.transform.position.x;
			float cameraOffset = cameraPos * parallaxConfig.parallaxAmount;
			float segment = screenWidth / parallaxConfig.numberOnscreen;
			for (int i = 0; i < sprites.Length; i++)
			{
				Vector3 position = sprites[i].transform.position;
				float absoluteDivider = (float)Math.Truncate((1 - parallaxConfig.parallaxAmount) * cameraPos / this.screenWidth) * this.screenWidth;
				position.x = cameraOffset
					+ absoluteDivider
					+ (segment * i)
					- (segment / 2 * (parallaxConfig.numberOnscreen - 1));
				sprites[i].transform.position = position;
			}
		}
	}
}
