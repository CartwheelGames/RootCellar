using RootCellar.Data;
using RootCellar.Data.Config;
using RootCellar.Data.State;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RootCellar.Logic
{
	public static class StageGenerator
	{
		public static Stage Generate(GameConfig config, int stageIndex = 0)
		{
			// Populate tiles:
			StageConfig stageConfig = config.Stages[stageIndex];
			int width = stageConfig.Width;
			int baseWidth = stageConfig.BaseWidth;
			string tileSetId = stageConfig.TileSet;
			Tile[] tiles = new Tile[width];
			int halfWidth = width / 2;
			int halfBaseWidth = baseWidth / 2;
			int baseStartX = halfWidth - halfBaseWidth;
			int baseEndX = halfWidth + halfBaseWidth;
			if (config.TileSets.TryGetValue(tileSetId, out TileSetConfig tileSet))
			{
				int totalWeight = tileSet.Tiles.Sum(x => x.Value.Weight);
				Dictionary<string, TileConfig> pathTiles = tileSet.Tiles
					.Where(x => x.Value.Type is TileType.Path)
					.ToDictionary(x => x.Key, x => x.Value);
				string[] pathKeys = pathTiles.Keys.ToArray();
				string[] tileKeys = tileSet.Tiles.Keys.ToArray();
				Random random = new();
				for (int x = 0; x < width; x++)
				{
					if (x >= baseStartX && x <= baseEndX)
					{
						int randomValue = random.Next(pathTiles.Count);
						string key = pathKeys[randomValue];
						tiles[x] = new Tile()
						{
							TileConfigId = key
						};
					}
					else
					{
						int randomValue = random.Next(totalWeight);
						for (int t = 0; t < tileSet.Tiles.Count; t++)
						{
							string key = tileKeys[t];
							TileConfig tile = tileSet.Tiles[key];
							if (randomValue < tile.Weight)
							{
								tiles[x] = new Tile()
								{
									TileConfigId = key
								};
								break;
							}
							randomValue -= tile.Weight;
						}
					}
				}
			}
			foreach (KeyValuePair<int, string> kvp in stageConfig.Structures)
			{
				StructureConfig structureConfig = config.Structures[kvp.Value];
				tiles[halfWidth + kvp.Key].Structure = new Structure()
				{
					ResourceCount = structureConfig.ResourceCount,
					ResourceType = structureConfig.ResourceType,
					StructureConfigId = kvp.Value
				};
			}
			return new Stage()
			{
				Tiles = tiles,
				TileSetId = tileSetId
			};
		}
	}
}
