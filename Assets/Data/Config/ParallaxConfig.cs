using System;
using System.Collections.Generic;
using UnityEngine;

namespace AssemblyCSharp.AssetsData.Data.Config
{
	[Serializable]
	public sealed class ParallaxConfig
	{
		[Range(-1, 1)]
		public float parallaxAmount;

		[Range(1, 20)]
		public int numberOnscreen;

		public float yOffset;

		public List<TileConfig> tiles = new();
	}
}
