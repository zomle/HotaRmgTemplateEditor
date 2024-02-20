﻿namespace HotaRmgTemplateEditor.Domain.HotaData
{
	public static class Artifacts
	{
		public static readonly Artifact[] All;
		public static readonly IReadOnlyDictionary<int, Artifact> Lookup;

		static Artifacts()
		{
			All =
			[
				new Artifact(136, "Admiral's Hat​", ArtifactClass.Relic, DefaultAvailability.Disabled),
				new Artifact(68, "Ambassador's Sash​", ArtifactClass.Major),
				new Artifact(54, "Amulet of the Undertaker​", ArtifactClass.Treasure),
				new Artifact(62, "Angel Feather Arrows​", ArtifactClass.Major),
				new Artifact(72, "Angel Wings​", ArtifactClass.Relic),
				new Artifact(129, "Angelic Alliance​", ArtifactClass.Relic, DefaultAvailability.Disabled),
				new Artifact(128, "Armageddon's Blade", ArtifactClass.Relic, DefaultAvailability.Disabled),
				new Artifact(132, "Armor of the Damned​", ArtifactClass.Relic, DefaultAvailability.Disabled),
				new Artifact(31, "Armor of Wonder​", ArtifactClass.Minor),
				new Artifact(121, "Arms of Legion​", ArtifactClass.Major),
				new Artifact(49, "Badge of Courage​", ArtifactClass.Treasure),
				new Artifact(63, "Bird of Perception​", ArtifactClass.Treasure),
				new Artifact(8, "Blackshard of the Dead Knight​", ArtifactClass.Minor),
				new Artifact(90, "Boots of Levitation​", ArtifactClass.Relic, DefaultAvailability.Conditional),
				new Artifact(59, "Boots of Polarity​", ArtifactClass.Relic, DefaultAvailability.Disabled),
				new Artifact(98, "Boots of Speed​", ArtifactClass.Minor),
				new Artifact(60, "Bow of Elven Cherrywood​", ArtifactClass.Treasure),
				new Artifact(137, "Bow of the Sharpshooter​", ArtifactClass.Relic, DefaultAvailability.Disabled),
				new Artifact(61, "Bowstring of the Unicorn's Mane​", ArtifactClass.Minor),
				new Artifact(29, "Breastplate of Brimstone​", ArtifactClass.Major),
				new Artifact(25, "Breastplate of Petrified Wood​", ArtifactClass.Treasure),
				new Artifact(15, "Buckler of the Gnoll King​", ArtifactClass.Minor),
				new Artifact(78, "Cape of Conjuring​", ArtifactClass.Treasure),
				new Artifact(159, "Cape of Silence​", ArtifactClass.Major),
				new Artifact(99, "Cape of Velocity​", ArtifactClass.Major),
				new Artifact(47, "Cards of Prophecy​", ArtifactClass.Treasure),
				new Artifact(33, "Celestial Necklace of Bliss​", ArtifactClass.Relic),
				new Artifact(7, "Centaur's Axe​", ArtifactClass.Treasure),
				new Artifact(162, "Charm of Eclipse​", ArtifactClass.Minor),
				new Artifact(73, "Charm of Mana​", ArtifactClass.Treasure),
				new Artifact(130, "Cloak of the Undead King​", ArtifactClass.Relic, DefaultAvailability.Disabled),
				new Artifact(46, "Clover of Fortune​", ArtifactClass.Treasure),
				new Artifact(76, "Collar of Conjuring​", ArtifactClass.Treasure),
				new Artifact(140, "Cornucopia​", ArtifactClass.Relic, DefaultAvailability.Disabled),
				new Artifact(50, "Crest of Valor​", ArtifactClass.Treasure),
				new Artifact(44, "Crown of Dragontooth​", ArtifactClass.Relic),
				new Artifact(150, "Crown of the Five Seas​", ArtifactClass.Major),
				new Artifact(22, "Crown of the Supreme Magi​", ArtifactClass.Minor),
				new Artifact(56, "Dead Man's Boots​", ArtifactClass.Major),
				new Artifact(153, "Demon's Horseshoe​", ArtifactClass.Treasure),
				new Artifact(141, "Diplomat's Cloak​", ArtifactClass.Relic, DefaultAvailability.Disabled),
				new Artifact(67, "Diplomat's Ring​", ArtifactClass.Major),
				new Artifact(40, "Dragon Scale Armor​", ArtifactClass.Relic),
				new Artifact(39, "Dragon Scale Shield​", ArtifactClass.Major),
				new Artifact(42, "Dragon Wing Tabard​", ArtifactClass.Minor),
				new Artifact(41, "Dragonbone Greaves​", ArtifactClass.Treasure),
				new Artifact(131, "Elixir of Life​", ArtifactClass.Relic),
				new Artifact(65, "Emblem of Cognizance​", ArtifactClass.Minor),
				new Artifact(116, "Endless Bag of Gold​", ArtifactClass.Major),
				new Artifact(117, "Endless Purse of Gold​", ArtifactClass.Major),
				new Artifact(115, "Endless Sack of Gold​", ArtifactClass.Relic),
				new Artifact(70, "Equestrian's Gloves​", ArtifactClass.Minor),
				new Artifact(109, "Everflowing Crystal Cloak​", ArtifactClass.Major),
				new Artifact(111, "Everpouring Vial of Mercury​", ArtifactClass.Major),
				new Artifact(113, "Eversmoking Ring of Sulfur​", ArtifactClass.Major),
				new Artifact(57, "Garniture of Interference​", ArtifactClass.Major, DefaultAvailability.Disabled),
				new Artifact(51, "Glyph of Gallantry​", ArtifactClass.Treasure),
				new Artifact(91, "Golden Bow​", ArtifactClass.Major),
				new Artifact(160, "Golden Goose​", ArtifactClass.Relic, DefaultAvailability.Disabled),
				new Artifact(9, "Greater Gnoll's Flail​", ArtifactClass.Minor),
				new Artifact(122, "Head of Legion​", ArtifactClass.Major),
				new Artifact(23, "Hellstorm Helmet​", ArtifactClass.Major),
				new Artifact(21, "Helm of Chaos​", ArtifactClass.Minor),
				new Artifact(36, "Helm of Heavenly Enlightenment​", ArtifactClass.Relic),
				new Artifact(19, "Helm of the Alabaster Unicorn​", ArtifactClass.Treasure),
				new Artifact(155, "Hideous Mask​", ArtifactClass.Minor),
				new Artifact(161, "Horn of the Abyss", ArtifactClass.Relic, DefaultAvailability.Disabled),
				new Artifact(85, "Hourglass of the Evil Hour​", ArtifactClass.Treasure),
				new Artifact(114, "Inexhaustible Cart of Lumber​", ArtifactClass.Minor),
				new Artifact(112, "Inexhaustible Cart of Ore​", ArtifactClass.Minor),
				new Artifact(143, "Ironfist of the Ogre​", ArtifactClass.Relic, DefaultAvailability.Disabled),
				new Artifact(48, "Ladybird of Luck​", ArtifactClass.Treasure),
				new Artifact(118, "Legs of Legion​", ArtifactClass.Treasure),
				new Artifact(34, "Lion's Shield of Courage​", ArtifactClass.Relic),
				new Artifact(119, "Loins of Legion​", ArtifactClass.Minor),
				new Artifact(75, "Mystic Orb of Mana​", ArtifactClass.Treasure),
				new Artifact(43, "Necklace of Dragonteeth​", ArtifactClass.Major),
				new Artifact(71, "Necklace of Ocean Guidance​", ArtifactClass.Major, DefaultAvailability.Conditional),
				new Artifact(97, "Necklace of Swiftness​", ArtifactClass.Treasure),
				new Artifact(10, "Ogre's Club of Havoc​", ArtifactClass.Major),
				new Artifact(82, "Orb of Driving Rain​", ArtifactClass.Major),
				new Artifact(126, "Orb of Inhibition​", ArtifactClass.Relic, DefaultAvailability.Disabled),
				new Artifact(80, "Orb of Silt​", ArtifactClass.Major),
				new Artifact(81, "Orb of Tempestuous Fire​", ArtifactClass.Major),
				new Artifact(79, "Orb of the Firmament​", ArtifactClass.Major),
				new Artifact(93, "Orb of Vulnerability​", ArtifactClass.Relic),
				new Artifact(108, "Pendant of Courage​", ArtifactClass.Major),
				new Artifact(104, "Pendant of Death​", ArtifactClass.Treasure),
				new Artifact(100, "Pendant of Dispassion​", ArtifactClass.Treasure),
				new Artifact(157, "Pendant of Downfall​", ArtifactClass.Major),
				new Artifact(105, "Pendant of Free Will​", ArtifactClass.Treasure),
				new Artifact(102, "Pendant of Holiness​", ArtifactClass.Treasure),
				new Artifact(103, "Pendant of Life​", ArtifactClass.Treasure),
				new Artifact(106, "Pendant of Negativity​", ArtifactClass.Major),
				new Artifact(142, "Pendant of Reflection​", ArtifactClass.Relic, DefaultAvailability.Disabled),
				new Artifact(101, "Pendant of Second Sight​", ArtifactClass.Major),
				new Artifact(107, "Pendant of Total Recall​", ArtifactClass.Treasure),
				new Artifact(164, "Plate of Dying Light", ArtifactClass.Relic),
				new Artifact(134, "Power of the Dragon Father​", ArtifactClass.Relic, DefaultAvailability.Disabled),
				new Artifact(37, "Quiet Eye of the Dragon​", ArtifactClass.Treasure),
				new Artifact(83, "Recanter's Cloak​", ArtifactClass.Major, DefaultAvailability.Disabled),
				new Artifact(38, "Red Dragon Flame Tongue​", ArtifactClass.Minor),
				new Artifact(26, "Rib Cage​", ArtifactClass.Minor),
				new Artifact(77, "Ring of Conjuring​", ArtifactClass.Treasure),
				new Artifact(110, "Ring of Infinite Gems​", ArtifactClass.Major),
				new Artifact(95, "Ring of Life​", ArtifactClass.Minor),
				new Artifact(158, "Ring of Oblivion​", ArtifactClass.Major),
				new Artifact(156, "Ring of Suppression​", ArtifactClass.Treasure),
				new Artifact(139, "Ring of the Magi​", ArtifactClass.Relic, DefaultAvailability.Disabled),
				new Artifact(69, "Ring of the Wayfarer​", ArtifactClass.Major),
				new Artifact(94, "Ring of Vitality​", ArtifactClass.Treasure),
				new Artifact(149, "Royal Armor of Nix​", ArtifactClass.Major),
				new Artifact(152, "Runes of Imminency​", ArtifactClass.Treasure),
				new Artifact(32, "Sandals of the Saint​", ArtifactClass.Relic),
				new Artifact(27, "Scales of the Greater Basilisk​", ArtifactClass.Minor),
				new Artifact(123, "Sea Captain's Hat​", ArtifactClass.Relic, DefaultAvailability.Conditional),
				new Artifact(163, "Seal of Sunset​", ArtifactClass.Minor),
				new Artifact(18, "Sentinel's Shield​", ArtifactClass.Relic),
				new Artifact(125, "Shackles of War​", ArtifactClass.Major),
				new Artifact(154, "Shaman's Puppet​", ArtifactClass.Minor),
				new Artifact(148, "Shield of Naval Glory​", ArtifactClass.Minor),
				new Artifact(17, "Shield of the Damned​", ArtifactClass.Major),
				new Artifact(13, "Shield of the Dwarven Lords​", ArtifactClass.Treasure),
				new Artifact(14, "Shield of the Yawning Dead​", ArtifactClass.Minor),
				new Artifact(20, "Skull Helmet​", ArtifactClass.Treasure),
				new Artifact(52, "Speculum​", ArtifactClass.Treasure),
				new Artifact(124, "Spellbinder's Hat​", ArtifactClass.Relic),
				new Artifact(92, "Sphere of Permanence​", ArtifactClass.Major),
				new Artifact(84, "Spirit of Oppression​", ArtifactClass.Treasure),
				new Artifact(53, "Spyglass​", ArtifactClass.Treasure),
				new Artifact(66, "Statesman's Medal​", ArtifactClass.Major),
				new Artifact(133, "Statue of Legion​", ArtifactClass.Relic, DefaultAvailability.Disabled),
				new Artifact(45, "Still Eye of the Dragon​", ArtifactClass.Treasure),
				new Artifact(64, "Stoic Watchman​", ArtifactClass.Treasure),
				new Artifact(58, "Surcoat of Counterpoise​", ArtifactClass.Major, DefaultAvailability.Disabled),
				new Artifact(11, "Sword of Hellfire​", ArtifactClass.Major),
				new Artifact(35, "Sword of Judgement​", ArtifactClass.Relic),
				new Artifact(74, "Talisman of Mana​", ArtifactClass.Treasure),
				new Artifact(16, "Targ of the Rampaging Ogre​", ArtifactClass.Major),
				new Artifact(24, "Thunder Helmet​", ArtifactClass.Relic),
				new Artifact(30, "Titan's Cuirass​", ArtifactClass.Relic),
				new Artifact(12, "Titan's Gladius​", ArtifactClass.Relic),
				new Artifact(135, "Titan's Thunder​", ArtifactClass.Relic, DefaultAvailability.Disabled),
				new Artifact(87, "Tome of Air​", ArtifactClass.Relic),
				new Artifact(89, "Tome of Earth​", ArtifactClass.Relic),
				new Artifact(86, "Tome of Fire​", ArtifactClass.Relic),
				new Artifact(88, "Tome of Water​", ArtifactClass.Relic),
				new Artifact(120, "Torso of Legion​", ArtifactClass.Minor),
				new Artifact(147, "Trident of Dominion​", ArtifactClass.Major),
				new Artifact(28, "Tunic of the Cyclops King​", ArtifactClass.Major),
				new Artifact(55, "Vampire's Cowl​", ArtifactClass.Minor),
				new Artifact(127, "Vial of Dragon Blood​", ArtifactClass.Relic, DefaultAvailability.Disabled),
				new Artifact(96, "Vial of Lifeblood​", ArtifactClass.Major),
				new Artifact(151, "Wayfarer's Boots​", ArtifactClass.Major),
				new Artifact(138, "Wizard's Well​", ArtifactClass.Relic, DefaultAvailability.Disabled)
			];

			Lookup = All.ToDictionary(a => a.Id, a => a);
		}
	}
}
