using HotaRmgTemplateEditor.Dialogs;
using HotaRmgTemplateEditor.Domain.HotaData;
using HotaRmgTemplateEditor.Domain.RmgFormat;
using HotaRmgTemplateEditor.Domain.RmgFormat.Overrides;
using HotaRmgTemplateEditor.Helpers;
using System.Collections;
using System.Collections.ObjectModel;


namespace HotaRmgTemplateEditor.ViewModels
{
    public class TemplatePackSettingsViewModel : ViewModelBase
	{
		public TemplatePack BaseTemplatePack { get; }
		public Template ActiveTemplate { get; }

		private string name;
		public string Name
		{
			get { return name; }
			set { name = value; NotifyPropertyChanged(); }
		}

		private string description;
		public string Description
		{
			get { return description; }
			set { description = value; NotifyPropertyChanged(); }
		}

		private bool allowAllTown;
		public bool AllowAllTown
		{
			get { return allowAllTown; }
			set { allowAllTown = value; NotifyPropertyChanged(); }
		}

		private bool allowCastle;
		public bool AllowCastle
		{
			get { return allowCastle; }
			set { allowCastle = value; NotifyPropertyChanged(); }
		}

		private bool allowRampart;
		public bool AllowRampart
		{
			get { return allowRampart; }
			set { allowRampart = value; NotifyPropertyChanged(); }
		}

		private bool allowTower;
		public bool AllowTower
		{
			get { return allowTower; }
			set { allowTower = value; NotifyPropertyChanged(); }
		}

		private bool allowInferno;
		public bool AllowInferno
		{
			get { return allowInferno; }
			set { allowInferno = value; NotifyPropertyChanged(); }
		}

		private bool allowNecropolis;
		public bool AllowNecropolis
		{
			get { return allowNecropolis; }
			set { allowNecropolis = value; NotifyPropertyChanged(); }
		}

		private bool allowDungeon;
		public bool AllowDungeon
		{
			get { return allowDungeon; }
			set { allowDungeon = value; NotifyPropertyChanged(); }
		}

		private bool allowStronghold;
		public bool AllowStronghold
		{
			get { return allowStronghold; }
			set { allowStronghold = value; NotifyPropertyChanged(); }
		}

		private bool allowFortress;
		public bool AllowFortress
		{
			get { return allowFortress; }
			set { allowFortress = value; NotifyPropertyChanged(); }
		}

		private bool allowConflux;
		public bool AllowConflux
		{
			get { return allowConflux; }
			set { allowConflux = value; NotifyPropertyChanged(); }
		}

		private bool allowCove;
		public bool AllowCove
		{
			get { return allowCove; }
			set { allowCove = value; NotifyPropertyChanged(); }
		}

		public RelayCommand ApplyTownSettingsToCurrentTemplate { get; }
		public RelayCommand ApplyTownSettingsToAllTemplates { get; }

		private bool maxBattleRoundsOverridden;
		public bool MaxBattleRoundsOverridden
		{
			get { return maxBattleRoundsOverridden; }
			set { maxBattleRoundsOverridden = value; NotifyPropertyChanged(); }
		}

		private int maxBattleRounds;
		public int MaxBattleRounds
		{
			get { return maxBattleRounds; }
			set { maxBattleRounds = value; NotifyPropertyChanged(); }
		}

		public ObservableCollection<EnableDisableItemViewModel> DisabledHeroes { get; }
		public ObservableCollection<EnableDisableItemViewModel> DefaultHeroes { get; }
		public ObservableCollection<EnableDisableItemViewModel> EnabledHeroes { get; }

