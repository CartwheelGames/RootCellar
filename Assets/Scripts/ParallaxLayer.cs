using System;
using AssemblyCSharp.Assets.Data;
using AssemblyCSharp.AssetsData.Data.Config;
using AssemblyCSharp.AssetsData.Data.State;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace AssemblyCSharp.Assets.Scripts
{
	public class ParallaxLayer: MonoBehaviour
	{
		public GameObject parallaxSpritePrefab;

		private ParallaxConfig parallaxConfig;

		private Camera localCamera;

		private float zDepth;
		
		private float screenWidth;

		private bool initialized;

		private GameObject[] sprites;

		private TileConfig[] tileConfigArray;

		private void Start()
		{
			this.initialized = false;
		}

		public void Initialize(ParallaxConfig parallaxConfig, Camera localCamera, float zDepth)
		{			
			this.parallaxConfig = parallaxConfig;
			this.localCamera = localCamera;
			this.zDepth = zDepth;


			this.screenWidth = 2 * this.localCamera.orthographicSize * this.localCamera.aspect;
;			
			int numItems = parallaxConfig.numberOnscreen + 1;
			sprites = new GameObject[numItems];
			List<TileConfig> tileConfigs = parallaxConfig.tiles;
			tileConfigArray = new TileConfig[numItems];
			for (int i = 0; i < numItems; i++)
			{
				sprites[i] = Instantiate(
					parallaxSpritePrefab, 
					new Vector3(i * this.screenWidth / parallaxConfig.numberOnscreen, 
					this.parallaxConfig.yOffset, zDepth), 
					Quaternion.identity, transform);
				tileConfigArray[i] = tileConfigs[UnityEngine.Random.Range(0,tileConfigs.Count)];
				SpriteRenderer spriteRenderer = sprites[i].GetComponent<SpriteRenderer>();
				spriteRenderer.sprite = tileConfigArray[i].sprite;
				float width = spriteRenderer.bounds.size.x;
     			// float height = spriteRenderer.bounds.size.y;
				sprites[i].GetComponent<Transform>().localScale *= (this.screenWidth / this.parallaxConfig.numberOnscreen) / width;
			}
			Debug.Log(this.screenWidth);

			this.initialized = true;
		}

		private void Update()
		{
			float cameraPos = this.localCamera.GetComponent<Transform>().position.x;
			float cameraOffset = cameraPos * this.parallaxConfig.parallaxAmount;
			float segment = this.screenWidth / this.parallaxConfig.numberOnscreen;
			// Debug.Log(this.screenWidth);
			// if (this.parallaxConfig.parallaxAmount == 1) Debug.Log(cameraOffset % this.screenWidth);

			for (int i = 0; i < sprites.Length; i++)
			{
				Vector3 position = sprites[i].GetComponent<Transform>().position;
				float absoluteDivider = ((float)Math.Truncate((1 - this.parallaxConfig.parallaxAmount) * cameraPos / this.screenWidth) * this.screenWidth);

				position.x = cameraOffset + absoluteDivider + segment * i - segment / 2 * (this.parallaxConfig.numberOnscreen - 1);
				// position.x = cameraOffset + (cameraInverse % this.screenWidth); //* (this.screenWidth / parallaxConfig.numberOnscreen * this.parallaxConfig.parallaxAmount);
				sprites[i].GetComponent<Transform>().position = position;
			}
		}
	}
}
