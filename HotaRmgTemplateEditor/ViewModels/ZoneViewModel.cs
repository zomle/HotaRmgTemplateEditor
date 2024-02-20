using HotaRmgTemplateEditor.Domain.RmgFormat;
using HotaRmgTemplateEditor.Helpers;

namespace HotaRmgTemplateEditor.ViewModels
{
	public class ZoneViewModel : ViewModelBase
	{
		public bool IsMirrorZone { get; set; }
		public int ZoneValue
		{
			get
			{
				int value = 0;

				if (Zone.Treasures.Range1.Enabled)
				{
					value += (Zone.Treasures.Range1.Low + Zone.Treasures.Range1.High) / 2 * Zone.Treasures.Range1.Density;
				}

				if (Zone.Treasures.Range2.Enabled)
				{
					value += (Zone.Treasures.Range2.Low + Zone.Treasures.Range2.High) / 2 * Zone.Treasures.Range2.Density;
				}

				if (Zone.Treasures.Range3.Enabled)
				{
					value += (Zone.Treasures.Range3.Low + Zone.Treasures.Range3.High) / 2 * Zone.Treasures.Range3.Density;
				}

				return value / 1000;
			}
		}

		private int ownerPlayer;
		public int OwnerPlayer
		{
			get
			{
				if (ownerPlayer == 0)
				{
					if (Zone.Type == ZoneType.HumanStart || Zone.Type == ZoneType.ComputerStart)
					{
						ownerPlayer = Zone.PlayerTowns.Ownership;
					}
				}

				return ownerPlayer;
			}
			set
			{
				ownerPlayer = value;
				NotifyPropertyChanged();
				NotifyPropertyChanged(nameof(BackgroundType));
			}
		}

		public UserControls.ZoneBackgroundType BackgroundType
		{
			get
			{
				switch (Zone.Type)
				{
					case ZoneType.HumanStart:
					case ZoneType.ComputerStart:
						return OwnerPlayer switch
						{
							1 => UserControls.ZoneBackgroundType.Player1,
							2 => UserControls.ZoneBackgroundType.Player2,
							3 => UserControls.ZoneBackgroundType.Player3,
							4 => UserControls.ZoneBackgroundType.Player4,
							5 => UserControls.ZoneBackgroundType.Player5,
							6 => UserControls.ZoneBackgroundType.Player6,
							7 => UserControls.ZoneBackgroundType.Player7,
							8 => UserControls.ZoneBackgroundType.Player8,
							_ => throw new InvalidOperationException("Unknown ownership: " + Zone.PlayerTowns.Ownership)
						};

					case ZoneType.Treasure:
						if (ZoneValue < 100)
						{
							return UserControls.ZoneBackgroundType.TreasurePoor;
						}
						else if (ZoneValue < 200)
						{
							return UserControls.ZoneBackgroundType.TreasureNormal;
						}
						else
						{
							return UserControls.ZoneBackgroundType.TreasureSuper;
						}

					case ZoneType.Junction:
						return UserControls.ZoneBackgroundType.TreasureSuper;

					default:
						throw new InvalidOperationException("Unhandled zone type: " + Zone.Type.ToString());
				}
			}
		}

		public ZoneMonsterStrength MonsterStrength
		{
			get { return Zone.Monsters.Strength; }
			set
			{
				Zone.Monsters.Strength = value;
				NotifyPropertyChanged();
			}
		}

		#region Towns
		public int PlayerTowns
		{
			get { return Zone.PlayerTowns.MinimumTowns; }
			set
			{
				Zone.PlayerTowns.MinimumTowns = value;
				NotifyPropertyChanged();
			}
		}

		public int PlayerCastles
		{
			get { return Zone.PlayerTowns.MinimumCastles; }
			set
			{
				Zone.PlayerTowns.MinimumCastles = value;
				NotifyPropertyChanged();
			}
		}

		public int NeutralTowns
		{
			get { return Zone.NeutralTowns.MinimumTowns; }
			set
			{
				Zone.NeutralTowns.MinimumTowns = value;
				NotifyPropertyChanged();
			}
		}

		public int NeutralCastles
		{
			get { return Zone.NeutralTowns.MinimumCastles; }
			set
			{
				Zone.NeutralTowns.MinimumCastles = value;
				NotifyPropertyChanged();
			}
		}
		#endregion

		#region Mines
		public int SawMills
		{
			get { return Zone.Mines.WoodMinimum; }
			set
			{
				Zone.Mines.WoodMinimum = value;
				NotifyPropertyChanged();
			}
		}

		public int GemMines
		{
			get { return Zone.Mines.GemsMinimum; }
			set
			{
				Zone.Mines.GemsMinimum = value;
				NotifyPropertyChanged();
			}
		}

		public int MercuryMines
		{
			get { return Zone.Mines.MercuryMinimum; }
			set
			{
				Zone.Mines.MercuryMinimum = value;
				NotifyPropertyChanged();
			}
		}

		public int OreMines
		{
			get { return Zone.Mines.OreMinimum; }
			set
			{
				Zone.Mines.OreMinimum = value;
				NotifyPropertyChanged();
			}
		}

		public int SulfurMines
		{
			get { return Zone.Mines.SulfurMinimum; }
			set
			{
				Zone.Mines.SulfurMinimum = value;
				NotifyPropertyChanged();
			}
		}

		public int CrystalMines
		{
			get { return Zone.Mines.CrystalMinimum; }
			set
			{
				Zone.Mines.CrystalMinimum = value;
				NotifyPropertyChanged();
			}
		}

		public int GoldMines
		{
			get { return Zone.Mines.GoldMinimum; }
			set
			{
				Zone.Mines.GoldMinimum = value;
				NotifyPropertyChanged();
			}
		}

		#endregion

		public RelayCommand ShowSettingsDialog { get; }
		public Zone Zone { get; }

		private IDialogService DialogService { get; }
		public ZoneViewModel(IDialogService dialogService, Zone zone)
		{
			DialogService = dialogService;
			Zone = zone;

			ShowSettingsDialog = new RelayCommand(_ =>
			{
				DialogService.ShowEditZoneSettingsDialog(new ZoneSettingsViewModel(dialogService, this));
			});
		}
	}
}
