using HotaRmgTemplateEditor.Domain.HotaData;
using HotaRmgTemplateEditor.Domain.RmgFormat.Overrides;
using HotaRmgTemplateEditor.Domain.RmgFormat.TownTypeHints;
using System.Globalization;
using System.Text;

namespace HotaRmgTemplateEditor.Domain.RmgFormat.Handler
{
	public class TemplatePackWriter
	{ 
		private const int TemplatePackFieldCount = 14;
		private const int TemplateFieldCount = 13;
		private const int ZoneFieldCount = 89;
		private const int ConnectionFieldCount = 13;

		public static string WriteFile(TemplatePack pack)
		{
			var sb = new StringBuilder();
			using var writer = new StringWriter(sb);

			WriteHeader(writer);
			WriteTemplatePack(writer, pack);

			writer.Flush();
			return sb.ToString();
		}

		private static void WriteTemplatePack(StringWriter writer, TemplatePack pack)
		{
			bool packWritten = false;
			foreach (var template in pack.Templates)
			{
				bool templateWritten = false;

				var connLinks = template.Connections.SelectMany(c => c.Links.Select(l => (c, l))).ToList();

				int maxIndex = Math.Max(template.Zones.Count, connLinks.Count);
				for (int i = 0; i < maxIndex; i++)
				{
					var zone = i < template.Zones.Count ? template.Zones[i] : null;
					var connLink = i < connLinks.Count ? connLinks[i] : (null, null);

					WriteLine(writer, packWritten ? null : pack, templateWritten ? null : template, zone, connLink.Item1, connLink.Item2);
					packWritten = true;
					templateWritten = true;

				}
			}
		}

		private static void WriteTemplatePackFields(StringWriter writer, TemplatePack pack)
		{
			Field(writer, pack.FieldCount.Town);
			Field(writer, pack.FieldCount.Terrain);
			Field(writer, pack.FieldCount.ZoneType);
			Field(writer, pack.FieldCount.PackNew);
			Field(writer, pack.FieldCount.MapNew);
			Field(writer, pack.FieldCount.ZoneNew);
			Field(writer, pack.FieldCount.ConnectionNew);

			Field(writer, pack.Options.Name);
			Multiline(writer, pack.Options.Description);
			OverrideList(writer, pack.Options.TownSelection.Values.Where(t => !t.IsAllowed));
			OverrideList(writer, pack.Options.HeroOverrides.Values.Where(h => h.EnableDisable != EnableDisableDefault.Default));
			Field(writer, pack.Options.Mirror);
			Field(writer, string.IsNullOrEmpty(pack.Options.Tags) ? string.Empty : throw new NotImplementedException());
			Field(writer, pack.Options.MaxBattleRoundsOverridden ? pack.Options.MaxBattleRounds.ToString() : string.Empty);
		}

		private static void WriteTemplateFields(StringWriter writer, Template template)
		{
			Field(writer, template.Name);
			Field(writer, (int)template.MinimumSize);
			Field(writer, (int)template.MaximumSize);
			OverrideList(writer, template.ArtifactOverrides.Values.Where(a => a.EnableDisable != EnableDisableDefault.Default));
			Field(writer, string.Empty);
			OverrideList(writer, template.SpellOverrides.Values.Where(a => a.EnableDisable != EnableDisableDefault.Default));
			OverrideList(writer, template.SecondarySkillOverrides.Values.Where(a => a.EnableDisable != EnableDisableDefault.Default));
			OverrideList(writer, template.ObjectOverrides.Where(a => a.EnableDisable != EnableDisableDefault.Default));

			if (template.RockBlockRadius.Setting == EnableDisableCustom.Enable)
			{
				Field(writer, "x");
			}
			else if (template.RockBlockRadius.Setting == EnableDisableCustom.Disable)
			{
				Field(writer, string.Empty);
			}
			else
			{
				Field(writer, template.RockBlockRadius.CustomValue);
			}

			if (template.ZoneSparseness.Default)
			{
				Field(writer, string.Empty);
			}
			else
			{
				Field(writer, template.ZoneSparseness.CustomValue);
			}

			Field(writer, template.SpecialWeeksDisabled);
			Field(writer, template.SpellResearch);
			Field(writer, template.Anarchy);
		}

