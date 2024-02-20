using HotaRmgTemplateEditor.Domain.RmgFormat.TownTypeHints;

namespace HotaRmgTemplateEditor.ViewModels
{
	public class AddEditTownTypeHintViewModel : ViewModelBase
	{
		private bool isAllTownsRelatedToZoneHint;
		public bool IsAllTownsRelatedToZoneHint
		{
			get { return isAllTownsRelatedToZoneHint; }
			set
			{
				isAllTownsRelatedToZoneHint = value;
				NotifyPropertyChanged();
				NotifyPropertyChanged(nameof(HintDescription));
				NotifyPropertyChanged(nameof(HintFormula));
			}
		}

		private bool isSameAsZoneHint;
		public bool IsSameAsZoneHint
		{
			get { return isSameAsZoneHint; }
			set
			{
				isSameAsZoneHint = value;
				NotifyPropertyChanged();
				NotifyPropertyChanged(nameof(HintDescription));
				NotifyPropertyChanged(nameof(HintFormula));
			}
		}

		private bool isDifferentFromZoneHint;
		public bool IsDifferentFromZoneHint
		{
			get { return isDifferentFromZoneHint; }
			set
			{
				isDifferentFromZoneHint = value;
				NotifyPropertyChanged();
				NotifyPropertyChanged(nameof(HintDescription));
				NotifyPropertyChanged(nameof(HintFormula));
			}
		}

		private bool isTownDifferentAsTownsInZoneHint;
		public bool IsTownDifferentAsTownsInZoneHint
		{
			get { return isTownDifferentAsTownsInZoneHint; }
			set
			{
				isTownDifferentAsTownsInZoneHint = value;
				NotifyPropertyChanged();
				NotifyPropertyChanged(nameof(HintDescription));
				NotifyPropertyChanged(nameof(HintFormula));
			}
		}

		private bool isTownRelatesToOtherTownInZoneHint;
		public bool IsTownRelatesToOtherTownInZoneHint
		{
			get { return isTownRelatesToOtherTownInZoneHint; }
			set
			{
				isTownRelatesToOtherTownInZoneHint = value;
				NotifyPropertyChanged();
				NotifyPropertyChanged(nameof(HintDescription));
				NotifyPropertyChanged(nameof(HintFormula));
			}
		}

		private int targetZoneId;
		public int TargetZoneId
		{
			get { return targetZoneId; }
			set
			{
				targetZoneId = value;
				NotifyPropertyChanged();
				NotifyPropertyChanged(nameof(HintDescription));
				NotifyPropertyChanged(nameof(HintFormula));
			}
		}

		private bool targetZoneIsCurrentZone;
		public bool TargetZoneIsCurrentZone
		{
			get { return targetZoneIsCurrentZone; }
			set
			{
				targetZoneIsCurrentZone = value;
				NotifyPropertyChanged();
				NotifyPropertyChanged(nameof(HintDescription));
				NotifyPropertyChanged(nameof(HintFormula));
			}
		}

		private int town1Id;
		public int Town1Id
		{
			get { return town1Id; }
			set
			{
				town1Id = value;
				NotifyPropertyChanged();
				NotifyPropertyChanged(nameof(HintDescription));
				NotifyPropertyChanged(nameof(HintFormula));
			}
		}

		private bool town1IsAllPlayerTowns;
		public bool Town1IsAllPlayerTowns
		{
			get { return town1IsAllPlayerTowns; }
			set
			{
				town1IsAllPlayerTowns = value;
				NotifyPropertyChanged();
				NotifyPropertyChanged(nameof(HintDescription));
				NotifyPropertyChanged(nameof(HintFormula));
			}
		}

		private int town2Id;
		public int Town2Id
		{
			get { return town2Id; }
			set
			{
				town2Id = value;
				NotifyPropertyChanged();
				NotifyPropertyChanged(nameof(HintDescription));
				NotifyPropertyChanged(nameof(HintFormula));
			}
		}

		private bool town2IsallPlayerTowns;
		public bool Town2IsAllPlayerTowns
		{
			get { return town2IsallPlayerTowns; }
			set
			{
				town2IsallPlayerTowns = value;
				NotifyPropertyChanged();
				NotifyPropertyChanged(nameof(HintDescription));
				NotifyPropertyChanged(nameof(HintFormula));
			}
		}

