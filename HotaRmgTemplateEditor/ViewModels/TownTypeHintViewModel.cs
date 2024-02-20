using HotaRmgTemplateEditor.Domain.RmgFormat.TownTypeHints;

namespace HotaRmgTemplateEditor.ViewModels
{
	public class TownTypeHintViewModel : ViewModelBase
	{
		public string Formula
		{
			get { return BaseHint.GetFormula(); }
		}

		public string Description
		{
			get { return BaseHint.GetDescription(); }
		}

		public void RefreshProperties()
		{
			NotifyPropertyChanged(nameof(Formula));
			NotifyPropertyChanged(nameof(Description));
		}

		public TownTypeHint BaseHint { get; }
        public TownTypeHintViewModel(TownTypeHint townTypeHint)
        {
			BaseHint = townTypeHint;
        }
    }
}