		private static void WriteZoneFields(StringWriter writer, Zone zone)
		{
			Field(writer, zone.Id);
			Field(writer, zone.Type == ZoneType.HumanStart);
			Field(writer, zone.Type == ZoneType.ComputerStart);
			Field(writer, zone.Type == ZoneType.Treasure);
			Field(writer, zone.Type == ZoneType.Junction);
			Field(writer, zone.Size);
			Field(writer, zone.Restrictions.MinimumHumanPlayers);
			Field(writer, zone.Restrictions.MaximumHumanPlayers);
			Field(writer, zone.Restrictions.MinimumTotalPlayers);
			Field(writer, zone.Restrictions.MaximumTotalPlayers);

			NonZeroField(writer, zone.Type == ZoneType.HumanStart || zone.Type == ZoneType.ComputerStart
				? zone.PlayerTowns.Ownership
				: 0);
			NonZeroField(writer, zone.PlayerTowns.MinimumTowns);
			NonZeroField(writer, zone.PlayerTowns.MinimumCastles);
			NonZeroField(writer, zone.PlayerTowns.TownDensity);
			NonZeroField(writer, zone.PlayerTowns.CastleDensity);

			NonZeroField(writer, zone.NeutralTowns.MinimumTowns);
			NonZeroField(writer, zone.NeutralTowns.MinimumCastles);
			NonZeroField(writer, zone.NeutralTowns.TownDensity);
			NonZeroField(writer, zone.NeutralTowns.CastleDensity);
			Field(writer, zone.NeutralTowns.TownsAreOfSameType);

			Field(writer, zone.AllowedTowns.Castle);
			Field(writer, zone.AllowedTowns.Rampart);
			Field(writer, zone.AllowedTowns.Tower);
			Field(writer, zone.AllowedTowns.Inferno);
			Field(writer, zone.AllowedTowns.Necropolis);
			Field(writer, zone.AllowedTowns.Dungeon);
			Field(writer, zone.AllowedTowns.Stronghold);
			Field(writer, zone.AllowedTowns.Fortress);
			Field(writer, zone.AllowedTowns.Conflux);
			Field(writer, zone.AllowedTowns.Cove);

			NonZeroField(writer, zone.Mines.WoodMinimum);
			NonZeroField(writer, zone.Mines.MercuryMinimum);
			NonZeroField(writer, zone.Mines.OreMinimum);
			NonZeroField(writer, zone.Mines.SulfurMinimum);
			NonZeroField(writer, zone.Mines.CrystalMinimum);
			NonZeroField(writer, zone.Mines.GemsMinimum);
			NonZeroField(writer, zone.Mines.GoldMinimum);

			NonZeroField(writer, zone.Mines.WoodDensity);
			NonZeroField(writer, zone.Mines.MercuryDensity);
			NonZeroField(writer, zone.Mines.OreDensity);
			NonZeroField(writer, zone.Mines.SulfurDensity);
			NonZeroField(writer, zone.Mines.CrystalDensity);
			NonZeroField(writer, zone.Mines.GemsDensity);
			NonZeroField(writer, zone.Mines.GoldDensity);

			Field(writer, zone.Terrain.MatchToTown);
			Field(writer, zone.Terrain.Dirt);
			Field(writer, zone.Terrain.Sand);
			Field(writer, zone.Terrain.Grass);
			Field(writer, zone.Terrain.Snow);
			Field(writer, zone.Terrain.Swamp);
			Field(writer, zone.Terrain.Rough);
			Field(writer, zone.Terrain.Subterranean);
			Field(writer, zone.Terrain.Lava);
			Field(writer, zone.Terrain.Highlands);
			Field(writer, zone.Terrain.Wasteland);

			Field(writer, zone.Monsters.Strength switch
			{
				ZoneMonsterStrength.None => "none",
				ZoneMonsterStrength.Weak => "weak",
				ZoneMonsterStrength.Average => "avg",
				ZoneMonsterStrength.Strong => "strong",
				_ => throw new InvalidOperationException(),
			});
			Field(writer, zone.Monsters.MatchToTown);
			Field(writer, zone.Monsters.Neutral);
			Field(writer, zone.Monsters.Castle);
			Field(writer, zone.Monsters.Rampart);
			Field(writer, zone.Monsters.Tower);
			Field(writer, zone.Monsters.Inferno);
			Field(writer, zone.Monsters.Necropolis);
			Field(writer, zone.Monsters.Dungeon);
			Field(writer, zone.Monsters.Stronghold);
			Field(writer, zone.Monsters.Fortress);
			Field(writer, zone.Monsters.Conflux);
			Field(writer, zone.Monsters.Cove);

			if (zone.Treasures.Range1.Enabled)
			{
				Field(writer, zone.Treasures.Range1.Low);
				Field(writer, zone.Treasures.Range1.High);
				Field(writer, zone.Treasures.Range1.Density);
			}
			else
			{
				Field(writer, string.Empty, 3);
			}

			if (zone.Treasures.Range2.Enabled)
			{
				Field(writer, zone.Treasures.Range2.Low);
				Field(writer, zone.Treasures.Range2.High);
				Field(writer, zone.Treasures.Range2.Density);
			}
			else
			{
				Field(writer, string.Empty, 3);
			}

			if (zone.Treasures.Range3.Enabled)
			{
				Field(writer, zone.Treasures.Range3.Low);
				Field(writer, zone.Treasures.Range3.High);
				Field(writer, zone.Treasures.Range3.Density);
			}
			else
			{
				Field(writer, string.Empty, 3);
			}

			Field(writer, zone.Placement switch
			{
				ZonePlacement.Free => string.Empty,
				ZonePlacement.Ground => "ground",
				ZonePlacement.Underground => "underground",
				_ => throw new NotImplementedException(),
			});
			OverrideList(writer, zone.ObjectOverrides.Where(a => a.EnableDisable != EnableDisableDefault.Default));
			Field(writer, string.Empty);
			Field(writer, $"{zone.ImageSettings.X:0.###} {zone.ImageSettings.Y:0.###}");
			Field(writer, zone.Treasures.ForceNeutralCreatures);
			Field(writer, zone.AllowNonCoherentRoad);
			Field(writer, zone.ZoneRepulsion);
			TownHintList(writer, zone.TownHints);
			Field(writer, zone.Monsters.Disposition switch
			{
				ZoneMonsterDisposition.AlwaysJoin => 0,
				ZoneMonsterDisposition.Friendly => 1,
				ZoneMonsterDisposition.Aggressive => 2,
				ZoneMonsterDisposition.Hostile => 3,
				ZoneMonsterDisposition.Savage => 4,
				ZoneMonsterDisposition.Custom1 or
				ZoneMonsterDisposition.Custom2 or
				ZoneMonsterDisposition.Custom3 or
				ZoneMonsterDisposition.Custom4 or
				ZoneMonsterDisposition.Custom5 or
				ZoneMonsterDisposition.Custom6 or
				ZoneMonsterDisposition.Custom7 or
				ZoneMonsterDisposition.Custom8 or
				ZoneMonsterDisposition.Custom9 => 5,
				_ => throw new InvalidOperationException()
			});

			NonZeroField(writer, zone.Monsters.Disposition switch
			{
				ZoneMonsterDisposition.AlwaysJoin or
				ZoneMonsterDisposition.Friendly or
				ZoneMonsterDisposition.Aggressive or
				ZoneMonsterDisposition.Hostile or
				ZoneMonsterDisposition.Savage => 0,
				ZoneMonsterDisposition.Custom1 => 1,
				ZoneMonsterDisposition.Custom2 => 2,
				ZoneMonsterDisposition.Custom3 => 3,
				ZoneMonsterDisposition.Custom4 => 4,
				ZoneMonsterDisposition.Custom5 => 5,
				ZoneMonsterDisposition.Custom6 => 6,
				ZoneMonsterDisposition.Custom7 => 7,
				ZoneMonsterDisposition.Custom8 => 8,
				ZoneMonsterDisposition.Custom9 => 9,
				_ => throw new InvalidOperationException()
			});

			Field(writer, zone.Monsters.JoinPercent switch
			{
				ZoneMonsterJoinPercent.Percent25 => 0,
				ZoneMonsterJoinPercent.Percent50 => 1,
				ZoneMonsterJoinPercent.Percent75 => 2,
				ZoneMonsterJoinPercent.Percent100 => 3,
				_ => throw new InvalidOperationException()
			});

			Field(writer, zone.Monsters.JoinOnlyForMoney);
		}

