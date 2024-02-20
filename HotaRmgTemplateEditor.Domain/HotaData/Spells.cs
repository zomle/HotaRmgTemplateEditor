using System;

namespace HotaRmgTemplateEditor.Domain.HotaData
{
	public static class Spells
	{
		public static readonly Spell[] All;
		public static readonly IReadOnlyDictionary<int, Spell> Lookup;

		static Spells()
		{
			All =
			[
				new Spell(0, "Summon boat", MagicSchool.Water, DefaultAvailability.Conditional),
				new Spell(1, "Scuttle Boat", MagicSchool.Water, DefaultAvailability.Conditional),
				new Spell(2, "Visions", MagicSchool.All),
				new Spell(3, "View Earth", MagicSchool.Earth),
				new Spell(4, "Disguise", MagicSchool.Air),
				new Spell(5, "View Air", MagicSchool.Air),
				new Spell(6, "Fly", MagicSchool.Air),
				new Spell(7, "Water Walk", MagicSchool.Water, DefaultAvailability.Conditional),
				new Spell(8, "Dimension Door", MagicSchool.Air),
				new Spell(9, "Town Portal", MagicSchool.Earth),
				new Spell(10, "Quicksand", MagicSchool.Earth),
				new Spell(11, "Land Mine", MagicSchool.Fire),
				new Spell(12, "Force Field", MagicSchool.Earth),
				new Spell(13, "Fire Wall", MagicSchool.Fire),
				new Spell(14, "Earthquake", MagicSchool.Earth),
				new Spell(15, "Magic Arrow", MagicSchool.All),
				new Spell(16, "Ice Bolt", MagicSchool.Water),
				new Spell(17, "Lightning Bolt", MagicSchool.Air),
				new Spell(18, "Implosion", MagicSchool.Earth),
				new Spell(19, "Chain Lightning", MagicSchool.Air),
				new Spell(20, "Frost Ring", MagicSchool.Water),
				new Spell(21, "Fireball", MagicSchool.Fire),
				new Spell(22, "Inferno", MagicSchool.Fire),
				new Spell(23, "Meteor Shower", MagicSchool.Earth),
				new Spell(24, "Death Ripple", MagicSchool.Earth),
				new Spell(25, "Destroy Undead", MagicSchool.Air),
				new Spell(26, "Armageddon", MagicSchool.Fire),
				new Spell(27, "Shield", MagicSchool.Earth),
				new Spell(28, "Air Shield", MagicSchool.Air),
				new Spell(29, "Fire Shield", MagicSchool.Fire),
				new Spell(30, "Protection from Air", MagicSchool.Air),
				new Spell(31, "Protection from Fire", MagicSchool.Fire),
				new Spell(32, "Protection from Water", MagicSchool.Water),
				new Spell(33, "Protection from Earth", MagicSchool.Earth),
				new Spell(34, "Anti-Magic", MagicSchool.Earth),
				new Spell(35, "Dispel", MagicSchool.Water),
				new Spell(36, "Magic Mirror", MagicSchool.Air),
				new Spell(37, "Cure", MagicSchool.Water),
				new Spell(38, "Resurrection", MagicSchool.Earth),
				new Spell(39, "Animate Dead", MagicSchool.Earth),
				new Spell(40, "Sacrifice", MagicSchool.Fire),
				new Spell(41, "Bless", MagicSchool.Water),
				new Spell(42, "Curse", MagicSchool.Fire),
				new Spell(43, "Bloodlust", MagicSchool.Fire),
				new Spell(44, "Precision", MagicSchool.Air),
				new Spell(45, "Weakness", MagicSchool.Water),
				new Spell(46, "Stone Skin", MagicSchool.Earth),
				new Spell(47, "Disrupting Ray", MagicSchool.Air),
				new Spell(48, "Prayer", MagicSchool.Water),
				new Spell(49, "Mirth", MagicSchool.Water),
				new Spell(50, "Sorrow", MagicSchool.Earth),
				new Spell(51, "Fortune", MagicSchool.Air),
				new Spell(52, "Misfortune", MagicSchool.Fire),
				new Spell(53, "Haste", MagicSchool.Air),
				new Spell(54, "Slow", MagicSchool.Earth),
				new Spell(55, "Slayer", MagicSchool.Fire),
				new Spell(56, "Frenzy", MagicSchool.Fire),
				new Spell(58, "Counterstrike", MagicSchool.Air),
				new Spell(59, "Berserk", MagicSchool.Fire),
				new Spell(60, "Hypnotize", MagicSchool.Air),
				new Spell(61, "Forgetfulness", MagicSchool.Water),
				new Spell(62, "Blind", MagicSchool.Fire),
				new Spell(63, "Teleport", MagicSchool.Water),
				new Spell(64, "Remove Obstacle", MagicSchool.Water),
				new Spell(65, "Clone", MagicSchool.Water),
				new Spell(66, "Fire Elemental", MagicSchool.Fire),
				new Spell(67, "Earth Elemental", MagicSchool.Earth),
				new Spell(68, "Water Elemental", MagicSchool.Water),
				new Spell(69, "Air Elemental", MagicSchool.Air)
			];

			Lookup = All.ToDictionary(a => a.Id, a => a);
		}
	}
}
