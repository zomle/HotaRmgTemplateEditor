using HotaRmgTemplateEditor.Domain.RmgFormat;
using HotaRmgTemplateEditor.Domain.RmgFormat.Handler;
using HotaRmgTemplateEditor.Helpers;
using System.IO;

namespace HotaRmgTemplateEditor.ViewModels
{
	public class MainViewModel : ViewModelBase
	{
		private TemplatePackViewModel templatePack;
		public TemplatePackViewModel TemplatePack
		{
			get { return templatePack; }
			set { templatePack = value; NotifyPropertyChanged(); }
		}

		public RelayCommand NewTemplatePackCommand { get; set; }
		public RelayCommand AddTemplateCommand { get; set; }
		
		public bool IsTemplateModified { get; set; }
		public string? OriginalPath { get; set; }
		private IDialogService DialogService { get; }

		public MainViewModel(IDialogService dialogService)
		{
			DialogService = dialogService;

			CreateNewTemplatePack();

			NewTemplatePackCommand = new RelayCommand(_ => CreateNewTemplatePack());
			AddTemplateCommand = new RelayCommand(_ => AddNewTemplate());
		}

		public void LoadTemplatePack(string fileName)
		{
			var reader = new TemplatePackReader();
			var pack = TemplatePackReader.LoadFile(fileName);

			TemplatePack = new TemplatePackViewModel(DialogService, pack);
		}

		public void ImportTemplatePack(string fileName)
		{
			var reader = new TemplatePackReader();
			var pack = TemplatePackReader.LoadFile(fileName);

			foreach (var template in pack.Templates)
			{
				TemplatePack?.AddTemplate(template);
			}
		}

		public void CreateNewTemplatePack()
		{
			var newPack = new TemplatePack();
			TemplatePack = new TemplatePackViewModel(DialogService, newPack);
		}

		public void SaveTemplatePack(string? filePath = null)
		{
			if (TemplatePack == null)
			{
				throw new InvalidOperationException();
			}

			if (filePath == null && string.IsNullOrWhiteSpace(OriginalPath))
			{
				throw new InvalidOperationException("The template pack hasn't been saved yet, and no target file name was provided.");
			}

			filePath ??= OriginalPath;

			var writer = new TemplatePackWriter();
			var content = TemplatePackWriter.WriteFile(TemplatePack.GetBasePack());
			File.WriteAllText(filePath, content);
		}

		private void AddNewTemplate()
		{
			TemplatePack?.AddNewTemplate();
		}
	}
}
