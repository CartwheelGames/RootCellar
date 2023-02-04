namespace VerdantVibes.Data.State
{
	public class Structure
	{
		public int Level { get; set; }

		public float Progress { get; set; }

		public int ResourceCount { get; set; }

		public ResourceType ResourceType { get; set; } = new();

		public string StructureConfigId { get; set; } = string.Empty;
	}
}
