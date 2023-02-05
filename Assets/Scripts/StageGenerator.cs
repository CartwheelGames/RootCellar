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
		public static Stage Generate(GameConfig gameConfig, int stageIndex = 0)
		{
			// Populate TileHandlers:
			StageConfig stageConfig = gameConfig.stages[stageIndex];
			int width = stageConfig.width;
			int baseWidth = stageConfig.baseWidth;
			string tileSetId = stageConfig.tileSet;
			Tile[] tiles = new Tile[width];
			int halfWidth = width / 2;
			int halfBaseWidth = baseWidth / 2;
			int baseStartX = halfWidth - halfBaseWidth;
			int baseEndX = halfWidth + halfBaseWidth;
			TileSetConfig tileSet = gameConfig.tileSets.SingleOrDefault(t => t.id == tileSetId);
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
							TileConfig tileConfig = tileSet.tiles[t];
							if (randomValue < tileConfig.weight)
							{
								tiles[x] = new Tile()
								{
									TileConfigId = tileConfig.id,
									CropConfigId = tileConfig.type == TileType.Mound ? GetRandomCropId(gameConfig) : string.Empty
								};
								break;
							}
							randomValue -= tileConfig.weight;
						}
					}
				}
			}
			foreach (StageConfig.StageStructureInfo structureInfo in stageConfig.structures)
			{
				StructureConfig structureConfig = gameConfig.structures.SingleOrDefault(s => s.id == structureInfo.structureId);
				if (structureConfig != null)
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
				stageConfigId = stageConfig.id,
				Tiles = tiles,
				TileSetId = tileSetId
			};
		}

		public static string GetRandomCropId(GameConfig gameConfig)
		{
			Random random = new();
			int totalWeight = gameConfig.crops.Sum(x => x.weight);
			int randomValue = random.Next(totalWeight);
			for (int c = 0; c < gameConfig.crops.Count; c++)
			{
				CropConfig cropConfig = gameConfig.crops[c];
				if (randomValue < cropConfig.weight)
				{
					return cropConfig.id;
				}
				randomValue -= cropConfig.weight;
			}
			return string.Empty;
		}
	}
}
