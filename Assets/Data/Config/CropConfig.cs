using System;
using System.Collections.Generic;
using UnityEngine;

namespace AssemblyCSharp.AssetsData.Data.Config
{
	[Serializable]
	public sealed class CropConfig
	{
		public Color color;

		public float stepTime = 10f;

		public string id;

		public List<Sprite> stepSprites = new();

		/// <remarks> Out of 100 </remarks>
		public byte seedChance;

		public Sprite seedImage;

		public int weight = 1;

		public int yield = 1;
	}
}
