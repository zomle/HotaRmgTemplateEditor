using HotaRmgTemplateEditor.Domain.RmgFormat.Handler;
using HotaRmgTemplateEditor.Domain.RmgFormat.Overrides;
using HotaRmgTemplateEditor.Helpers;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace HotaRmgTemplateEditor.ViewModels
{
    public class ObjectRulesViewModel : ViewModelBase
	{
		public bool ShowMaxOnMap { get; set; }
		public ObservableCollection<ObjectRuleItemViewModel> RuleItems { get; set; }
		
		private ObjectRuleItemViewModel? selectedRuleItem;
		public ObjectRuleItemViewModel? SelectedRuleItem
		{
			get { return selectedRuleItem; }
			set { selectedRuleItem = value; NotifyPropertyChanged(); }
		}

		public RelayCommand AddCommand { get; }
		public RelayCommand EditCommand { get; }
		public RelayCommand DeleteCommand { get; }
		public RelayCommand CopyCommand { get; }
		public RelayCommand PasteCommand { get; }
		public RelayCommand CutCommand { get; }
		public RelayCommand DuplicateCommand { get; }

		public ObjectRulesViewModel()
		{
			RuleItems = [];

			AddCommand = new RelayCommand(_ => { });
			EditCommand = new RelayCommand(_ => { }, _ => SelectedRuleItem != null);
			DeleteCommand = new RelayCommand(_ => { }, _ => SelectedRuleItem != null);
			CopyCommand = new RelayCommand(_ => { }, _ => SelectedRuleItem != null);
			PasteCommand = new RelayCommand(_ => { });
			CutCommand = new RelayCommand(_ => { }, _ => SelectedRuleItem != null);
			DuplicateCommand = new RelayCommand(_ => { }, _ => SelectedRuleItem != null);
		}
	}

	public enum ObjectRuleType
	{
		Disable,
		EnableEdit
	}

	public class CustomizedValueViewModel : ViewModelBase
	{
		private AmountRestriction amountRestriction;
		public AmountRestriction AmountRestriction
		{
			get { return amountRestriction; }
			set
			{
				amountRestriction = value; NotifyPropertyChanged();
				NotifyPropertyChanged(nameof(FullString));
				NotifyPropertyChanged(nameof(ShortString));
			}
		}

		private int customizedAmount;
		public int CustomizedAmount
		{
			get { return customizedAmount; }
			set
			{
				customizedAmount = value;
				NotifyPropertyChanged();
				NotifyPropertyChanged(nameof(FullString));
				NotifyPropertyChanged(nameof(ShortString));
			}
		}
		public string FullString
		{
			get
			{
				return AmountRestriction switch
				{
					AmountRestriction.Default => "default",
					AmountRestriction.NoLimit => "no limit",
					AmountRestriction.Disabled => string.Empty,
					AmountRestriction.Custom => CustomizedAmount.ToString(),
					_ => throw new NotImplementedException()
				};
			}
		}

		public string ShortString
		{
			get
			{
				return AmountRestriction switch
				{
					AmountRestriction.Default => "d",
					AmountRestriction.NoLimit => "n",
					AmountRestriction.Disabled => string.Empty,
					AmountRestriction.Custom => CustomizedAmount.ToString(),
					_ => throw new NotImplementedException()
				};
			}
		}


		public CustomizedValueViewModel(AmountRestriction amountRestriction, int customizedAmount)
		{
			AmountRestriction = amountRestriction;
			CustomizedAmount = customizedAmount;
		}
	}

	public class ObjectRuleItemViewModel : ViewModelBase
	{
		private ObjectRuleType ruleType;
		public ObjectRuleType RuleType
		{
			get { return ruleType; }
			set { ruleType = value; NotifyPropertyChanged(); }
		}

		private string objectName;
		public string ObjectName
		{
			get { return objectName; }
			set { objectName = value; NotifyPropertyChanged(); }
		}

		private string objectReference;
		public string ObjectReference
		{
			get { return objectReference; }
			set { objectReference = value; NotifyPropertyChanged(); NotifyPropertyChanged(nameof(Formula)); }
		}

		private CustomizedValueViewModel objectValue;
		public CustomizedValueViewModel ObjectValue
		{
			get { return objectValue; }
			set { objectValue = value; NotifyPropertyChanged(); }
		}

		private CustomizedValueViewModel frequency;
		public CustomizedValueViewModel Frequency
		{
			get { return frequency; }
			set { frequency = value; NotifyPropertyChanged(); }
		}

		private CustomizedValueViewModel maxOnMap;
		public CustomizedValueViewModel MaxOnMap
		{
			get { return maxOnMap; }
			set { maxOnMap = value; NotifyPropertyChanged(); }
		}

		private CustomizedValueViewModel maxPerZone;
		public CustomizedValueViewModel MaxPerZone
		{
			get { return maxPerZone; }
			set { maxPerZone = value; NotifyPropertyChanged(); }
		}

		public string Formula
		{
			get
			{
				var sb = new StringBuilder();
				if (RuleType == ObjectRuleType.EnableEdit)
				{
					sb.Append('+');
					sb.Append($"{ObjectReference} {ObjectValue.ShortString} {Frequency.ShortString} {MaxOnMap.ShortString} {MaxPerZone.ShortString}");
				}
				else if (RuleType == ObjectRuleType.Disable)
				{
					sb.Append('-');
					sb.Append(ObjectReference);
				}

				return sb.ToString();
			}
		}

		public GameObjectOverride BaseObject { get; }

		public ObjectRuleItemViewModel(GameObjectOverride baseObject)
		{
			BaseObject = baseObject;

			RuleType = BaseObject.EnableDisable switch
			{
				EnableDisableDefault.Default => throw new NotImplementedException(),
				EnableDisableDefault.Enable => ObjectRuleType.EnableEdit,
				EnableDisableDefault.Disable => ObjectRuleType.Disable,
				_ => throw new NotImplementedException(),
			};

			ObjectReference = $"[{string.Join(" ", BaseObject.ObjectReference)}]";
			ObjectName = BaseObject.GameObject.Name;

			ObjectValue = new CustomizedValueViewModel(BaseObject.CustomValue == null ? AmountRestriction.Default : AmountRestriction.Custom, BaseObject.CustomValue ?? 0);
			Frequency = new CustomizedValueViewModel(BaseObject.CustomFrequency == null ? AmountRestriction.Default : AmountRestriction.Custom, BaseObject.CustomFrequency ?? 0);
			MaxOnMap = new CustomizedValueViewModel(BaseObject.MaxOnMap, BaseObject.MaxOnMapAmount ?? 0);
			MaxPerZone = new CustomizedValueViewModel(BaseObject.MaxPerZone, BaseObject.MaxPerZoneAmount ?? 0);
		}
	}
}
