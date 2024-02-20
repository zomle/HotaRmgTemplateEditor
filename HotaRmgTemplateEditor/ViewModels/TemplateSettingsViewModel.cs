using HotaRmgTemplateEditor.Domain.RmgFormat;
using HotaRmgTemplateEditor.Domain.RmgFormat.Overrides;
using HotaRmgTemplateEditor.Helpers;
using System.Collections.ObjectModel;

namespace HotaRmgTemplateEditor.ViewModels
{
    public enum RockBlockRadius
	{
		Enable,
		Disable,
		Custom
	}

	public enum ZoneSparseness
	{
		Default,
		Custom
	}

	public class TemplateSettingsViewModel : ViewModelBase
	{
		private string name;
		public string Name
		{
			get { return name; }
			set { name = value; NotifyPropertyChanged(); }
		}

		private MapSize minimumSize;
		public MapSize MinimumSize
		{
			get { return minimumSize; }
			set { minimumSize = value; NotifyPropertyChanged(); }
		}

		private MapSize maximumSize;
		public MapSize MaximumSize
		{
			get { return maximumSize; }
			set { maximumSize = value; NotifyPropertyChanged(); }
		}

		private bool spellResearch;
		public bool SpellResearch
		{
			get { return spellResearch; }
			set { spellResearch = value; NotifyPropertyChanged(); }
		}

		private bool disableSpecialWeeks;
		public bool DisableSpecialWeeks
		{
			get { return disableSpecialWeeks; }
			set { disableSpecialWeeks = value; NotifyPropertyChanged(); }
		}

		private bool useStandardPlayerSettings;
		public bool UseStandardPlayerSettings
		{
			get { return useStandardPlayerSettings; }
			set { useStandardPlayerSettings = value; NotifyPropertyChanged(); }
		}

		private int minimumHumanPositions;
		public int MinimumHumanPositions
		{
			get { return minimumHumanPositions; }
			set { minimumHumanPositions = value; NotifyPropertyChanged(); }
		}

		private int maximumHumanPositions;
		public int MaximumHumanPositions
		{
			get { return maximumHumanPositions; }
			set { maximumHumanPositions = value; NotifyPropertyChanged(); }
		}

		private int minimumTotalPositions;
		public int MinimumTotalPositions
		{
			get { return minimumTotalPositions; }
			set { minimumTotalPositions = value; NotifyPropertyChanged(); }
		}

		private int maximumTotalPositions;
		public int MaximumTotalPositions
		{
			get { return maximumTotalPositions; }
			set { maximumTotalPositions = value; NotifyPropertyChanged(); }
		}

		private RockBlockRadius rockBlockRadius;
		public RockBlockRadius RockBlockRadius
		{
			get { return rockBlockRadius; }
			set { rockBlockRadius = value; NotifyPropertyChanged(); }
		}

		private double rockBlockRadiusValue;
		public double RockBlockRadiusValue
		{
			get { return rockBlockRadiusValue; }
			set { rockBlockRadiusValue = value; NotifyPropertyChanged(); }
		}

		private ZoneSparseness zoneSparseness;
		public ZoneSparseness ZoneSparseness
		{
			get { return zoneSparseness; }
			set { zoneSparseness = value; NotifyPropertyChanged(); }
		}

		private double zoneSparsenessValue;
		public double ZoneSparsenessValue
		{
			get { return zoneSparsenessValue; }
			set { zoneSparsenessValue = value; NotifyPropertyChanged(); }
		}

		private bool anarchy;
		public bool Anarchy
		{
			get { return anarchy; }
			set { anarchy = value; }
		}

		public ObservableCollection<EnableDisableItemViewModel> DisabledArtifacts { get; }
		public ObservableCollection<EnableDisableItemViewModel> DefaultArtifacts { get; }
		public ObservableCollection<EnableDisableItemViewModel> EnabledArtifacts { get; }

		public ObservableCollection<EnableDisableItemViewModel> DisabledSpells { get; }
		public ObservableCollection<EnableDisableItemViewModel> DefaultSpells { get; }
		public ObservableCollection<EnableDisableItemViewModel> EnabledSpells { get; }

		public ObservableCollection<EnableDisableItemViewModel> DisabledSecondarySkills { get; }
		public ObservableCollection<EnableDisableItemViewModel> DefaultSecondarySkills { get; }
		public ObservableCollection<EnableDisableItemViewModel> EnabledSecondarySkills { get; }

		public ObjectRulesViewModel ObjectRules { get; }

