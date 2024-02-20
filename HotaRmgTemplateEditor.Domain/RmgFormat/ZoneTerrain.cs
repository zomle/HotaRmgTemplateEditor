namespace HotaRmgTemplateEditor.Domain.RmgFormat
{
	public class ZoneTerrain
	{
		public bool MatchToTown { get; set; }
		public bool Dirt { get; set; }
		public bool Sand { get; set; }
		public bool Grass { get; set; }
		public bool Snow { get; set; }
		public bool Swamp { get; set; }
		public bool Rough { get; set; }
		public bool Subterranean { get; set; } // cave
		public bool Lava { get; set; }
		public bool Highlands { get; set; }
		public bool Wasteland { get; set; }
	}
}
