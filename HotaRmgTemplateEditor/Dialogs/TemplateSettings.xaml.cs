using HotaRmgTemplateEditor.ViewModels;
using System.Windows;

namespace HotaRmgTemplateEditor.Dialogs
{
	/// <summary>
	/// Interaction logic for TemplateSettings.xaml
	/// </summary>
	public partial class TemplateSettings : Window
	{
		private TemplateSettingsViewModel ViewModel { get { return (TemplateSettingsViewModel)DataContext; } }

		public TemplateSettings()
		{
			InitializeComponent();
		}

		private void OkButton_Click(object sender, RoutedEventArgs e)
		{
			ViewModel.SaveChanges();
			Close();
        }

		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}
