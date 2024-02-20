namespace HotaRmgTemplateEditor.Domain.RmgFormat
{
	public class ZoneTreasure
	{
		public ZoneTreasureSetting Range1 { get; set; }
		public ZoneTreasureSetting Range2 { get; set; }
		public ZoneTreasureSetting Range3 { get; set; }
		public bool ForceNeutralCreatures { get; set; }

		public ZoneTreasure()
		{
			Range1 = new ZoneTreasureSetting();
			Range2 = new ZoneTreasureSetting();
			Range3 = new ZoneTreasureSetting();
		}
	}
}
