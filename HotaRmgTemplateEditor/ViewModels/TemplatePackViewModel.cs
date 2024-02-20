using HotaRmgTemplateEditor.Domain.RmgFormat;
using System.Collections.ObjectModel;

namespace HotaRmgTemplateEditor.ViewModels
{
	public class TemplatePackViewModel : ViewModelBase
	{
		public ObservableCollection<TemplateViewModel> Templates { get; set; }

		private TemplateViewModel activeTemplate;
		public TemplateViewModel ActiveTemplate
		{
			get { return activeTemplate; }
			set { activeTemplate = value; NotifyPropertyChanged(); }
		}

		private TemplatePack BaseTemplatePack { get; }

		private IDialogService DialogService { get; }
		public TemplatePackViewModel(IDialogService dialogService, TemplatePack baseTemplatePack)
		{
			DialogService = dialogService;
			BaseTemplatePack = baseTemplatePack;

			Templates = [];

			foreach (var template in BaseTemplatePack.Templates)
			{
				var newTemplateVm = new TemplateViewModel(DialogService, template);

				ActiveTemplate ??= newTemplateVm;

				Templates.Add(newTemplateVm);
			}
		}

		public TemplatePack GetBasePack()
		{
			return BaseTemplatePack;
		}

		public void AddNewTemplate()
		{
			var newTemplateVm = new TemplateViewModel(DialogService, new Template(BaseTemplatePack));

			ActiveTemplate = newTemplateVm;

			Templates.Add(newTemplateVm);
		}

		public void AddTemplate(Template template)
		{
			var newTemplateVm = new TemplateViewModel(DialogService, template);

			Templates.Add(newTemplateVm);
		}
	}
}
