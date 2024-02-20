using HotaRmgTemplateEditor.Domain.RmgFormat;
using HotaRmgTemplateEditor.Domain.RmgFormat.TownTypeHints;
using HotaRmgTemplateEditor.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace HotaRmgTemplateEditor.ViewModels
{
	public class ZoneSettingsViewModel : ViewModelBase
	{
		private int size;
		public int Size
		{
			get { return size; }
			set { size = value; NotifyPropertyChanged(); }
		}

		private int owner;
		public int Owner
		{
			get { return owner; }
			set { owner = value; NotifyPropertyChanged(); }
		}

		private int zoneId;
		public int ZoneId
		{
			get { return zoneId; }
			set { zoneId = value; NotifyPropertyChanged(); }
		}

		private ZoneType zoneType;
		public ZoneType ZoneType
		{
			get { return zoneType; }
			set { zoneType = value; NotifyPropertyChanged(); }
		}

		private bool zoneRepulsion;
		public bool ZoneRepulsion
		{
			get { return zoneRepulsion; }
			set { zoneRepulsion = value; NotifyPropertyChanged(); }
		}

		private ZonePlacement placement;
		public ZonePlacement Placement
		{
			get { return placement; }
			set { placement = value; NotifyPropertyChanged(); }
		}

		private bool allowNonCoherentRoad;
		public bool AllowNonCoherentRoad
		{
			get { return allowNonCoherentRoad; }
			set { allowNonCoherentRoad = value; NotifyPropertyChanged(); }
		}

		private bool terrainMatchToTown;
		public bool TerrainMatchToTown
		{
			get { return terrainMatchToTown; }
			set { terrainMatchToTown = value; NotifyPropertyChanged(); }
		}

		private bool terrainDirt;
		public bool TerrainDirt
		{
			get { return terrainDirt; }
			set { terrainDirt = value; NotifyPropertyChanged(); }
		}

		private bool terrainSand;
		public bool TerrainSand
		{
			get { return terrainSand; }
			set { terrainSand = value; NotifyPropertyChanged(); }
		}

		private bool terrainGrass;
		public bool TerrainGrass
		{
			get { return terrainGrass; }
			set { terrainGrass = value; NotifyPropertyChanged(); }
		}

		private bool terrainSnow;
		public bool TerrainSnow
		{
			get { return terrainSnow; }
			set { terrainSnow = value; NotifyPropertyChanged(); }
		}

		private bool terrainSwamp;
		public bool TerrainSwamp
		{
			get { return terrainSwamp; }
			set { terrainSwamp = value; NotifyPropertyChanged(); }
		}

		private bool terrainRough;
		public bool TerrainRough
		{
			get { return terrainRough; }
			set { terrainRough = value; NotifyPropertyChanged(); }
		}

		private bool terrainSubterranean;
		public bool TerrainSubterranean
		{
			get { return terrainSubterranean; }
			set { terrainSubterranean = value; NotifyPropertyChanged(); }
		}

		private bool terrainLava;
		public bool TerrainLava
		{
			get { return terrainLava; }
			set { terrainLava = value; NotifyPropertyChanged(); }
		}

		private bool terrainHighlands;
		public bool TerrainHighlands
		{
			get { return terrainHighlands; }
			set { terrainHighlands = value; NotifyPropertyChanged(); }
		}

		private bool terrainWasteland;
		public bool TerrainWasteland
		{
			get { return terrainWasteland; }
			set { terrainWasteland = value; NotifyPropertyChanged(); }
		}

		private bool treasureEnableRange1;
		public bool TreasureEnableRange1
		{
			get { return treasureEnableRange1; }
			set { treasureEnableRange1 = value; NotifyPropertyChanged(); NotifyPropertyChanged(nameof(TreasureZoneValue)); }
		}

		private int treasureRange1Low;
		public int TreasureRange1Low
		{
			get { return treasureRange1Low; }
			set { treasureRange1Low = value; NotifyPropertyChanged(); NotifyPropertyChanged(nameof(TreasureZoneValue)); }
		}

		private int treasureRange1High;
		public int TreasureRange1High
		{
			get { return treasureRange1High; }
			set { treasureRange1High = value; NotifyPropertyChanged(); NotifyPropertyChanged(nameof(TreasureZoneValue)); }
		}

		private int treasureRange1Density;
		public int TreasureRange1Density
		{
			get { return treasureRange1Density; }
			set { treasureRange1Density = value; NotifyPropertyChanged(); NotifyPropertyChanged(nameof(TreasureZoneValue)); }
		}

		private bool treasureEnableRange2;
		public bool TreasureEnableRange2
		{
			get { return treasureEnableRange2; }
			set { treasureEnableRange2 = value; NotifyPropertyChanged(); NotifyPropertyChanged(nameof(TreasureZoneValue)); }
		}

		private int treasureRange2Low;
		public int TreasureRange2Low
		{
			get { return treasureRange2Low; }
			set { treasureRange2Low = value; NotifyPropertyChanged(); NotifyPropertyChanged(nameof(TreasureZoneValue)); }
		}

		private int treasureRange2High;
		public int TreasureRange2High
		{
			get { return treasureRange2High; }
			set { treasureRange2High = value; NotifyPropertyChanged(); NotifyPropertyChanged(nameof(TreasureZoneValue)); }
		}

		private int treasureRange2Density;
		public int TreasureRange2Density
		{
			get { return treasureRange2Density; }
			set { treasureRange2Density = value; NotifyPropertyChanged(); NotifyPropertyChanged(nameof(TreasureZoneValue)); }
		}

		private bool treasureEnableRange3;
		public bool TreasureEnableRange3
		{
			get { return treasureEnableRange3; }
			set { treasureEnableRange3 = value; NotifyPropertyChanged(); NotifyPropertyChanged(nameof(TreasureZoneValue)); }
		}

		private int treasureRange3Low;
		public int TreasureRange3Low
		{
			get { return treasureRange3Low; }
			set { treasureRange3Low = value; NotifyPropertyChanged(); NotifyPropertyChanged(nameof(TreasureZoneValue)); }
		}

		private int treasureRange3High;
		public int TreasureRange3High
		{
			get { return treasureRange3High; }
			set { treasureRange3High = value; NotifyPropertyChanged(); NotifyPropertyChanged(nameof(TreasureZoneValue)); }
		}

		private int treasureRange3Density;
		public int TreasureRange3Density
		{
			get { return treasureRange3Density; }
			set { treasureRange3Density = value; NotifyPropertyChanged(); NotifyPropertyChanged(nameof(TreasureZoneValue)); }
		}

		private bool treasureNeutralRewards;
		public bool TreasureNeutralRewards
		{
			get { return treasureNeutralRewards; }
			set { treasureNeutralRewards = value; }
		}

		public int TreasureZoneValue
		{
			get
			{
				int value = 0;

				if (TreasureEnableRange1)
				{
					value += (TreasureRange1Low + TreasureRange1High) / 2 * TreasureRange1Density;
				}

				if (TreasureEnableRange2)
				{
					value += (TreasureRange2Low + TreasureRange2High) / 2 * TreasureRange2Density;
				}

				if (TreasureEnableRange3)
				{
					value += (TreasureRange3Low + TreasureRange3High) / 2 * TreasureRange3Density;
				}

				value /= 1000;

				return value;
			}
		}

		private ZoneMonsterStrength monsterStrength;
		public ZoneMonsterStrength MonsterStrength
		{
			get { return monsterStrength; }
			set { monsterStrength = value; NotifyPropertyChanged(); }
		}

		private ZoneMonsterDisposition monsterDisposition;
		public ZoneMonsterDisposition MonsterDisposition
		{
			get { return monsterDisposition; }
			set { monsterDisposition = value; NotifyPropertyChanged(); }
		}

		private ZoneMonsterJoinPercent monsterJoinPercent;
		public ZoneMonsterJoinPercent MonsterJoinPercent
		{
			get { return monsterJoinPercent; }
			set { monsterJoinPercent = value; NotifyPropertyChanged(); }
		}

		private bool monsterJoinOnlyForMoney;
		public bool MonsterJoinOnlyForMoney
		{
			get { return monsterJoinOnlyForMoney; }
			set { monsterJoinOnlyForMoney = value; NotifyPropertyChanged(); }
		}

		private bool monsterMatchToTown;
		public bool MonsterMatchToTown
		{
			get { return monsterMatchToTown; }
			set { monsterMatchToTown = value; NotifyPropertyChanged(); }
		}

		private bool monsterNeutral;
		public bool MonsterNeutral
		{
			get { return monsterNeutral; }
			set { monsterNeutral = value; NotifyPropertyChanged(); }
		}

		private bool monsterCastle;
		public bool MonsterCastle
		{
			get { return monsterCastle; }
			set { monsterCastle = value; NotifyPropertyChanged(); }
		}

		private bool monsterRampart;
		public bool MonsterRampart
		{
			get { return monsterRampart; }
			set { monsterRampart = value; NotifyPropertyChanged(); }
		}

		private bool monsterTower;
		public bool MonsterTower
		{
			get { return monsterTower; }
			set { monsterTower = value; NotifyPropertyChanged(); }
		}

		private bool monsterInferno;
		public bool MonsterInferno
		{
			get { return monsterInferno; }
			set { monsterInferno = value; NotifyPropertyChanged(); }
		}

		private bool monsterNecropolis;
		public bool MonsterNecropolis
		{
			get { return monsterNecropolis; }
			set { monsterNecropolis = value; NotifyPropertyChanged(); }
		}

		private bool monsterDungeon;
		public bool MonsterDungeon
		{
			get { return monsterDungeon; }
			set { monsterDungeon = value; NotifyPropertyChanged(); }
		}

		private bool monsterStronghold;
		public bool MonsterStronghold
		{
			get { return monsterStronghold; }
			set { monsterStronghold = value; NotifyPropertyChanged(); }
		}

		private bool monsterFortress;
		public bool MonsterFortress
		{
			get { return monsterFortress; }
			set { monsterFortress = value; NotifyPropertyChanged(); }
		}

		private bool monsterConflux;
		public bool MonsterConflux
		{
			get { return monsterConflux; }
			set { monsterConflux = value; NotifyPropertyChanged(); }
		}

		private bool monsterCove;
		public bool MonsterCove
		{
			get { return monsterCove; }
			set { monsterCove = value; NotifyPropertyChanged(); }
		}

		private bool restConditionalExistence;

		public bool RestConditionalExistence
		{
			get { return restConditionalExistence; }
			set { restConditionalExistence = value; NotifyPropertyChanged(); }
		}

		private int restMinHumanPlayers;
		public int RestMinHumanPlayers
		{
			get { return restMinHumanPlayers; }
			set { restMinHumanPlayers = value; NotifyPropertyChanged(); }
		}

		private int restMaxHumanPlayers;
		public int RestMaxHumanPlayers
		{
			get { return restMaxHumanPlayers; }
			set { restMaxHumanPlayers = value; NotifyPropertyChanged(); }
		}

		private int restMinTotalPlayers;
		public int RestMinTotalPlayers
		{
			get { return restMinTotalPlayers; }
			set { restMinTotalPlayers = value; NotifyPropertyChanged(); }
		}

		private int restMaxTotalPlayers;
		public int RestMaxTotalPlayers
		{
			get { return restMaxTotalPlayers; }
			set { restMaxTotalPlayers = value; NotifyPropertyChanged(); }
		}

		private int playerMinTowns;
		public int PlayerMinTowns
		{
			get { return playerMinTowns; }
			set { playerMinTowns = value; NotifyPropertyChanged(); }
		}

		private int playerTownDensity;
		public int PlayerTownDensity
		{
			get { return playerTownDensity; }
			set { playerTownDensity = value; NotifyPropertyChanged(); }
		}

		private int playerMinCastles;
		public int PlayerMinCastles
		{
			get { return playerMinCastles; }
			set { playerMinCastles = value; NotifyPropertyChanged(); }
		}

		private int playerCastleDensity;
		public int PlayerCastleDensity
		{
			get { return playerCastleDensity; }
			set { playerCastleDensity = value; NotifyPropertyChanged(); }
		}


		private int neutralMinTowns;
		public int NeutralMinTowns
		{
			get { return neutralMinTowns; }
			set { neutralMinTowns = value; NotifyPropertyChanged(); }
		}

		private int neutralTownDensity;
		public int NeutralTownDensity
		{
			get { return neutralTownDensity; }
			set { neutralTownDensity = value; NotifyPropertyChanged(); }
		}

		private int neutralMinCastles;
		public int NeutralMinCastles
		{
			get { return neutralMinCastles; }
			set { neutralMinCastles = value; NotifyPropertyChanged(); }
		}

		private int neutralCastleDensity;
		public int NeutralCastleDensity
		{
			get { return neutralCastleDensity; }
			set { neutralCastleDensity = value; NotifyPropertyChanged(); }
		}

		private bool neutralTownsSameType;

		public bool NeutralTownsSameType
		{
			get { return neutralTownsSameType; }
			set { neutralTownsSameType = value; NotifyPropertyChanged(); }
		}

		private bool allowedCastle;
		public bool AllowedCastle
		{
			get { return allowedCastle; }
			set { allowedCastle = value; NotifyPropertyChanged(); }
		}

		private bool allowedRampart;
		public bool AllowedRampart
		{
			get { return allowedRampart; }
			set { allowedRampart = value; NotifyPropertyChanged(); }
		}

		private bool allowedTower;
		public bool AllowedTower
		{
			get { return allowedTower; }
			set { allowedTower = value; NotifyPropertyChanged(); }
		}

		private bool allowedInferno;
		public bool AllowedInferno
		{
			get { return allowedInferno; }
			set { allowedInferno = value; NotifyPropertyChanged(); }
		}

		private bool allowedNecropolis;
		public bool AllowedNecropolis
		{
			get { return allowedNecropolis; }
			set { allowedNecropolis = value; NotifyPropertyChanged(); }
		}

		private bool allowedDungeon;
		public bool AllowedDungeon
		{
			get { return allowedDungeon; }
			set { allowedDungeon = value; NotifyPropertyChanged(); }
		}

		private bool allowedStronghold;
		public bool AllowedStronghold
		{
			get { return allowedStronghold; }
			set { allowedStronghold = value; NotifyPropertyChanged(); }
		}

		private bool allowedFortress;
		public bool AllowedFortress
		{
			get { return allowedFortress; }
			set { allowedFortress = value; NotifyPropertyChanged(); }
		}

		private bool allowedConflux;
		public bool AllowedConflux
		{
			get { return allowedConflux; }
			set { allowedConflux = value; NotifyPropertyChanged(); }
		}

		private bool allowedCove;
		public bool AllowedCove
		{
			get { return allowedCove; }
			set { allowedCove = value; NotifyPropertyChanged(); }
		}

		private int mineMinWood;
		public int MineMinWood
		{
			get { return mineMinWood; }
			set { mineMinWood = value; NotifyPropertyChanged(); }
		}

		private int mineDensityWood;
		public int MineDensityWood
		{
			get { return mineDensityWood; }
			set { mineDensityWood = value; NotifyPropertyChanged(); }
		}

		private int mineMinOre;
		public int MineMinOre
		{
			get { return mineMinOre; }
			set { mineMinOre = value; NotifyPropertyChanged(); }
		}

		private int mineDensityOre;
		public int MineDensityOre
		{
			get { return mineDensityOre; }
			set { mineDensityOre = value; NotifyPropertyChanged(); }
		}

		private int mineMinMercury;
		public int MineMinMercury
		{
			get { return mineMinMercury; }
			set { mineMinMercury = value; NotifyPropertyChanged(); }
		}

		private int mineDensityMercury;
		public int MineDensityMercury
		{
			get { return mineDensityMercury; }
			set { mineDensityMercury = value; NotifyPropertyChanged(); }
		}

		private int mineMinSulfur;
		public int MineMinSulfur
		{
			get { return mineMinSulfur; }
			set { mineMinSulfur = value; NotifyPropertyChanged(); }
		}

		private int mineDensitySulfur;
		public int MineDensitySulfur
		{
			get { return mineDensitySulfur; }
			set { mineDensitySulfur = value; NotifyPropertyChanged(); }
		}

		private int mineMinCrystal;
		public int MineMinCrystal
		{
			get { return mineMinCrystal; }
			set { mineMinCrystal = value; NotifyPropertyChanged(); }
		}

		private int mineDensityCrystal;
		public int MineDensityCrystal
		{
			get { return mineDensityCrystal; }
			set { mineDensityCrystal = value; NotifyPropertyChanged(); }
		}

		private int mineMinGems;
		public int MineMinGems
		{
			get { return mineMinGems; }
			set { mineMinGems = value; NotifyPropertyChanged(); }
		}

		private int mineDensityGems;
		public int MineDensityGems
		{
			get { return mineDensityGems; }
			set { mineDensityGems = value; NotifyPropertyChanged(); }
		}

		private int mineMinGold;
		public int MineMinGold
		{
			get { return mineMinGold; }
			set { mineMinGold = value; NotifyPropertyChanged(); }
		}

		private int mineDensityGold;
		public int MineDensityGold
		{
			get { return mineDensityGold; }
			set { mineDensityGold = value; NotifyPropertyChanged(); }
		}

		public ObjectRulesViewModel ObjectRules { get; }

		public ObservableCollection<TownTypeHintViewModel> TownTypeHints { get; }
		public TownTypeHintViewModel SelectedTownTypeHint { get; set; }

		public RelayCommand SelectAllTerrainCommand { get; }
		public RelayCommand SelectNoTerrainCommand { get; }

		public RelayCommand SelectAllMonstersCommand { get; }
		public RelayCommand SelectNoMonsterCommand { get; }

		public RelayCommand SelectAllTownsCommand { get; }
		public RelayCommand SelectNoTownCommand { get; }

		public RelayCommand AddTownHintCommand { get; }
		public RelayCommand EditTownHintCommand { get; }
		public RelayCommand DeleteTownHintCommand { get; }
		public RelayCommand CopyTownHintCommand { get; }
		public RelayCommand PasteTownHintCommand { get; }
		public RelayCommand CutTownHintCommand { get; }
		public RelayCommand DuplicateTownHintCommand { get; }

		public Dictionary<ZoneType, string> ZoneTypeEnumsWithCaptions { get; private set; }
		public Dictionary<ZonePlacement, string> ZonePlacementEnumsWithCaption { get; private set; }
		public Dictionary<ZoneMonsterStrength, string> ZoneMonsterStrengthEnumsWithCaption { get; private set; }
		public Dictionary<ZoneMonsterDisposition, string> ZoneMonsterDispositionEnumsWithCaption { get; private set; }
		public Dictionary<ZoneMonsterJoinPercent, string> ZoneMonsterJoinPercentEnumsWithCaption { get; private set; }

		private void SetupHelpers()
		{
			ZoneTypeEnumsWithCaptions = new Dictionary<ZoneType, string>
			{
				{ ZoneType.HumanStart, "Human start"},
				{ ZoneType.ComputerStart, "Computer start"},
				{ ZoneType.Treasure, "Treasure"},
				{ ZoneType.Junction, "Junction"},
			};

			ZonePlacementEnumsWithCaption = new Dictionary<ZonePlacement, string>
			{
				{ ZonePlacement.Free, "Free" },
				{ ZonePlacement.Underground, "Underground" },
				{ ZonePlacement.Ground, "Ground" },
			};

			ZoneMonsterStrengthEnumsWithCaption = new Dictionary<ZoneMonsterStrength, string>
			{
				{ ZoneMonsterStrength.None, "None" },
				{ ZoneMonsterStrength.Weak, "Weak" },
				{ ZoneMonsterStrength.Average, "Average" },
				{ ZoneMonsterStrength.Strong, "Strong" },
			};

			ZoneMonsterDispositionEnumsWithCaption = new Dictionary<ZoneMonsterDisposition, string>
			{
				{ ZoneMonsterDisposition.AlwaysJoin, "Compliant (always join)" },
				{ ZoneMonsterDisposition.Friendly, "Friendly (1-7)" },
				{ ZoneMonsterDisposition.Aggressive, "Aggressive (1-10)" },
				{ ZoneMonsterDisposition.Hostile, "Hostile (4-10)" },
				{ ZoneMonsterDisposition.Savage, "Savage (10)" },
				{ ZoneMonsterDisposition.Custom1, "Custom: 1" },
				{ ZoneMonsterDisposition.Custom2, "Custom: 2" },
				{ ZoneMonsterDisposition.Custom3, "Custom: 3" },
				{ ZoneMonsterDisposition.Custom4, "Custom: 4" },
				{ ZoneMonsterDisposition.Custom5, "Custom: 5" },
				{ ZoneMonsterDisposition.Custom6, "Custom: 6" },
				{ ZoneMonsterDisposition.Custom7, "Custom: 7" },
				{ ZoneMonsterDisposition.Custom8, "Custom: 8" },
				{ ZoneMonsterDisposition.Custom9, "Custom: 9" },
			};

			ZoneMonsterJoinPercentEnumsWithCaption = new Dictionary<ZoneMonsterJoinPercent, string>
			{
				{ ZoneMonsterJoinPercent.Percent25, "25%" },
				{ ZoneMonsterJoinPercent.Percent50, "50%" },
				{ ZoneMonsterJoinPercent.Percent75, "75%" },
				{ ZoneMonsterJoinPercent.Percent100, "100%" },
			};
		}

		public Domain.RmgFormat.Zone BaseZone { get; }
		private IDialogService DialogService { get; }

		public ZoneSettingsViewModel(IDialogService dialogService, ZoneViewModel zoneVm)
		{
			DialogService = dialogService;

			SetupHelpers();

			ObjectRules = new ObjectRulesViewModel();
			foreach (var objOverride in zoneVm.Zone.ObjectOverrides)
			{
				var oovm = new ObjectRuleItemViewModel(objOverride);
				ObjectRules.RuleItems.Add(oovm);
			}

			BaseZone = zoneVm.Zone;
			var z = zoneVm.Zone;
			Size = z.Size;
			Owner = z.PlayerTowns.Ownership;
			ZoneId = z.Id;
			ZoneType = z.Type;
			ZoneRepulsion = z.ZoneRepulsion;
			Placement = z.Placement;
			AllowNonCoherentRoad = z.AllowNonCoherentRoad;
			TerrainMatchToTown = z.Terrain.MatchToTown;
			TerrainDirt = z.Terrain.Dirt;
			TerrainSand = z.Terrain.Sand;
			TerrainGrass = z.Terrain.Grass;
			TerrainSnow = z.Terrain.Snow;
			TerrainSwamp = z.Terrain.Swamp;
			TerrainRough = z.Terrain.Rough;
			TerrainSubterranean = z.Terrain.Subterranean;
			TerrainLava = z.Terrain.Lava;
			TerrainHighlands = z.Terrain.Highlands;
			TerrainWasteland = z.Terrain.Wasteland;

			TreasureEnableRange1 = z.Treasures.Range1.Enabled;
			TreasureEnableRange2 = z.Treasures.Range2.Enabled;
			TreasureEnableRange3 = z.Treasures.Range3.Enabled;
			TreasureRange1Low = z.Treasures.Range1.Low;
			TreasureRange2Low = z.Treasures.Range2.Low;
			TreasureRange3Low = z.Treasures.Range3.Low;
			TreasureRange1High = z.Treasures.Range1.High;
			TreasureRange2High = z.Treasures.Range2.High;
			TreasureRange3High = z.Treasures.Range3.High;
			TreasureRange1Density = z.Treasures.Range1.Density;
			TreasureRange2Density = z.Treasures.Range2.Density;
			TreasureRange3Density = z.Treasures.Range3.Density;
			TreasureNeutralRewards = z.Treasures.ForceNeutralCreatures;

			MonsterStrength = z.Monsters.Strength;
			MonsterDisposition = z.Monsters.Disposition;
			MonsterJoinPercent = z.Monsters.JoinPercent;
			MonsterJoinOnlyForMoney = z.Monsters.JoinOnlyForMoney;
			MonsterMatchToTown = z.Monsters.MatchToTown;
			MonsterNeutral = z.Monsters.Neutral;
			MonsterCastle = z.Monsters.Castle;
			MonsterRampart = z.Monsters.Rampart;
			MonsterTower = z.Monsters.Tower;
			MonsterInferno = z.Monsters.Inferno;
			MonsterNecropolis = z.Monsters.Necropolis;
			MonsterDungeon = z.Monsters.Dungeon;
			MonsterStronghold = z.Monsters.Stronghold;
			MonsterFortress = z.Monsters.Fortress;
			MonsterConflux = z.Monsters.Conflux;
			MonsterCove = z.Monsters.Cove;

			RestMinHumanPlayers = z.Restrictions.MinimumHumanPlayers;
			RestMaxHumanPlayers = z.Restrictions.MaximumHumanPlayers;
			RestMinTotalPlayers = z.Restrictions.MinimumTotalPlayers;
			RestMaxTotalPlayers = z.Restrictions.MaximumTotalPlayers;

			PlayerMinTowns = z.PlayerTowns.MinimumTowns;
			PlayerTownDensity = z.PlayerTowns.TownDensity;
			PlayerMinCastles = z.PlayerTowns.MinimumCastles;
			PlayerCastleDensity = z.PlayerTowns.CastleDensity;

			NeutralMinTowns = z.NeutralTowns.MinimumTowns;
			NeutralTownDensity = z.NeutralTowns.TownDensity;
			NeutralMinCastles = z.NeutralTowns.MinimumCastles;
			NeutralCastleDensity = z.NeutralTowns.CastleDensity;
			NeutralTownsSameType = z.NeutralTowns.TownsAreOfSameType;

			AllowedCastle = z.AllowedTowns.Castle;
			AllowedRampart = z.AllowedTowns.Rampart;
			AllowedTower = z.AllowedTowns.Tower;
			AllowedInferno = z.AllowedTowns.Inferno;
			AllowedNecropolis = z.AllowedTowns.Necropolis;
			AllowedDungeon = z.AllowedTowns.Dungeon;
			AllowedStronghold = z.AllowedTowns.Stronghold;
			AllowedFortress = z.AllowedTowns.Fortress;
			AllowedConflux = z.AllowedTowns.Conflux;
			AllowedCove = z.AllowedTowns.Cove;

			MineMinWood = z.Mines.WoodMinimum;
			MineDensityWood = z.Mines.WoodDensity;
			MineMinOre = z.Mines.OreMinimum;
			MineDensityOre = z.Mines.OreDensity;
			MineMinMercury = z.Mines.MercuryMinimum;
			MineDensityMercury = z.Mines.MercuryDensity;
			MineMinSulfur = z.Mines.SulfurMinimum;
			MineDensitySulfur = z.Mines.SulfurDensity;
			MineMinCrystal = z.Mines.CrystalMinimum;
			MineDensityCrystal = z.Mines.CrystalDensity;
			MineMinGems = z.Mines.GemsMinimum;
			MineDensityGems = z.Mines.GemsDensity;
			MineMinGold = z.Mines.GoldMinimum;
			MineDensityGold = z.Mines.GoldDensity;

			TownTypeHints = [];
			foreach (var hint in z.TownHints)
			{
				TownTypeHints.Add(new TownTypeHintViewModel(hint));
			}

			SelectAllTerrainCommand = new RelayCommand(_ =>
			{
				TerrainDirt = true;
				TerrainSand = true;
				TerrainGrass = true;
				TerrainSnow = true;
				TerrainSwamp = true;
				TerrainRough = true;
				TerrainSubterranean = true;
				TerrainLava = true;
				TerrainHighlands = true;
				TerrainWasteland = true;
			});

			SelectNoTerrainCommand = new RelayCommand(_ =>
			{
				TerrainDirt = false;
				TerrainSand = false;
				TerrainGrass = false;
				TerrainSnow = false;
				TerrainSwamp = false;
				TerrainRough = false;
				TerrainSubterranean = false;
				TerrainLava = false;
				TerrainHighlands = false;
				TerrainWasteland = false;
			});

			SelectAllMonstersCommand = new RelayCommand(_ =>
			{
				MonsterNeutral = true;
				MonsterCastle = true;
				MonsterRampart = true;
				MonsterTower = true;
				MonsterInferno = true;
				MonsterNecropolis = true;
				MonsterDungeon = true;
				MonsterStronghold = true;
				MonsterFortress = true;
				MonsterConflux = true;
				MonsterCove = true;
			});

			SelectNoMonsterCommand = new RelayCommand(_ =>
			{
				MonsterNeutral = false;
				MonsterCastle = false;
				MonsterRampart = false;
				MonsterTower = false;
				MonsterInferno = false;
				MonsterNecropolis = false;
				MonsterDungeon = false;
				MonsterStronghold = false;
				MonsterFortress = false;
				MonsterConflux = false;
				MonsterCove = false;
			});

			SelectAllTownsCommand = new RelayCommand(_ =>
			{
				AllowedCastle = true;
				AllowedRampart = true;
				AllowedTower = true;
				AllowedInferno = true;
				AllowedNecropolis = true;
				AllowedDungeon = true;
				AllowedStronghold = true;
				AllowedFortress = true;
				AllowedConflux = true;
				AllowedCove = true;
			});

			SelectNoTownCommand = new RelayCommand(_ =>
			{
				AllowedCastle = false;
				AllowedRampart = false;
				AllowedTower = false;
				AllowedInferno = false;
				AllowedNecropolis = false;
				AllowedDungeon = false;
				AllowedStronghold = false;
				AllowedFortress = false;
				AllowedConflux = false;
				AllowedCove = false;
			});

			AddTownHintCommand = new RelayCommand(_ =>
			{
				var newHint = new TownTypeHint();
				var vm = new AddEditTownTypeHintViewModel(newHint);
				if (DialogService.ShowTownTypeHintDialog(vm) ?? false)
				{
					TownTypeHints.Add(new TownTypeHintViewModel(newHint));
				}
			});

			EditTownHintCommand = new RelayCommand(_ =>
			{
				var vm = new AddEditTownTypeHintViewModel(SelectedTownTypeHint.BaseHint);
				if (DialogService.ShowTownTypeHintDialog(vm) ?? false)
				{
					SelectedTownTypeHint.RefreshProperties();
				}
			},
			_ => SelectedTownTypeHint != null);

			DeleteTownHintCommand = new RelayCommand(lst =>
			{
				var selectedItems = lst as IList;
				if (selectedItems == null)
				{
					return;
				}

				var vms = selectedItems.Cast<TownTypeHintViewModel>().ToList();
				foreach (var vm in vms)
				{
					z.TownHints.Remove(vm.BaseHint);
					TownTypeHints.Remove(vm);
				}
			},
			lst =>
			{
				var selectedItems = lst as IList;
				return selectedItems != null && selectedItems.Count > 0;
			});

			CopyTownHintCommand = new RelayCommand(lst => {
				var selectedItems = lst as IList;

			},
			lst => {
				var selectedItems = lst as IList;
				return selectedItems != null && selectedItems.Count > 0;
			});

			PasteTownHintCommand = new RelayCommand(_ => { });
			CutTownHintCommand = new RelayCommand(_ => { });
			DuplicateTownHintCommand = new RelayCommand(_ => { });
		}

		public void SaveChanges()
		{
			var z = BaseZone;
			z.Size = Size;
			z.PlayerTowns.Ownership = Owner;
			z.Id = ZoneId;
			z.Type = ZoneType;
			z.ZoneRepulsion = ZoneRepulsion;
			z.Placement = Placement;
			z.AllowNonCoherentRoad = AllowNonCoherentRoad;
			z.Terrain.MatchToTown = TerrainMatchToTown;
			z.Terrain.Dirt = TerrainDirt;
			z.Terrain.Sand = TerrainSand;
			z.Terrain.Grass = TerrainGrass;
			z.Terrain.Snow = TerrainSnow;
			z.Terrain.Swamp = TerrainSwamp;
			z.Terrain.Rough = TerrainRough;
			z.Terrain.Subterranean = TerrainSubterranean;
			z.Terrain.Lava = TerrainLava;
			z.Terrain.Highlands = TerrainHighlands;
			z.Terrain.Wasteland = TerrainWasteland;

			z.Treasures.Range1.Enabled = TreasureEnableRange1;
			z.Treasures.Range2.Enabled = TreasureEnableRange2;
			z.Treasures.Range3.Enabled = TreasureEnableRange3;
			z.Treasures.Range1.Low = TreasureRange1Low;
			z.Treasures.Range2.Low = TreasureRange2Low;
			z.Treasures.Range3.Low = TreasureRange3Low;
			z.Treasures.Range1.High = TreasureRange1High;
			z.Treasures.Range2.High = TreasureRange2High;
			z.Treasures.Range3.High = TreasureRange3High;
			z.Treasures.Range1.Density = TreasureRange1Density;
			z.Treasures.Range2.Density = TreasureRange2Density;
			z.Treasures.Range3.Density = TreasureRange3Density;
			z.Treasures.ForceNeutralCreatures = TreasureNeutralRewards;

			z.Monsters.Strength = MonsterStrength;
			z.Monsters.Disposition = MonsterDisposition;
			z.Monsters.JoinPercent = MonsterJoinPercent;
			z.Monsters.JoinOnlyForMoney = MonsterJoinOnlyForMoney;
			z.Monsters.MatchToTown = MonsterMatchToTown;
			z.Monsters.Neutral = MonsterNeutral;
			z.Monsters.Castle = MonsterCastle;
			z.Monsters.Rampart = MonsterRampart;
			z.Monsters.Tower = MonsterTower;
			z.Monsters.Inferno = MonsterInferno;
			z.Monsters.Necropolis = MonsterNecropolis;
			z.Monsters.Dungeon = MonsterDungeon;
			z.Monsters.Stronghold = MonsterStronghold;
			z.Monsters.Fortress = MonsterFortress;
			z.Monsters.Conflux = MonsterConflux;
			z.Monsters.Cove = MonsterCove;

			z.Restrictions.MinimumHumanPlayers = RestMinHumanPlayers;
			z.Restrictions.MaximumHumanPlayers = RestMaxHumanPlayers;
			z.Restrictions.MinimumTotalPlayers = RestMinTotalPlayers;
			z.Restrictions.MaximumTotalPlayers = RestMaxTotalPlayers;

			z.PlayerTowns.MinimumTowns = PlayerMinTowns;
			z.PlayerTowns.TownDensity = PlayerTownDensity;
			z.PlayerTowns.MinimumCastles = PlayerMinCastles;
			z.PlayerTowns.CastleDensity = PlayerCastleDensity;

			z.NeutralTowns.MinimumTowns = NeutralMinTowns;
			z.NeutralTowns.TownDensity = NeutralTownDensity;
			z.NeutralTowns.MinimumCastles = NeutralMinCastles;
			z.NeutralTowns.CastleDensity = NeutralCastleDensity;
			z.NeutralTowns.TownsAreOfSameType = NeutralTownsSameType;

			z.AllowedTowns.Castle = AllowedCastle;
			z.AllowedTowns.Rampart = AllowedRampart;
			z.AllowedTowns.Tower = AllowedTower;
			z.AllowedTowns.Inferno = AllowedInferno;
			z.AllowedTowns.Necropolis = AllowedNecropolis;
			z.AllowedTowns.Dungeon = AllowedDungeon;
			z.AllowedTowns.Stronghold = AllowedStronghold;
			z.AllowedTowns.Fortress = AllowedFortress;
			z.AllowedTowns.Conflux = AllowedConflux;
			z.AllowedTowns.Cove = AllowedCove;

			z.Mines.WoodMinimum = MineMinWood;
			z.Mines.WoodDensity = MineDensityWood;
			z.Mines.OreMinimum = MineMinOre;
			z.Mines.OreDensity = MineDensityOre;
			z.Mines.MercuryMinimum = MineMinMercury;
			z.Mines.MercuryDensity = MineDensityMercury;
			z.Mines.SulfurMinimum = MineMinSulfur;
			z.Mines.SulfurDensity = MineDensitySulfur;
			z.Mines.CrystalMinimum = MineMinCrystal;
			z.Mines.CrystalDensity = MineDensityCrystal;
			z.Mines.GemsMinimum = MineMinGems;
			z.Mines.GemsDensity = MineDensityGems;
			z.Mines.GoldMinimum = MineMinGold;
			z.Mines.GoldDensity = MineDensityGold;
		}
	}
}
