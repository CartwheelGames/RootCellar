using System;
using System.Collections.Generic;

namespace AssemblyCSharp.AssetsData.Data.Config
{
	[Serializable]
	public sealed class StageConfig
	{
		public int baseWidth;

		public string id;

		public float moundPlaceInterval;

		public List<StageStructureInfo> structures = new();

		public string tileSet = string.Empty;

		public int width;

		[Serializable]
		public class StageStructureInfo
		{
			public string structureId;

			public int x;
		}
	}
}