		private static void WriteConnectionFields(StringWriter writer, Connection connection, ConnectionLink link)
		{
			Field(writer, connection.Zone1Id);
			Field(writer, connection.Zone2Id);
			NonZeroField(writer, link.Value);
			Field(writer, link.Type == ConnectionType.Wide);

			if (link.BorderGuard != null)
			{
				var borderText = string.Join(' ', link.BorderGuard.Properties);
				Field(writer, $"x {borderText}".Trim());
			}
			else
			{
				Field(writer, string.Empty);
			}

			Field(writer, link.Roads switch
			{
				ConnectionRoad.Random => string.Empty,
				ConnectionRoad.Yes => "+",
				ConnectionRoad.No => "-",
				_ => throw new InvalidOperationException()
			});

			Field(writer, link.PlacementHint switch
			{
				ConnectionPlacementHint.Default => string.Empty,
				ConnectionPlacementHint.Ground => "ground",
				ConnectionPlacementHint.UndergroundGate => "underground",
				ConnectionPlacementHint.Monolith => "teleport",
				ConnectionPlacementHint.Random => "random",
				_ => throw new InvalidOperationException(),
			});

			Field(writer, link.Type == ConnectionType.Fictive);
			Field(writer, link.PortalRepulsion);

			Field(writer, link.Restriction.MinimumHumanPlayers);
			Field(writer, link.Restriction.MaximumHumanPlayers);
			Field(writer, link.Restriction.MinimumTotalPlayers);
			Field(writer, link.Restriction.MaximumTotalPlayers,0);
		}