		private TownTypeRelation townTypeRelation;
		public TownTypeRelation TownTypeRelation
		{
			get { return townTypeRelation; }
			set
			{
				townTypeRelation = value;
				NotifyPropertyChanged();
				NotifyPropertyChanged(nameof(HintDescription));
				NotifyPropertyChanged(nameof(HintFormula));
			}
		}

		public string HintDescription
		{
			get { return GetDescription(); }
		}

		public string HintFormula
		{
			get { return GetFormula(); }
		}

		public Dictionary<TownTypeRelation, string> TownTypeRelationEnumsWithCaption { get; }

		private TownTypeHint BaseHint { get; }

		public AddEditTownTypeHintViewModel(TownTypeHint baseHint)
		{
			BaseHint = baseHint;

			TownTypeRelationEnumsWithCaption = new Dictionary<TownTypeRelation, string>
			{
				{ TownTypeRelation.SameType, "Same type"},
				{ TownTypeRelation.DifferentType, "Different type"},
			};

			IsAllTownsRelatedToZoneHint = baseHint.HintType == TownTypeHintType.AllTownsRelatedToZone;
			IsSameAsZoneHint = baseHint.HintType == TownTypeHintType.SameAsZone;
			IsDifferentFromZoneHint = baseHint.HintType == TownTypeHintType.DifferentFromZone;
			IsTownDifferentAsTownsInZoneHint = baseHint.HintType == TownTypeHintType.TownDifferentAsTownsInZone;
			IsTownRelatesToOtherTownInZoneHint = baseHint.HintType == TownTypeHintType.TownRelatesToOtherTownInZone;

			TargetZoneId = baseHint.TargetZoneId;
			TargetZoneIsCurrentZone = baseHint.TargetZoneIsCurrentZone;
			Town1Id = baseHint.NeutralTown1Id;
			Town1IsAllPlayerTowns = baseHint.Town1IsAllPlayerTowns;
			Town2Id = baseHint.NeutralTown2Id;
			Town2IsAllPlayerTowns = baseHint.Town2IsAllPlayerTowns;
			TownTypeRelation = baseHint.TownTypeRelation;
		}

		public void SaveChanges()
		{
			BaseHint.HintType = GetHintType();
			BaseHint.TargetZoneId = TargetZoneId;
			BaseHint.TargetZoneIsCurrentZone = TargetZoneIsCurrentZone;
			BaseHint.NeutralTown1Id = Town1Id;
			BaseHint.Town1IsAllPlayerTowns = Town1IsAllPlayerTowns;
			BaseHint.NeutralTown2Id = Town2Id;
			BaseHint.Town2IsAllPlayerTowns = Town2IsAllPlayerTowns;
			BaseHint.TownTypeRelation = TownTypeRelation;
		}

		private TownTypeHintType GetHintType()
		{
			if (IsAllTownsRelatedToZoneHint)
			{
				return TownTypeHintType.AllTownsRelatedToZone;
			}
			else if (IsDifferentFromZoneHint)
			{
				return TownTypeHintType.DifferentFromZone;
			}
			else if (IsSameAsZoneHint)
			{
				return TownTypeHintType.SameAsZone;
			}
			else if (IsTownDifferentAsTownsInZoneHint)
			{
				return TownTypeHintType.TownDifferentAsTownsInZone;
			}
			else if (IsTownRelatesToOtherTownInZoneHint)
			{
				return TownTypeHintType.TownRelatesToOtherTownInZone;
			}
			else
			{
				throw new InvalidOperationException();
			}
		}

		private string GetDescription()
		{
			var hintType = GetHintType();
			return TownTypeHint.GetDescription(hintType, TargetZoneId, TargetZoneIsCurrentZone, Town1Id, Town1IsAllPlayerTowns, Town2Id, Town2IsAllPlayerTowns, TownTypeRelation);
		}

		private string GetFormula()
		{
			var hintType = GetHintType();
			var result = TownTypeHint.GetFormula(hintType, TargetZoneId, TargetZoneIsCurrentZone, Town1Id, Town1IsAllPlayerTowns, Town2Id, Town2IsAllPlayerTowns, TownTypeRelation);
			return result.Replace("_", "__");
		}
	}
}
