using HotaRmgTemplateEditor.Domain.RmgFormat;

namespace HotaRmgTemplateEditor.ViewModels
{
	public class TemplateViewModel : ViewModelBase
	{
		private IDialogService DialogService { get; }
		private Template BaseTemplate { get; }

		private double currentScale;
		public double CurrentScale
		{
			get { return currentScale; }
			set { currentScale = value; NotifyPropertyChanged(); }
		}

		public TemplateViewModel(IDialogService dialogService, Template baseTemplate)
		{
			DialogService = dialogService;
			BaseTemplate = baseTemplate;
			CurrentScale = double.MinValue;
		}

		public void ZoomIn()
		{
			CurrentScale *= Constants.ZoomInMultiplier;
		}

		public void ZoomOut()
		{
			CurrentScale *= Constants.ZoomOutMultiplier;
		}

		public Template GetBaseTemplate()
		{
			return BaseTemplate;
		}
	}
}
