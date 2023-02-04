using System;

namespace RootCellar.Data.Config
{
	public sealed class StructureConfig
	{
		public Level[] Levels { get; set; } = Array.Empty<Level>();

		public string Name { get; set; } = string.Empty;

		public int ResourceCount { get; set; }

		public ResourceType ResourceType { get; set; }

		public StructureType Type { get; set; }

		public sealed class Level
		{
			public int Capacity { get; set; }

			public LevelCost Cost { get; set; }

			public string Event { get; set; } = string.Empty;

			public string Image { get; set; } = string.Empty;
		}

		public class LevelCost
		{
			public int Amount { get; set; }

			public string ResourceId { get; set; }
		}
	}
}
