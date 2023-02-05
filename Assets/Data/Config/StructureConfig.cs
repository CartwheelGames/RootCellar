using System;
using UnityEngine;

namespace AssemblyCSharp.AssetsData.Data.Config
{
	[Serializable]
	public sealed class StructureConfig
	{
		public string id;

		public string name = string.Empty;

		public Sprite sprite;

		public int resourceCount;

		public ResourceType resourceType;

		public StructureType type;
	}
}
