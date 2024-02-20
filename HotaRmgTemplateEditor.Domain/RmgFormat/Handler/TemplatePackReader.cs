using HotaRmgTemplateEditor.Domain.HotaData;
using HotaRmgTemplateEditor.Domain.RmgFormat.Overrides;
using HotaRmgTemplateEditor.Domain.RmgFormat.TownTypeHints;
using System;

namespace HotaRmgTemplateEditor.Domain.RmgFormat.Handler
{
	public class TemplatePackReader
	{
		private const int TemplatePackColumnCount = 14;
		private const int TemplateColumnCount = 13;
		private const int ZoneColumnCount = 89;

		public static TemplatePack LoadFile(string path)
		{
			if (!File.Exists(path))
			{
				throw new InvalidOperationException($"File doesn't exist: {path}");
			}
			var content = File.ReadAllText(path);

			var lines = content.Split("\r\n");
			if (lines.Length < 4)
			{
				throw new Exception($"Template pack only has {lines.Length} line(s).");
			}

			if (!lines[0].StartsWith("Pack\t")
				|| !lines[1].StartsWith("Field count\t")
				|| !lines[2].StartsWith("Town\t"))
			{
				throw new Exception("First 3 lines of the template pack starts in an unexpected way.");
			}

			var result = new TemplatePack();
			Template? currentTemplate = null;
			for (int i = 3; i < lines.Length; ++i)
			{
				var line = lines[i];

				if (string.IsNullOrWhiteSpace(line))
				{
					continue;
				}

				var columns = line.Split('\t');

				int ix = 0;
				if (!string.IsNullOrEmpty(columns[ix]))
				{
					result.FieldCount.Town = Number(columns[ix++]);
					result.FieldCount.Terrain = Number(columns[ix++]);
					result.FieldCount.ZoneType = Number(columns[ix++]);
					result.FieldCount.PackNew = Number(columns[ix++]);
					result.FieldCount.MapNew = Number(columns[ix++]);
					result.FieldCount.ZoneNew = Number(columns[ix++]);
					result.FieldCount.ConnectionNew = Number(columns[ix++]);

					result.Options.Name = columns[ix++];
					result.Options.Description = Multiline(columns[ix++]);
					result.Options.TownSelection = TownOverrideDict(columns[ix++]);
					result.Options.HeroOverrides = HeroOverrideDict(columns[ix++]);
					result.Options.Mirror = Bool(columns[ix++]);
					result.Options.Tags = columns[ix++];

					var maxBattleRounds = columns[ix++];
					result.Options.MaxBattleRoundsOverridden = !string.IsNullOrWhiteSpace(maxBattleRounds);
					result.Options.MaxBattleRounds = Number(maxBattleRounds);
				}
				else
				{
					ix += TemplatePackColumnCount;
				}

				if (!string.IsNullOrEmpty(columns[ix]))
				{
					currentTemplate = new Template(result);
					result.Templates.Add(currentTemplate);

					currentTemplate.Name = columns[ix++];
					currentTemplate.MinimumSize = (MapSize)Number(columns[ix++]);
					currentTemplate.MaximumSize = (MapSize)Number(columns[ix++]);
					currentTemplate.ArtifactOverrides = ArtifactOverrideDict(columns[ix++]);
					currentTemplate.ComboArtifacts = columns[ix++];
					currentTemplate.SpellOverrides = SpellOverrideDict(columns[ix++]);
					currentTemplate.SecondarySkillOverrides = SecondarySkillOverrideDict(columns[ix++]);
					currentTemplate.ObjectOverrides = ObjectOverrideList(columns[ix++]);
					var blockRadius = columns[ix++];
					if (blockRadius == "x")
					{
						currentTemplate.RockBlockRadius.Setting = EnableDisableCustom.Enable;
					}
					else if (string.IsNullOrWhiteSpace(blockRadius))
					{
						currentTemplate.RockBlockRadius.Setting = EnableDisableCustom.Disable;
					}
					else
					{
						currentTemplate.RockBlockRadius.Setting = EnableDisableCustom.Custom;
						currentTemplate.RockBlockRadius.CustomValue = Float(blockRadius);
					}

					var zoneSparseness = columns[ix++];
					if (string.IsNullOrWhiteSpace(zoneSparseness))
					{
						currentTemplate.ZoneSparseness.Default = true;
					}
					else
					{
						currentTemplate.ZoneSparseness.Default = false;
						currentTemplate.ZoneSparseness.CustomValue = Float(zoneSparseness);
					}
					currentTemplate.SpecialWeeksDisabled = Bool(columns[ix++]);
					currentTemplate.SpellResearch = Bool(columns[ix++]);
					currentTemplate.Anarchy = Bool(columns[ix++]);
				}
				else
				{
					ix += TemplateColumnCount;
				}

				if (!string.IsNullOrEmpty(columns[ix]))
				{
					var zone = new Zone();
					currentTemplate?.Zones.Add(zone);
					zone.Id = Number(columns[ix++]);
					if (Bool(columns[ix++]))
					{
						zone.Type = ZoneType.HumanStart;
					}
					if (Bool(columns[ix++]))
					{
						zone.Type = ZoneType.ComputerStart;
					}
					if (Bool(columns[ix++]))
					{
						zone.Type = ZoneType.Treasure;
					}
					if (Bool(columns[ix++]))
					{
						zone.Type = ZoneType.Junction;
					}
					zone.Size = Number(columns[ix++]);

					zone.Restrictions.MinimumHumanPlayers = Number(columns[ix++]);
					zone.Restrictions.MaximumHumanPlayers = Number(columns[ix++]);
					zone.Restrictions.MinimumTotalPlayers = Number(columns[ix++]);
					zone.Restrictions.MaximumTotalPlayers = Number(columns[ix++]);

					zone.PlayerTowns.Ownership = Number(columns[ix++]);
					zone.PlayerTowns.MinimumTowns = Number(columns[ix++]);
					zone.PlayerTowns.MinimumCastles = Number(columns[ix++]);
					zone.PlayerTowns.TownDensity = Number(columns[ix++]);
					zone.PlayerTowns.CastleDensity = Number(columns[ix++]);

					zone.NeutralTowns.MinimumTowns = Number(columns[ix++]);
					zone.NeutralTowns.MinimumCastles = Number(columns[ix++]);
					zone.NeutralTowns.TownDensity = Number(columns[ix++]);
					zone.NeutralTowns.CastleDensity = Number(columns[ix++]);
					zone.NeutralTowns.TownsAreOfSameType = Bool(columns[ix++]);

					zone.AllowedTowns.Castle = Bool(columns[ix++]);
					zone.AllowedTowns.Rampart = Bool(columns[ix++]);
					zone.AllowedTowns.Tower = Bool(columns[ix++]);
					zone.AllowedTowns.Inferno = Bool(columns[ix++]);
					zone.AllowedTowns.Necropolis = Bool(columns[ix++]);
					zone.AllowedTowns.Dungeon = Bool(columns[ix++]);
					zone.AllowedTowns.Stronghold = Bool(columns[ix++]);
					zone.AllowedTowns.Fortress = Bool(columns[ix++]);
					zone.AllowedTowns.Conflux = Bool(columns[ix++]);
					zone.AllowedTowns.Cove = Bool(columns[ix++]);

					zone.Mines.WoodMinimum = Number(columns[ix++]);
					zone.Mines.MercuryMinimum = Number(columns[ix++]);
					zone.Mines.OreMinimum = Number(columns[ix++]);
					zone.Mines.SulfurMinimum = Number(columns[ix++]);
					zone.Mines.CrystalMinimum = Number(columns[ix++]);
					zone.Mines.GemsMinimum = Number(columns[ix++]);
					zone.Mines.GoldMinimum = Number(columns[ix++]);

					zone.Mines.WoodDensity = Number(columns[ix++]);
					zone.Mines.MercuryDensity = Number(columns[ix++]);
					zone.Mines.OreDensity = Number(columns[ix++]);
					zone.Mines.SulfurDensity = Number(columns[ix++]);
					zone.Mines.CrystalDensity = Number(columns[ix++]);
					zone.Mines.GemsDensity = Number(columns[ix++]);
					zone.Mines.GoldDensity = Number(columns[ix++]);

					zone.Terrain.MatchToTown = Bool(columns[ix++]);
					zone.Terrain.Dirt = Bool(columns[ix++]);
					zone.Terrain.Sand = Bool(columns[ix++]);
					zone.Terrain.Grass = Bool(columns[ix++]);
					zone.Terrain.Snow = Bool(columns[ix++]);
					zone.Terrain.Swamp = Bool(columns[ix++]);
					zone.Terrain.Rough = Bool(columns[ix++]);
					zone.Terrain.Subterranean = Bool(columns[ix++]);
					zone.Terrain.Lava = Bool(columns[ix++]);
					zone.Terrain.Highlands = Bool(columns[ix++]);
					zone.Terrain.Wasteland = Bool(columns[ix++]);

					zone.Monsters.Strength = columns[ix++] switch
					{
						"none" => ZoneMonsterStrength.None,
						"weak" => ZoneMonsterStrength.Weak,
						"avg" => ZoneMonsterStrength.Average,
						"strong" => ZoneMonsterStrength.Strong,
						_ => throw new Exception("Monster strength not handled")
					};

					zone.Monsters.MatchToTown = Bool(columns[ix++]);
					zone.Monsters.Neutral = Bool(columns[ix++]);
					zone.Monsters.Castle = Bool(columns[ix++]);
					zone.Monsters.Rampart = Bool(columns[ix++]);
					zone.Monsters.Tower = Bool(columns[ix++]);
					zone.Monsters.Inferno = Bool(columns[ix++]);
					zone.Monsters.Necropolis = Bool(columns[ix++]);
					zone.Monsters.Dungeon = Bool(columns[ix++]);
					zone.Monsters.Stronghold = Bool(columns[ix++]);
					zone.Monsters.Fortress = Bool(columns[ix++]);
					zone.Monsters.Conflux = Bool(columns[ix++]);
					zone.Monsters.Cove = Bool(columns[ix++]);

					zone.Treasures.Range1.Low = Number(columns[ix++]);
					zone.Treasures.Range1.High = Number(columns[ix++]);
					zone.Treasures.Range1.Density = Number(columns[ix++]);
					zone.Treasures.Range1.Enabled = zone.Treasures.Range1.Low != default && zone.Treasures.Range1.High != default;

					zone.Treasures.Range2.Low = Number(columns[ix++]);
					zone.Treasures.Range2.High = Number(columns[ix++]);
					zone.Treasures.Range2.Density = Number(columns[ix++]);
					zone.Treasures.Range2.Enabled = zone.Treasures.Range2.Low != default && zone.Treasures.Range2.High != default;

					zone.Treasures.Range3.Low = Number(columns[ix++]);
					zone.Treasures.Range3.High = Number(columns[ix++]);
					zone.Treasures.Range3.Density = Number(columns[ix++]);
					zone.Treasures.Range3.Enabled = zone.Treasures.Range3.Low != default && zone.Treasures.Range3.High != default;

					var placement = columns[ix++];
					zone.Placement = string.IsNullOrWhiteSpace(placement) ? ZonePlacement.Free : Enum.Parse<ZonePlacement>(placement, true);
					zone.ObjectOverrides = ObjectOverrideList(columns[ix++]);
					zone.MinimumObjects = columns[ix++];
					zone.ImageSettings = Coordinate(columns[ix++]);

					zone.Treasures.ForceNeutralCreatures = Bool(columns[ix++]);
					zone.AllowNonCoherentRoad = Bool(columns[ix++]);
					zone.ZoneRepulsion = Bool(columns[ix++]);
					zone.TownHints = TownTypeHintList(columns[ix++]);
					var stdDisposition = Number(columns[ix++]);
					var custDisposition = Number(columns[ix++]);
					if (0 <= stdDisposition && stdDisposition <= 4)
					{
						zone.Monsters.Disposition = stdDisposition switch
						{
							0 => ZoneMonsterDisposition.AlwaysJoin,
							1 => ZoneMonsterDisposition.Friendly,
							2 => ZoneMonsterDisposition.Aggressive,
							3 => ZoneMonsterDisposition.Hostile,
							4 => ZoneMonsterDisposition.Savage,
							_ => throw new InvalidOperationException()
						};
					}
					else if (stdDisposition == 5)
					{
						zone.Monsters.Disposition = custDisposition switch
						{
							1 => ZoneMonsterDisposition.Custom1,
							2 => ZoneMonsterDisposition.Custom2,
							3 => ZoneMonsterDisposition.Custom3,
							4 => ZoneMonsterDisposition.Custom4,
							5 => ZoneMonsterDisposition.Custom5,
							6 => ZoneMonsterDisposition.Custom6,
							7 => ZoneMonsterDisposition.Custom7,
							8 => ZoneMonsterDisposition.Custom8,
							9 => ZoneMonsterDisposition.Custom9,
							_ => throw new InvalidOperationException()
						};
					}
					else
					{
						throw new InvalidOperationException();
					}

					zone.Monsters.JoinPercent = Number(columns[ix++]) switch
					{
						0 => ZoneMonsterJoinPercent.Percent25,
						1 => ZoneMonsterJoinPercent.Percent50,
						2 => ZoneMonsterJoinPercent.Percent75,
						3 => ZoneMonsterJoinPercent.Percent100,
						_ => throw new InvalidOperationException()
					};
					zone.Monsters.JoinOnlyForMoney = Bool(columns[ix++]);
				}
				else
				{
					ix += ZoneColumnCount;
				}

				if (!string.IsNullOrEmpty(columns[ix]))
				{
					var zone1Id = Number(columns[ix++]);
					var zone2Id = Number(columns[ix++]);

					var conn = currentTemplate?.Connections.FirstOrDefault(c => c.Zone1Id == zone1Id && c.Zone2Id == zone2Id);
					if (conn == null)
					{
						conn = new Connection
						{
							Zone1Id = zone1Id,
							Zone2Id = zone2Id
						};
						currentTemplate?.Connections.Add(conn);

						if (conn.Zone2Id < 0 && !result.Options.Mirror)
						{
							result.Options.Mirror = true;
						}
					}

					var link = new ConnectionLink();
					conn.Links.Add(link);
					link.Value = Number(columns[ix++]);

					var wide = Bool(columns[ix++]);
					link.BorderGuard = BorderGuard(columns[ix++]);

					link.Roads = columns[ix++] switch
					{
						"" => ConnectionRoad.Random,
						"+" => ConnectionRoad.Yes,
						"-" => ConnectionRoad.No,
						_ => throw new InvalidOperationException()
					};

					link.PlacementHint = columns[ix++] switch
					{
						"" => ConnectionPlacementHint.Default,
						"ground" => ConnectionPlacementHint.Ground,
						"underground" => ConnectionPlacementHint.UndergroundGate,
						"teleport" => ConnectionPlacementHint.Monolith,
						"random" => ConnectionPlacementHint.Random,
						_ => throw new InvalidOperationException()
					};

					var fictive = Bool(columns[ix++]);

					link.PortalRepulsion = Bool(columns[ix++]);

					if (wide)
					{
						link.Type = ConnectionType.Wide;
					}
					else if (link.BorderGuard != null)
					{
						link.Type = ConnectionType.BorderGuard;
					}
					else if (fictive)
					{
						link.Type = ConnectionType.Fictive;
					}
					else
					{
						link.Type = ConnectionType.Standard;
					}

					link.Restriction.MinimumHumanPlayers = Number(columns[ix++]);
					link.Restriction.MaximumHumanPlayers = Number(columns[ix++]);
					link.Restriction.MinimumTotalPlayers = Number(columns[ix++]);
					link.Restriction.MaximumTotalPlayers = Number(columns[ix++]);
				}
			}

			return result;
		}
		private static BorderGuard? BorderGuard(string value)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				return null;
			}

