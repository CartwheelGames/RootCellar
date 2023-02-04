using System.Collections.Generic;
using UnityEngine;

namespace AssemblyCSharp.AssetsData.Data.Config
{
	public sealed class GameConfig : ScriptableObject
	{
		public List<CropConfig> crops;

		public CharacterConfig playerCharacter;

		public StageConfig[] stages;

		public List<StructureConfig> structures;

		public List<TileSetConfig> tileSets;
	}
}