		private static void WriteLine(StringWriter writer, TemplatePack? pack, Template? template, Zone? zone, Connection? connection, ConnectionLink? connectionLink)
		{
			if (pack == null)
			{
				Field(writer, string.Empty, TemplatePackFieldCount);
			}
			else
			{
				WriteTemplatePackFields(writer, pack);
			}

			if (template == null)
			{
				Field(writer, string.Empty, TemplateFieldCount);
			}
			else
			{
				WriteTemplateFields(writer, template);
			}

			if (zone == null)
			{
				Field(writer, string.Empty, ZoneFieldCount);
			}
			else
			{
				WriteZoneFields(writer, zone);
			}

			if (connection == null || connectionLink == null)
			{
				Field(writer, string.Empty, ConnectionFieldCount);
			}
			else
			{
				WriteConnectionFields(writer, connection, connectionLink);
			}

			NewLine(writer);
		}

		private static void TownHintList(StringWriter writer, IEnumerable<TownTypeHint> hints, int tabCount = 1)
		{
			var formulas = hints.Select(e => e.GetFormula());
			Field(writer, string.Join(' ', formulas), tabCount);
		}

		private static void OverrideList(StringWriter writer, IEnumerable<IOverrideItem> overrideList, int tabCount = 1)
		{
			var formulas = overrideList.Select(e => e.GetFormula());
			foreach (var formula in formulas)
			{
				writer.Write(formula);
				writer.Write(' ');
			}

			Field(writer, string.Empty, tabCount);
		}

