using System;
using UnityEngine;

namespace AssemblyCSharp.AssetsData.Data.Config
{
	[Serializable]
	public sealed class TileConfig
	{
		public string id;

		public Texture2D image;

		public TileType type;

		public int weight;
	}
}
