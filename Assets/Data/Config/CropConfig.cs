using System;
using System.Collections.Generic;
using UnityEngine;

namespace AssemblyCSharp.AssetsData.Data.Config
{
	[Serializable]
	public sealed class CropConfig
	{
		public Color color;

		public string id;

		public Sprite rootImage;

		/// <remarks> Out of 100 </remarks>
		public byte seedChance;

		public Sprite seedImage;

		public List<Sprite> stepSprites = new();

		public float growSpeed = 0.1f;

		public int weight = 1;

		public int yield = 1;
	}
}