		public TemplatePackSettingsViewModel(TemplatePack baseTemplatePack, Template activeTemplate)
		{
			BaseTemplatePack = baseTemplatePack;
			ActiveTemplate = activeTemplate;

			DisabledHeroes = [];
			EnabledHeroes = [];
			DefaultHeroes = [];

			Name = BaseTemplatePack.Options.Name;
			Description = BaseTemplatePack.Options.Description;

			AllowAllTown = true;

			foreach (var townOverride in BaseTemplatePack.Options.TownSelection.Values)
			{
				var allow = townOverride.IsAllowed;
				if (AllowAllTown == true && allow == false)
				{
					AllowAllTown = false;
				}

				switch (townOverride.Town)
				{
					case Town.Castle: AllowCastle = allow; break;
					case Town.Rampart: AllowRampart = allow; break;
					case Town.Tower: AllowTower = allow; break;
					case Town.Inferno: AllowInferno = allow; break;
					case Town.Necropolis: AllowNecropolis = allow; break;
					case Town.Dungeon: AllowDungeon = allow; break;
					case Town.Stronghold: AllowStronghold = allow; break;
					case Town.Fortress: AllowFortress = allow; break;
					case Town.Conflux: AllowConflux = allow; break;
					case Town.Cove: AllowCove = allow; break;
					case Town.Neutral: break;
					default:
						throw new NotImplementedException($"Town type not handled in {nameof(TemplatePackViewModel)}.ctor: {townOverride.Town}");
				}
			}

			ApplyTownSettingsToCurrentTemplate = new RelayCommand(_ => UpdateTownSettings(new[] { ActiveTemplate }));
			ApplyTownSettingsToAllTemplates = new RelayCommand(_ => UpdateTownSettings(BaseTemplatePack.Templates));

			MaxBattleRoundsOverridden = BaseTemplatePack.Options.MaxBattleRoundsOverridden;
			if (MaxBattleRoundsOverridden)
			{
				MaxBattleRounds = BaseTemplatePack.Options.MaxBattleRounds;
			}
			else
			{
				MaxBattleRounds = 100;
			}

			foreach (var itemOverride in BaseTemplatePack.Options.HeroOverrides.Values.OrderBy(a => a.Hero.Name))
			{
				var list = itemOverride.EnableDisable switch
				{
					EnableDisableDefault.Default => DefaultHeroes,
					EnableDisableDefault.Enable => EnabledHeroes,
					EnableDisableDefault.Disable => DisabledHeroes,
					_ => throw new InvalidOperationException()
				};
				var vm = new HeroViewModel(itemOverride.Hero);
				list.Add(vm);
			}
		}

		public void SaveChanges()
		{
			BaseTemplatePack.Options.Name = Name;
			BaseTemplatePack.Options.Description = Description;

			BaseTemplatePack.Options.MaxBattleRoundsOverridden = MaxBattleRoundsOverridden;
			BaseTemplatePack.Options.MaxBattleRounds = MaxBattleRounds;

			BaseTemplatePack.Options.TownSelection[Town.Castle].IsAllowed = AllowCastle;
			BaseTemplatePack.Options.TownSelection[Town.Rampart].IsAllowed = AllowRampart;
			BaseTemplatePack.Options.TownSelection[Town.Tower].IsAllowed = AllowTower;
			BaseTemplatePack.Options.TownSelection[Town.Inferno].IsAllowed = AllowInferno;
			BaseTemplatePack.Options.TownSelection[Town.Necropolis].IsAllowed = AllowNecropolis;
			BaseTemplatePack.Options.TownSelection[Town.Dungeon].IsAllowed = AllowDungeon;
			BaseTemplatePack.Options.TownSelection[Town.Stronghold].IsAllowed = AllowStronghold;
			BaseTemplatePack.Options.TownSelection[Town.Fortress].IsAllowed = AllowFortress;
			BaseTemplatePack.Options.TownSelection[Town.Conflux].IsAllowed = AllowConflux;
			BaseTemplatePack.Options.TownSelection[Town.Cove].IsAllowed = AllowCove;

			foreach (var item in DisabledHeroes.Cast<HeroViewModel>())
			{
				BaseTemplatePack.Options.HeroOverrides[item.BaseHero.Id].EnableDisable = EnableDisableDefault.Disable;
			}
			foreach (var item in DefaultHeroes.Cast<HeroViewModel>())
			{
				BaseTemplatePack.Options.HeroOverrides[item.BaseHero.Id].EnableDisable = EnableDisableDefault.Default;
			}
			foreach (var item in EnabledHeroes.Cast<HeroViewModel>())
			{
				BaseTemplatePack.Options.HeroOverrides[item.BaseHero.Id].EnableDisable = EnableDisableDefault.Enable;
			}
		}

		private void UpdateTownSettings(IEnumerable<Template> templates)
		{
			throw new NotImplementedException();
		}
	}
}
