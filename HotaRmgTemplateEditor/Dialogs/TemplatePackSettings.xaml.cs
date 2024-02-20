using HotaRmgTemplateEditor.ViewModels;
using System.Windows;


namespace HotaRmgTemplateEditor.Dialogs
{
	/// <summary>
	/// Interaction logic for TemplatePackSettings.xaml
	/// </summary>
	public partial class TemplatePackSettings : Window
	{
		private TemplatePackSettingsViewModel ViewModel { get { return (TemplatePackSettingsViewModel)DataContext; } }

		public TemplatePackSettings()
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
