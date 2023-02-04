using System;
using System.Collections.Generic;

namespace AssemblyCSharp.AssetsData.Data.Config
{
	[Serializable]
	public sealed class TileSetConfig
	{
		public string id;

		public List<TileConfig> tiles = new();
	}
}
