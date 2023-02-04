using System.Collections.Generic;

namespace AssemblyCSharp.AssetsData.Data.Config
{
	public sealed class GameConfig
	{
		public Dictionary<string, CropConfig> Crops { get; set; }

		public CharacterConfig PlayerCharacter { get; set; }

		public Dictionary<ResourceType, string> ResourceIcons { get; set; }

		public StageConfig[] Stages { get; set; }

		public Dictionary<string, StructureConfig> Structures { get; set; }

		public Dictionary<string, TileSetConfig> TileSets { get; set; }
	}
}