		public Dictionary<MapSize, string> MapSizeEnumsWithCaption { get; private set; }
		public Dictionary<RockBlockRadius, string> RockBlockRadiusEnumsWithCaption { get; private set; }
		public Dictionary<ZoneSparseness, string> ZoneSparsenessEnumsWithCaption { get; private set; }

		private void SetupHelpers()
		{
			MapSizeEnumsWithCaption = new Dictionary<MapSize, string>
			{
				{ MapSize.Small_WoU, $"{(int)MapSize.Small_WoU} - 36x36 (S) without underground" },
				{ MapSize.Small_W,   $"{(int)MapSize.Small_W} - 36x36 (S) with underground" },
				{ MapSize.Medium_WoU, $"{(int)MapSize.Medium_WoU} - 72x72 (M) without underground" },
				{ MapSize.Medium_WU,  $"{(int)MapSize.Medium_WU} - 72x72 (M) with underground" },
				{ MapSize.Large_WoU, $"{(int)MapSize.Large_WoU} - 108x108 (L) without underground" },
				{ MapSize.Large_WU,  $"{(int)MapSize.Large_WU} - 108x108 (L) with underground" },
				{ MapSize.ExtraLarge_WoU, $"{(int)MapSize.ExtraLarge_WoU} - 144x144 (XL) without underground" },
				{ MapSize.ExtraLarge_WU,  $"{(int)MapSize.ExtraLarge_WU} - 144x144 (XL) with underground" },
				{ MapSize.Huge_WoU, $"{(int)MapSize.Huge_WoU} - 180x180 (H) without underground" },
				{ MapSize.Huge_WU,  $"{(int)MapSize.Huge_WU} - 180x180 (H) with underground" },
				{ MapSize.ExtraHuge_WoU, $"{(int)MapSize.ExtraHuge_WoU} - 216x216 (XH) without underground" },
				{ MapSize.ExtraHuge_WU,  $"{(int)MapSize.ExtraHuge_WU} - 216x216 (XH) with underground" },
				{ MapSize.Giant_WoU, $"{(int)MapSize.Giant_WoU} - 252x252 (G) without underground" },
				{ MapSize.Giant_WU,  $"{(int)MapSize.Giant_WU} - 252x252 (G) with underground" },
			};

			RockBlockRadiusEnumsWithCaption = new Dictionary<RockBlockRadius, string>
			{
				{ RockBlockRadius.Enable, "Enable" },
				{ RockBlockRadius.Disable, "Disable" },
				{ RockBlockRadius.Custom, "Custom" }
			};

			ZoneSparsenessEnumsWithCaption = new Dictionary<ZoneSparseness, string>
			{
				{ ZoneSparseness.Default, "Default" },
				{ ZoneSparseness.Custom, "Custom" }
			};
		}

		private TemplatePack BaseTemplatePack { get; }
		private Template BaseTemplate { get; }

