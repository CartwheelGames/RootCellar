using System.Collections.Generic;

namespace VerdantVibes.Data.Config
{
	public sealed class CropConfig
	{
		public int Days { get; set; }

		public Dictionary<int, string> ImagesByDay { get; set; } = new();

		/// <remarks> Out of 100 </remarks>
		public byte SeedChance { get; set; }

		public string SeedImage { get; set; } = string.Empty;

		public int Yield { get; set; }
	}
}
