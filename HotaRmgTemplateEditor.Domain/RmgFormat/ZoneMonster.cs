namespace HotaRmgTemplateEditor.Domain.RmgFormat
{
	public class ZoneMonster
	{
		public ZoneMonsterStrength Strength { get; set; }
		public ZoneMonsterDisposition Disposition { get; set; }
		public ZoneMonsterJoinPercent JoinPercent { get; set; }
		public bool JoinOnlyForMoney { get; set; }
		public bool MatchToTown { get; set; }
		public bool Neutral { get; set; }
		public bool Castle { get; set; }
		public bool Rampart { get; set; }
		public bool Tower { get; set; }
		public bool Inferno { get; set; }
		public bool Necropolis { get; set; }
		public bool Dungeon { get; set; }
		public bool Stronghold { get; set; }
		public bool Fortress { get; set; }
		public bool Conflux { get; set; }
		public bool Cove { get; set; }
	}
}