		private static void WriteHeader(StringWriter writer)
		{
			Field(writer, "Pack", 14);
			Field(writer, "Map", 13);
			Field(writer, "Zone", 9 + 5 * 2 * 8);
			Field(writer, "Connections", 13);
			NewLine(writer);

			Field(writer, "Field count", 7);
			Field(writer, "Options", 21);
			Field(writer, "Type", 5);
			Field(writer, "Restrictions", 4);
			Field(writer, "Player towns", 5);
			Field(writer, "Neutral towns", 5);
			Field(writer, "Town types", 10);
			Field(writer, "Minimum mines", 7);
			Field(writer, "Mine Density", 7);
			Field(writer, "Terrain", 11);
			Field(writer, "Monsters", 13);
			Field(writer, "Treasure", 9);
			Field(writer, "Options", 12);
			Field(writer, "Zones", 2);
			Field(writer, "Options", 7);
			Field(writer, "Restrictions", 3);
			NewLine(writer);

			Field(writer, "Town");
			Field(writer, "Terrain");
			Field(writer, "Zone type");
			Field(writer, "Pack new");
			Field(writer, "Map new");
			Field(writer, "Zone new");
			Field(writer, "Connection new");
			Field(writer, "Name");
			Field(writer, "Description");
			Field(writer, "Town selection");
			Field(writer, "Heroes");
			Field(writer, "Mirror");
			Field(writer, "Tags");
			Field(writer, "Max Battle Rounds");
			Field(writer, "Name");
			Field(writer, "Minimum Size");
			Field(writer, "Maximum Size");
			Field(writer, "Artifacts");
			Field(writer, "Combo Arts");
			Field(writer, "Spells");
			Field(writer, "Secondary skills");
			Field(writer, "Objects");
			Field(writer, "Rock blocks");
			Field(writer, "Zone sparseness");
			Field(writer, "Special weeks disabled");
			Field(writer, "Spell Research");
			Field(writer, "Anarchy");
			Field(writer, "Id");
			Field(writer, "human start");
			Field(writer, "computer start");
			Field(writer, "Treasure");
			Field(writer, "Junction");
			Field(writer, "Base Size");
			Field(writer, "Minimum human positions");
			Field(writer, "Maximum human positions");
			Field(writer, "Minimum total positions");
			Field(writer, "Maximum total positions");
			Field(writer, "Ownership");
			Field(writer, "Minimum towns");
			Field(writer, "Minimum castles");
			Field(writer, "Town Density");
			Field(writer, "Castle Density");
			Field(writer, "Minimum towns");
			Field(writer, "Minimum castles");
			Field(writer, "Town Density");
			Field(writer, "Castle Density");
			Field(writer, "Towns are of same type");
			Field(writer, "Castle");
			Field(writer, "Rampart");
			Field(writer, "Tower");
			Field(writer, "Inferno");
			Field(writer, "Necropolis");
			Field(writer, "Dungeon");
			Field(writer, "Stronghold");
			Field(writer, "Fortress");
			Field(writer, "Conflux");
			Field(writer, "Cove");
			Field(writer, "Wood");
			Field(writer, "Mercury");
			Field(writer, "Ore");
			Field(writer, "Sulfur");
			Field(writer, "Crystal");
			Field(writer, "Gems");
			Field(writer, "Gold");
			Field(writer, "Wood");
			Field(writer, "Mercury");
			Field(writer, "Ore");
			Field(writer, "Sulfur");
			Field(writer, "Crystal");
			Field(writer, "Gems");
			Field(writer, "Gold");
			Field(writer, "Match to town");
			Field(writer, "Dirt");
			Field(writer, "Sand");
			Field(writer, "Grass");
			Field(writer, "Snow");
			Field(writer, "Swamp");
			Field(writer, "Rough");
			Field(writer, "Cave");
			Field(writer, "Lava");
			Field(writer, "Highlands");
			Field(writer, "Wasteland");
			Field(writer, "Strength");
			Field(writer, "Match to town");
			Field(writer, "Neutral");
			Field(writer, "Castle");
			Field(writer, "Rampart");
			Field(writer, "Tower");
			Field(writer, "Inferno");
			Field(writer, "Necropolis");
			Field(writer, "Dungeon");
			Field(writer, "Stronghold");
			Field(writer, "Fortress");
			Field(writer, "Conflux");
			Field(writer, "Cove");
			Field(writer, "Low");
			Field(writer, "High");
			Field(writer, "Density");
			Field(writer, "Low");
			Field(writer, "High");
			Field(writer, "Density");
			Field(writer, "Low");
			Field(writer, "High");
			Field(writer, "Density");
			Field(writer, "Placement");
			Field(writer, "Objects");
			Field(writer, "Minimum objects");
			Field(writer, "Image settings");
			Field(writer, "Force neutral creatures");
			Field(writer, "Allow non-coherent road");
			Field(writer, "Zone repulsion");
			Field(writer, "Town Hint");
			Field(writer, "Monsters disposition (standard)");
			Field(writer, "Monsters disposition (custom)");
			Field(writer, "Monsters joining percentage");
			Field(writer, "Monsters join only for money");
			Field(writer, "Zone 1");
			Field(writer, "Zone 2");
			Field(writer, "Value");
			Field(writer, "Wide");
			Field(writer, "Border Guard");
			Field(writer, "Road");
			Field(writer, "Type");
			Field(writer, "Fictive");
			Field(writer, "Portal repulsion");
			Field(writer, "Minimum human positions");
			Field(writer, "Maximum human positions");
			Field(writer, "Minimum total positions");
			Field(writer, "Maximum total positions", 0);
			NewLine(writer);
		}

