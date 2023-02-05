using AssemblyCSharp.AssetsData.Data;
using AssemblyCSharp.AssetsData.Data.Config;
using AssemblyCSharp.AssetsData.Data.State;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AssemblyCSharp.AssetsData.Logic
{
	public static class StageGenerator
	{
		public static Stage Generate(GameConfig config, int stageIndex = 0)
		{
			// Populate TileHandlers:
			StageConfig stageConfig = config.stages[stageIndex];
			int width = stageConfig.width;
			int baseWidth = stageConfig.baseWidth;
			string tileSetId = stageConfig.tileSet;
			Tile[] tiles = new Tile[width];
			int halfWidth = width / 2;
			int halfBaseWidth = baseWidth / 2;
			int baseStartX = halfWidth - halfBaseWidth;
			int baseEndX = halfWidth + halfBaseWidth;
			TileSetConfig tileSet = config.tileSets.SingleOrDefault(t => t.id == tileSetId);
			if (tileSet != null)
			{
				int totalWeight = tileSet.tiles.Sum(x => x.weight);
				List<TileConfig> pathTiles = tileSet.tiles
					.Where(x => x.type is TileType.Path)
					.ToList();
				List<TileConfig> tileKeys = tileSet.tiles;
				Random random = new();
				for (int x = 0; x < width; x++)
				{
					if (x >= baseStartX && x <= baseEndX)
					{
						int randomValue = random.Next(pathTiles.Count);
						TileConfig tile = pathTiles[randomValue];
						tiles[x] = new Tile()
						{
							TileConfigId = tile.id
						};
					}
					else
					{
						int randomValue = random.Next(totalWeight);
						for (int t = 0; t < tileSet.tiles.Count; t++)
						{
							TileConfig tile = tileSet.tiles[t];
							if (randomValue < tile.weight)
							{
								tiles[x] = new Tile()
								{
									TileConfigId = tile.id
								};
								break;
							}
							randomValue -= tile.weight;
						}
					}
				}
			}
			foreach (StageConfig.StageStructureInfo structureInfo in stageConfig.structures)
			{
				StructureConfig structureConfig = config.structures.SingleOrDefault(s => s.id == structureInfo.structureId);
				if(structureConfig != null)
				{
					tiles[halfWidth + structureInfo.x].Structure = new Structure()
					{
						ResourceCount = structureConfig.resourceCount,
						ResourceType = structureConfig.resourceType,
						StructureConfigId = structureInfo.structureId
					};
				}
			}
			return new Stage()
			{
				Tiles = tiles,
				TileSetId = tileSetId
			};
		}
	}
}
