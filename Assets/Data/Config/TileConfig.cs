using System;
using UnityEngine;

namespace AssemblyCSharp.AssetsData.Data.Config
{
	[Serializable]
	public sealed class TileConfig
	{
		public string id;

		public Sprite sprite;

		public TileType type;

		[Range(0, 100)]
		public int weight;
	}
}
