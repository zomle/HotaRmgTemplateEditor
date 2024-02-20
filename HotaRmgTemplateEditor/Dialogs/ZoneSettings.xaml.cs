using HotaRmgTemplateEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HotaRmgTemplateEditor.Dialogs
{
	/// <summary>
	/// Interaction logic for ZoneSettings.xaml
	/// </summary>
	public partial class ZoneSettings : Window
	{
		private ZoneSettingsViewModel ViewModel { get { return (ZoneSettingsViewModel)DataContext; } }


		public ZoneSettings()
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
