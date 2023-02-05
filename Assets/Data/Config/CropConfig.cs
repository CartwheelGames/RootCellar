using System;
using System.Collections.Generic;
using UnityEngine;

namespace AssemblyCSharp.AssetsData.Data.Config
{
	[Serializable]
	public sealed class CropConfig
	{
		public int days;

		public string id;

		public List<Texture2D> imagesByDay = new();

		/// <remarks> Out of 100 </remarks>
		public byte seedChance;

		public Texture2D seedImage;

		public int weight = 1;

		public int yield = 1;
	}
}
