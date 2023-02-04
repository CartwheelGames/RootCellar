using System;
using System.Collections.Generic;
using UnityEngine;

namespace AssemblyCSharp.AssetsData.Data.Config
{
	[Serializable]
	public sealed class StructureConfig
	{
		public string id;

		public List<Level> levels = new();

		public string name = string.Empty;

		public int resourceCount;

		public ResourceType resourceType;

		public StructureType type;

		public sealed class Level
		{
			public int capacity;

			public LevelCost cost;

			public string eventName = string.Empty;

			public Texture2D image;
		}

		public class LevelCost
		{
			public int amount;

			public string resourceId;
		}
	}
}