		public TemplateSettingsViewModel(TemplatePack baseTemplatePack, Template baseTemplate)
		{
			BaseTemplatePack = baseTemplatePack;
			BaseTemplate = baseTemplate;

			DisabledArtifacts = [];
			DefaultArtifacts = [];
			EnabledArtifacts = [];

			DisabledSpells = [];
			DefaultSpells = [];
			EnabledSpells = [];

			DisabledSecondarySkills = [];
			DefaultSecondarySkills = [];
			EnabledSecondarySkills = [];

			MapSizeEnumsWithCaption = [];
			RockBlockRadiusEnumsWithCaption = [];
			ZoneSparsenessEnumsWithCaption = [];

			SetupHelpers();

			foreach (var itemOverride in BaseTemplate.ArtifactOverrides.Values.OrderBy(a => a.Artifact.Name))
			{
				var list = itemOverride.EnableDisable switch
				{
					EnableDisableDefault.Default => DefaultArtifacts,
					EnableDisableDefault.Enable => EnabledArtifacts,
					EnableDisableDefault.Disable => DisabledArtifacts,
					_ => throw new InvalidOperationException()
				};
				var vm = new ArtifactViewModel(itemOverride.Artifact);
				list.Add(vm);
			}

			foreach (var itemOverride in BaseTemplate.SpellOverrides.Values.OrderBy(a => a.Spell.Name))
			{
				var list = itemOverride.EnableDisable switch
				{
					EnableDisableDefault.Default => DefaultSpells,
					EnableDisableDefault.Enable => EnabledSpells,
					EnableDisableDefault.Disable => DisabledSpells,
					_ => throw new InvalidOperationException()
				};
				var vm = new SpellViewModel(itemOverride.Spell);
				list.Add(vm);
			}

			foreach (var itemOverride in BaseTemplate.SecondarySkillOverrides.Values.OrderBy(a => a.SecondarySkill.Name))
			{
				var list = itemOverride.EnableDisable switch
				{
					EnableDisableDefault.Default => DefaultSecondarySkills,
					EnableDisableDefault.Enable => EnabledSecondarySkills,
					EnableDisableDefault.Disable => DisabledSecondarySkills,
					_ => throw new InvalidOperationException()
				};
				var vm = new SecondarySkillViewModel(itemOverride.SecondarySkill);
				list.Add(vm);
			}

			ObjectRules = new ObjectRulesViewModel() { ShowMaxOnMap = true };
			if (BaseTemplate != null)
			{
				foreach (var objOverride in BaseTemplate.ObjectOverrides)
				{
					var oovm = new ObjectRuleItemViewModel(objOverride);
					ObjectRules.RuleItems.Add(oovm);
				}
			}

			Name = BaseTemplate?.Name ?? "untitled";
			MinimumSize = BaseTemplate?.MinimumSize ?? MapSize.Small_WoU;
			MaximumSize = BaseTemplate?.MaximumSize ?? MapSize.ExtraLarge_WU;
			SpellResearch = BaseTemplate?.SpellResearch ?? true;
			DisableSpecialWeeks = BaseTemplate?.SpecialWeeksDisabled ?? false;
			//MinimumHumanPositions =
			//MaximumHumanPositions =
			//MinimumTotalPositions =
			//MaximumTotalPositions =
			//RockBlockRadius = BaseTemplate.
			//ZoneSparseness
			Anarchy = BaseTemplate?.Anarchy ?? false;
		}

		public void SaveChanges()
		{
			BaseTemplate.Name = Name;
			BaseTemplate.MinimumSize = MinimumSize;
			BaseTemplate.MaximumSize = MaximumSize;
			BaseTemplate.SpellResearch = SpellResearch;
			BaseTemplate.SpecialWeeksDisabled = DisableSpecialWeeks;
			BaseTemplate.Anarchy = Anarchy;

			foreach (var item in DisabledArtifacts.Cast<ArtifactViewModel>())
			{
				BaseTemplate.ArtifactOverrides[item.BaseArtifact.Id].EnableDisable = EnableDisableDefault.Disable;
			}
			foreach (var item in DefaultArtifacts.Cast<ArtifactViewModel>())
			{
				BaseTemplate.ArtifactOverrides[item.BaseArtifact.Id].EnableDisable = EnableDisableDefault.Default;
			}
			foreach (var item in EnabledArtifacts.Cast<ArtifactViewModel>())
			{
				BaseTemplate.ArtifactOverrides[item.BaseArtifact.Id].EnableDisable = EnableDisableDefault.Enable;
			}

			foreach (var item in DisabledSpells.Cast<SpellViewModel>())
			{
				BaseTemplate.SpellOverrides[item.BaseSpell.Id].EnableDisable = EnableDisableDefault.Disable;
			}
			foreach (var item in DefaultSpells.Cast<SpellViewModel>())
			{
				BaseTemplate.SpellOverrides[item.BaseSpell.Id].EnableDisable = EnableDisableDefault.Default;
			}
			foreach (var item in EnabledSpells.Cast<SpellViewModel>())
			{
				BaseTemplate.SpellOverrides[item.BaseSpell.Id].EnableDisable = EnableDisableDefault.Enable;
			}

			foreach (var item in DisabledSecondarySkills.Cast<SecondarySkillViewModel>())
			{
				BaseTemplate.SecondarySkillOverrides[item.BaseSecondarySkill.Id].EnableDisable = EnableDisableDefault.Disable;
			}
			foreach (var item in DefaultSecondarySkills.Cast<SecondarySkillViewModel>())
			{
				BaseTemplate.SecondarySkillOverrides[item.BaseSecondarySkill.Id].EnableDisable = EnableDisableDefault.Default;
			}
			foreach (var item in EnabledSecondarySkills.Cast<SecondarySkillViewModel>())
			{
				BaseTemplate.SecondarySkillOverrides[item.BaseSecondarySkill.Id].EnableDisable = EnableDisableDefault.Enable;
			}
		}

		public Template GetBaseTemplate()
		{
			return BaseTemplate;
		}
	}
}