		private static void NewLine(StringWriter writer)
		{
			writer.WriteLine();
		}

		private static void NonZeroField(StringWriter writer, int val, int tabCount = 1)
		{
			if (val == 0)
			{
				Field(writer, string.Empty, tabCount);
			}
			else
			{
				Field(writer, val, tabCount);
			}
		}

		private static void Field(StringWriter writer, float val, int tabCount = 1)
		{
			var str = val.ToString("0.###", CultureInfo.InvariantCulture);
			Field(writer, str, tabCount);
		}

		private static void Field(StringWriter writer, bool val, int tabCount = 1)
		{
			Field(writer, val ? "x" : string.Empty, tabCount);
		}

		private static void Field(StringWriter writer, string? str, int tabCount = 1)
		{
			if (string.IsNullOrEmpty(str) == false)
			{
				writer.Write(str);
			}

			if (tabCount > 0)
			{
				writer.Write(new string('\t', tabCount));
			}
		}

		private static void Multiline(StringWriter writer, string? str, int tabCount = 1)
		{
			if (str != null)
			{
				str = str.Replace("\r\n", "\n");
				str = str.Replace("\"", string.Empty);
				if (str.Contains('\n'))
				{
					writer.Write('"');
					writer.Write(str);
					writer.Write('"');
				}
				else
				{
					writer.Write(str);
				}				
			}

			if (tabCount > 0)
			{
				writer.Write(new string('\t', tabCount));
			}
		}

		private static void Field(StringWriter writer, int val, int tabCount = 1)
		{
			writer.Write(val);
			if (tabCount > 0)
			{
				writer.Write(new string('\t', tabCount));
			}
		}
	}
}
