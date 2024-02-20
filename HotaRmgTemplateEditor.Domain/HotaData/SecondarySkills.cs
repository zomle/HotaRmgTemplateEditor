namespace HotaRmgTemplateEditor.Domain.HotaData
{
	public static class SecondarySkills
	{
		public static readonly SecondarySkill[] All; 
		public static readonly IReadOnlyDictionary<int, SecondarySkill> Lookup;
		static SecondarySkills()
		{
			All =
			[
				new SecondarySkill(0, "Pathfinding"),
				new SecondarySkill(1, "Archery"),
				new SecondarySkill(2, "Logistics"),
				new SecondarySkill(3, "Scouting"),
				new SecondarySkill(4, "Diplomacy"),
				new SecondarySkill(5, "Navigation", DefaultAvailability.Conditional),
				new SecondarySkill(6, "Leadership"),
				new SecondarySkill(7, "Wisdom"),
				new SecondarySkill(8, "Mysticism"),
				new SecondarySkill(9, "Luck"),
				new SecondarySkill(10, "Ballistics"),
				new SecondarySkill(11, "Eagle Eye"),
				new SecondarySkill(12, "Necromancy"),
				new SecondarySkill(13, "Estates"),
				new SecondarySkill(14, "Fire Magic"),
				new SecondarySkill(15, "Air Magic"),
				new SecondarySkill(16, "Water Magic"),
				new SecondarySkill(17, "Earth Magic"),
				new SecondarySkill(18, "Scholar"),
				new SecondarySkill(19, "Tactics"),
				new SecondarySkill(20, "Artillery"),
				new SecondarySkill(21, "Learning"),
				new SecondarySkill(22, "Offense"),
				new SecondarySkill(23, "Armorer"),
				new SecondarySkill(24, "Intelligence"),
				new SecondarySkill(25, "Sorcery"),
				new SecondarySkill(26, "Resistance", DefaultAvailability.Disabled),
				new SecondarySkill(27, "First Aid"),
				new SecondarySkill(28, "Interference")
			];

			Lookup = All.ToDictionary(a => a.Id, a => a);
		}
	}
}