			var tokens = value.Split(' ');
			if (tokens[0] == "x")
			{
				return new BorderGuard(tokens[1..]);
			}
			else
			{
				throw new NotImplementedException();
			}
		}

		private static int Number(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return default;
			}

			return int.Parse(value);
		}

		private static float Float(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return default;
			}

			return float.Parse(value, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
		}

		private static bool Bool(string value)
		{
			return value == "x";
		}

		private static string Multiline(string value)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				return value;
			}

			if (value[0] == value[^1] && value[0] == '"')
			{
				value = value[1..^1];
			}

			value = value.Replace("\n", "\r\n");
			return value;
		}

		private static ZoneDisplayCoordinate Coordinate(string value)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				return new ZoneDisplayCoordinate(0, 0);
			}

			var tokens = value.Split(' ');
			if (tokens.Length != 2)
			{
				throw new InvalidOperationException($"Invalid coordinate value: '{value}'");
			}

			return new ZoneDisplayCoordinate(Float(tokens[0]), Float(tokens[1]));
		}

		private static Dictionary<int, HeroOverride> HeroOverrideDict(string value)
		{
			var result = new Dictionary<int, HeroOverride>();
			var optionList = GetOverrideList(value);

			foreach (var option in optionList)
			{
				int id = int.Parse(option.Tokens[0]);
				result.Add(id, new HeroOverride(id, option.EnableDisable));
			}

			foreach (var hero in Heroes.All)
			{
				if (!result.ContainsKey(hero.Id))
				{
					result.Add(hero.Id, new HeroOverride(hero, EnableDisableDefault.Default));
				}
			}

			return result;
		}

		private static Dictionary<Town, TownOverride> TownOverrideDict(string value)
		{
			var result = new Dictionary<Town, TownOverride>();

			var optionList = GetOverrideList(value);
			foreach (var option in optionList)
			{
				var id = int.Parse(option.Tokens[0]);
				var town = (Town)id;
				result.Add(town, new TownOverride(town, option.EnableDisable != EnableDisableDefault.Disable));
			}

			foreach (var town in Enum.GetValues<Town>())
			{
				if (!result.ContainsKey(town))
				{
					result.Add(town, new TownOverride(town, true));
				}
			}

			return result;
		}

		private static List<TownTypeHint> TownTypeHintList(string value)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				return [];
			}

			var result = new List<TownTypeHint>();
			var tokens = value.Split(' ', StringSplitOptions.RemoveEmptyEntries);
			foreach (var token in tokens)
			{
				if (TownTypeHint.TryParse(token, out var hint))
				{
					result.Add(hint);
				}
				else
				{
					throw new Exception($"Unrecognized town type hint: {token}");
				}
			}
			return result;
		}

		private static List<GameObjectOverride> ObjectOverrideList(string value)
		{
			var result = new List<GameObjectOverride>();
			var optionList = GetOverrideList(value);
			foreach (var option in optionList)
			{
				var overrideObj = new GameObjectOverride
				{
					EnableDisable = option.EnableDisable
				};

				if (option.EnableDisable == EnableDisableDefault.Disable)
				{
					overrideObj.ObjectReference = option.Tokens.ToArray();
					overrideObj.GameObject = FindObject(overrideObj.ObjectReference);
				}
				else
				{
					var maxPerZone = option.Tokens[^1];
					if (maxPerZone == "d")
					{
						overrideObj.MaxPerZone = AmountRestriction.Default;
					}
					else if (maxPerZone == "n")
					{
						overrideObj.MaxPerZone = AmountRestriction.NoLimit;
					}
					else if (int.TryParse(maxPerZone, out var maxPerZoneAmnt))
					{
						overrideObj.MaxPerZone = AmountRestriction.Custom;
						overrideObj.MaxPerZoneAmount = maxPerZoneAmnt;
					}
					else
					{
						throw new NotImplementedException($"Object Override zone amount not handled: '{maxPerZone}'");
					}

					var maxPerMap = option.Tokens[^2];
					if (maxPerMap == "d")
					{
						overrideObj.MaxOnMap = AmountRestriction.Default;
					}
					else if (maxPerMap == "n")
					{
						overrideObj.MaxOnMap = AmountRestriction.NoLimit;
					}
					else if (int.TryParse(maxPerMap, out var maxPerMapAmnt))
					{
						overrideObj.MaxOnMap = AmountRestriction.Custom;
						overrideObj.MaxOnMapAmount = maxPerMapAmnt;
					}
					else
					{
						throw new NotImplementedException($"Object Override map amount not handled: '{maxPerMap}'");
					}

					var freq = option.Tokens[^3];
					if (freq == "d")
					{
						overrideObj.CustomFrequency = null;
					}
					else if (int.TryParse(freq, out var customFreq))
					{
						overrideObj.CustomFrequency = customFreq;
					}
					else
					{
						throw new NotImplementedException($"Object Override frequency not handled: '{freq}'");
					}

					var val = option.Tokens[^4];
					if (val == "d")
					{
						overrideObj.CustomValue = null;
					}
					else if (int.TryParse(val, out var customVal))
					{
						overrideObj.CustomValue = customVal;
					}
					else
					{
						throw new NotImplementedException($"Object Override value not handled: '{freq}'");
					}

					overrideObj.ObjectReference = option.Tokens[0..^4];
					overrideObj.GameObject = FindObject(overrideObj.ObjectReference);
				}

				result.Add(overrideObj);
			}
			return result;
		}

		private static GameObject FindObject(IReadOnlyList<string> reference)
		{
			var refKey = string.Join(' ', reference);
			return GameObjects.Lookup[refKey];
		}

		private static Dictionary<int, ArtifactOverride> ArtifactOverrideDict(string value)
		{
			var result = new Dictionary<int, ArtifactOverride>();
			var optionList = GetOverrideList(value);

			foreach (var option in optionList)
			{
				int id = int.Parse(option.Tokens[0]);
				result.Add(id, new ArtifactOverride(id, option.EnableDisable));
			}

			foreach (var artifact in Artifacts.All)
			{
				if (!result.ContainsKey(artifact.Id))
				{
					result.Add(artifact.Id, new ArtifactOverride(artifact, EnableDisableDefault.Default));
				}
			}

			return result;
		}

		private static Dictionary<int, SecondarySkillOverride> SecondarySkillOverrideDict(string value)
		{
			var result = new Dictionary<int, SecondarySkillOverride>();
			var optionList = GetOverrideList(value);

			foreach (var option in optionList)
			{
				int id = int.Parse(option.Tokens[0]);
				result.Add(id, new SecondarySkillOverride(id, option.EnableDisable));
			}

			foreach (var secondarySkill in SecondarySkills.All)
			{
				if (!result.ContainsKey(secondarySkill.Id))
				{
					result.Add(secondarySkill.Id, new SecondarySkillOverride(secondarySkill, EnableDisableDefault.Default));
				}
			}

			return result;
		}

		private static Dictionary<int, SpellOverride> SpellOverrideDict(string value)
		{
			var result = new Dictionary<int, SpellOverride>();
			var optionList = GetOverrideList(value);

			foreach (var option in optionList)
			{
				int id = int.Parse(option.Tokens[0]);
				result.Add(id, new SpellOverride(id, option.EnableDisable));
			}

			foreach (var spell in Spells.All)
			{
				if (!result.ContainsKey(spell.Id))
				{
					result.Add(spell.Id, new SpellOverride(spell, EnableDisableDefault.Default));
				}
			}

			return result;
		}

		private static List<GenericOverrideOption> GetOverrideList(string value)
		{
			var result = new List<GenericOverrideOption>();

			var tokens = value.Split(" ", StringSplitOptions.RemoveEmptyEntries);

			GenericOverrideOption? option = null;
			for (int i = 0; i < tokens.Length; i++)
			{
				var token = tokens[i];
				switch (token[0])
				{
					case '-':
						option = new GenericOverrideOption();
						result.Add(option);

						option.EnableDisable = EnableDisableDefault.Disable;
						option.Tokens.Add(token[1..]);
						break;

					case '+':
						option = new GenericOverrideOption();
						result.Add(option);

						option.EnableDisable = EnableDisableDefault.Enable;
						option.Tokens.Add(token[1..]);
						break;

					default:
						if (option == null)
						{
							throw new InvalidOperationException();
						}
						option.Tokens.Add(token);
						break;
				}
			}

			return result;
		}
	}
}
