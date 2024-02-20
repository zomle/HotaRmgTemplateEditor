using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Navigation;

namespace HotaRmgTemplateEditor.Dialogs
{
	/// <summary>
	/// Interaction logic for About.xaml
	/// </summary>
	public partial class About : Window
	{
		public About()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
		{
			var result = MessageBox.Show("Do you want to navigate to the url?", "Open project URL", MessageBoxButton.YesNo, MessageBoxImage.Question);
			if (result == MessageBoxResult.Yes)
			{
				Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
				e.Handled = true;
			}			
		}
	}
}
