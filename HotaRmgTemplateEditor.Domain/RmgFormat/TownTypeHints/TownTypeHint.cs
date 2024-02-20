using System;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace HotaRmgTemplateEditor.Domain.RmgFormat.TownTypeHints
{
	[DebuggerDisplay($"{{{nameof(GetFormula)}(),nq}}")]
	public class TownTypeHint
	{
		public TownTypeHintType HintType { get; set; }

		public int TargetZoneId { get; set; }
		public bool TargetZoneIsCurrentZone { get; set; }
		public int NeutralTown1Id { get; set; }
		public bool Town1IsAllPlayerTowns { get; set; }
		public int NeutralTown2Id { get; set; }
		public bool Town2IsAllPlayerTowns { get; set; }
		public TownTypeRelation TownTypeRelation { get; set; }

		public TownTypeHint()
		{
			HintType = TownTypeHintType.SameAsZone;

			TargetZoneId = 1;
			NeutralTown1Id = 1;
			NeutralTown2Id = 1;
		}

		public string GetFormula()
		{
			return GetFormula(HintType, TargetZoneId, TargetZoneIsCurrentZone, NeutralTown1Id, Town1IsAllPlayerTowns, NeutralTown2Id, Town2IsAllPlayerTowns, TownTypeRelation);
		}

		public string GetDescription()
		{
			return GetDescription(HintType, TargetZoneId, TargetZoneIsCurrentZone, NeutralTown1Id, Town1IsAllPlayerTowns, NeutralTown2Id, Town2IsAllPlayerTowns, TownTypeRelation);
		}

		private static readonly Regex AllTownsRelatedToZone_FormulaRegex = new Regex("^([sd])(x|\\d+)_(p|\\d+)$", RegexOptions.Compiled);
		private static readonly Regex DifferentFromZone_FormulaRegex = new Regex("^d(\\d+)$", RegexOptions.Compiled);
		private static readonly Regex SameAsZone_FormulaRegex = new Regex("^s(\\d+)$", RegexOptions.Compiled);
		private static readonly Regex TownDifferentAsTownsInZone_FormulaRegex = new Regex("^(p|\\d+)d(x|\\d+)$", RegexOptions.Compiled);
		private static readonly Regex TownRelatesToOtherTownInZone_FormulaRegex = new Regex("^(p|\\d+)([sd])(x|\\d+)_(p|\\d+)$", RegexOptions.Compiled);

		public static bool TryParse(string formula, out TownTypeHint? townTypeHint)
		{
			var result = TryParseAllTownsRelatedToZone(formula, out townTypeHint);
			if (!result)
			{
				result = TryParseDifferentFromZone(formula, out townTypeHint);
			}

			if (!result)
			{
				result = TryParseSameAsZone(formula, out townTypeHint);
			}

			if (!result)
			{
				result = TryParseTownDifferentAsTownsInZone(formula, out townTypeHint);
			}

			if (!result)
			{
				result = TryParseTownRelatesToOtherTownInZone(formula, out townTypeHint);
			}

			return result;
		}

		private static bool TryParseAllTownsRelatedToZone(string formula, out TownTypeHint? result)
		{
			var match = AllTownsRelatedToZone_FormulaRegex.Match(formula);
			if (!match.Success)
			{
				result = null;
				return false;
			}

			result = new TownTypeHint();
			result.TownTypeRelation = match.Groups[1].Value switch
			{
				"s" => TownTypeRelation.SameType,
				"d" => TownTypeRelation.DifferentType,
				_ => throw new InvalidOperationException()
			};

			if (match.Groups[2].Value == "x")
			{
				result.TargetZoneIsCurrentZone = true;
			}
			else
			{
				result.TargetZoneId = int.Parse(match.Groups[2].Value);
			}

			if (match.Groups[3].Value == "p")
			{
				result.Town1IsAllPlayerTowns = true;
			}
			else
			{
				result.NeutralTown1Id = int.Parse(match.Groups[3].Value) + 1;
			}

			return true;
		}

		private static bool TryParseDifferentFromZone(string formula, out TownTypeHint? result)
		{
			if (!DifferentFromZone_FormulaRegex.IsMatch(formula))
			{
				result = null;
				return false;
			}

			var zoneId = int.Parse(formula[1..]);
			result = new TownTypeHint
			{
				TargetZoneId = zoneId
			};
			return true;
		}

		private static bool TryParseSameAsZone(string formula, out TownTypeHint? result)
		{
			if (!SameAsZone_FormulaRegex.IsMatch(formula))
			{
				result = null;
				return false;
			}

			var zoneId = int.Parse(formula[1..]);
			result = new TownTypeHint
			{
				TargetZoneId = zoneId
			};
			return true;
		}

		private static bool TryParseTownDifferentAsTownsInZone(string formula, out TownTypeHint? result)
		{
			var match = TownDifferentAsTownsInZone_FormulaRegex.Match(formula);
			if (!match.Success)
			{
				result = null;
				return false;
			}

			result = new TownTypeHint();
			if (match.Groups[1].Value == "p")
			{
				result.Town1IsAllPlayerTowns = true;
			}
			else
			{
				result.NeutralTown1Id = int.Parse(match.Groups[1].Value) + 1;
			}

			if (match.Groups[2].Value == "x")
			{
				result.TargetZoneIsCurrentZone = true;
			}
			else
			{
				result.TargetZoneId = int.Parse(match.Groups[2].Value);
			}

			return true;
		}

		private static bool TryParseTownRelatesToOtherTownInZone(string formula, out TownTypeHint? result)
		{
			var match = TownRelatesToOtherTownInZone_FormulaRegex.Match(formula);
			if (!match.Success)
			{
				result = null;
				return false;
			}

			result = new TownTypeHint();
			if (match.Groups[1].Value == "p")
			{
				result.Town1IsAllPlayerTowns = true;
			}
			else
			{
				result.NeutralTown1Id = int.Parse(match.Groups[1].Value) + 1;
			}

			result.TownTypeRelation = match.Groups[2].Value switch
			{
				"s" => TownTypeRelation.SameType,
				"d" => TownTypeRelation.DifferentType,
				_ => throw new InvalidOperationException()
			};

			if (match.Groups[3].Value == "x")
			{
				result.TargetZoneIsCurrentZone = true;
			}
			else
			{
				result.TargetZoneId = int.Parse(match.Groups[3].Value);
			}

			if (match.Groups[4].Value == "p")
			{
				result.Town2IsAllPlayerTowns = true;
			}
			else
			{
				result.NeutralTown2Id = int.Parse(match.Groups[4].Value) + 1;
			}

			return true;
		}

		public static string GetFormula(TownTypeHintType hintType, int targetZoneId, bool targetZoneIsCurrentZone, int neutralTown1Id, bool town1IsAllPlayerTowns, int neutralTown2Id, bool town2IsAllPlayerTowns, TownTypeRelation relation)
		{
			switch (hintType)
			{
				case TownTypeHintType.AllTownsRelatedToZone:
					{
						var sb = new StringBuilder();
						sb.Append(relation switch
						{
							TownTypeRelation.SameType => 's',
							TownTypeRelation.DifferentType => 'd',
							_ => throw new NotImplementedException()
						});

						if (targetZoneIsCurrentZone)
						{
							sb.Append('x');
						}
						else
						{
							sb.Append(targetZoneId);
						}

						sb.Append('_');

						if (town1IsAllPlayerTowns)
						{
							sb.Append('p');
						}
						else
						{
							sb.Append(neutralTown1Id - 1);
						}
						return sb.ToString();
					}

				case TownTypeHintType.SameAsZone:
					{
						return $"s{targetZoneId}";
					}

				case TownTypeHintType.DifferentFromZone:
					{
						return $"d{targetZoneId}";
					}

				case TownTypeHintType.TownDifferentAsTownsInZone:
					{
						var sb = new StringBuilder();
						if (town1IsAllPlayerTowns)
						{
							sb.Append('p');
						}
						else
						{
							sb.Append(neutralTown1Id - 1);
						}

						sb.Append('d');

						if (targetZoneIsCurrentZone)
						{
							sb.Append('x');
						}
						else
						{
							sb.Append(targetZoneId);
						}
						return sb.ToString();
					}

				case TownTypeHintType.TownRelatesToOtherTownInZone:
					{
						var sb = new StringBuilder();
						if (town1IsAllPlayerTowns)
						{
							sb.Append('p');
						}
						else
						{
							sb.Append(neutralTown1Id - 1);
						}

						sb.Append(relation switch
						{
							TownTypeRelation.SameType => 's',
							TownTypeRelation.DifferentType => 'd',
							_ => throw new NotImplementedException()
						});

						if (targetZoneIsCurrentZone)
						{
							sb.Append('x');
						}
						else
						{
							sb.Append(targetZoneId);
						}

						sb.Append('_');

						if (town2IsAllPlayerTowns)
						{
							sb.Append('p');
						}
						else
						{
							sb.Append(neutralTown2Id - 1);
						}
						return sb.ToString();
					}

				default:
					throw new NotImplementedException();
			}
		}

		public static string GetDescription(TownTypeHintType hintType, int targetZoneId, bool targetZoneIsCurrentZone, int neutralTown1Id, bool town1IsAllPlayerTowns, int neutralTown2Id, bool Town2IsAllPlayerTowns, TownTypeRelation relation)
		{
			switch (hintType)
			{
				case TownTypeHintType.AllTownsRelatedToZone:
					{
						var sb = new StringBuilder();
						sb.Append("All towns have ");
						sb.Append(relation switch
						{
							TownTypeRelation.SameType => "the same type as ",
							TownTypeRelation.DifferentType => "different type than ",
							_ => throw new NotImplementedException()
						});

						if (town1IsAllPlayerTowns)
						{
							sb.Append("player towns ");
						}
						else
						{
							sb.Append($"neutral town {neutralTown1Id} ");
						}

						if (targetZoneIsCurrentZone)
						{
							sb.Append("of current zone.");
						}
						else
						{
							sb.Append($"of zone {targetZoneId}");
						}

						return sb.ToString();
					}

				case TownTypeHintType.SameAsZone:
					{
						return $"Use same type pattern as zone {targetZoneId}";
					}

				case TownTypeHintType.DifferentFromZone:
					{
						return $"No town type in common with zone {targetZoneId}";
					}

				case TownTypeHintType.TownDifferentAsTownsInZone:
					{
						var sb = new StringBuilder();
						if (town1IsAllPlayerTowns)
						{
							sb.Append("Player towns have different type than towns of ");
						}
						else
						{
							sb.Append($"Neutral town {neutralTown1Id} has different type than towns of ");
						}

						if (targetZoneIsCurrentZone)
						{
							sb.Append("current zone");
						}
						else
						{
							sb.Append($"zone {targetZoneId}");
						}
						return sb.ToString();
					};

				case TownTypeHintType.TownRelatesToOtherTownInZone:
					{
						var sb = new StringBuilder();
						if (town1IsAllPlayerTowns)
						{
							sb.Append("Player towns have ");
						}
						else
						{
							sb.Append($"Neutral town {neutralTown1Id} has ");
						}

						sb.Append(relation switch
						{
							TownTypeRelation.SameType => "the same type as ",
							TownTypeRelation.DifferentType => "different type than ",
							_ => throw new NotImplementedException()
						});

						if (Town2IsAllPlayerTowns)
						{
							sb.Append("player towns ");
						}
						else
						{
							sb.Append($"neutral town {neutralTown2Id} ");
						}

						if (targetZoneIsCurrentZone)
						{
							sb.Append("in current zone");
						}
						else
						{
							sb.Append($"of zone {targetZoneId}");
						}
						return sb.ToString();
					}

				default:
					throw new InvalidOperationException();
			}
		}

	}
}
