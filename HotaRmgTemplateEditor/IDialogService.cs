using HotaRmgTemplateEditor.Dialogs;
using HotaRmgTemplateEditor.ViewModels;
using System;

namespace HotaRmgTemplateEditor
{
	public interface IDialogService
	{
		bool? ShowTownTypeHintDialog(AddEditTownTypeHintViewModel vm);
		bool? ShowEditZoneSettingsDialog(ZoneSettingsViewModel vm);
	}

	public class DialogService : IDialogService
	{
		public bool? ShowEditZoneSettingsDialog(ZoneSettingsViewModel vm)
		{
			var s = new ZoneSettings
			{
				DataContext = vm
			};
			return s.ShowDialog();
		}

		public bool? ShowTownTypeHintDialog(AddEditTownTypeHintViewModel vm)
		{
			var dialog = new AddEditTownTypeHint()
			{
				DataContext = vm
			};
			return dialog.ShowDialog();
		}
	}
}
