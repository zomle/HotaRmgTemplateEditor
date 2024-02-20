using HotaRmgTemplateEditor.Dialogs;
using HotaRmgTemplateEditor.Helpers;
using HotaRmgTemplateEditor.ViewModels;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace HotaRmgTemplateEditor
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private MainViewModel ViewModel { get { return (MainViewModel)DataContext; } }
		public ICommand NewTemplatePack { get; set; }
		public MainWindow()
		{
			InitializeComponent();

			NewTemplatePack = new RelayCommand(_ => NewTemplatePackBinding_Executed(this, null));
			DataContext = new MainViewModel(new DialogService());
		}

		private static readonly Regex FormulaRegex = new Regex("([sd])(x|\\d+)_(p|\\d+)", RegexOptions.Compiled);

		private void NewTemplatePackBinding_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			if (ViewModel.IsTemplateModified)
			{
				string messageBoxText = "Do you want to save changes made to the currently open template pack?";

				string caption = string.IsNullOrWhiteSpace(ViewModel.OriginalPath)
					? "New template"
					: "Modified template";

				MessageBoxButton button = MessageBoxButton.YesNoCancel;
				MessageBoxImage icon = MessageBoxImage.Question;
				MessageBoxResult result;

				result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
				if (result == MessageBoxResult.Yes)
				{
					if (string.IsNullOrWhiteSpace(ViewModel.OriginalPath))
					{
						SaveAsTemplatePack(sender, e);
					}
					else
					{
						SaveTemplatePack(sender, e);
					}
				}
				else if (result == MessageBoxResult.Cancel)
				{
					return;
				}
			}

			ViewModel.CreateNewTemplatePack();
		}

		private void OpenTemplatePack(object sender, RoutedEventArgs e)
		{
			var openFileDialog = new OpenFileDialog
			{
				AddToRecent = true,
				Filter = "RMG template files (*.txt)|*.txt|All files (*.*)|*.*",
				Title = "Open RMG template file"
			};

			var result = openFileDialog.ShowDialog() ?? false;
			if (!result)
			{
				return;
			}

			ViewModel.LoadTemplatePack(openFileDialog.FileName);
		}
		private void ImportTemplatePack(object sender, RoutedEventArgs e)
		{
			var openFileDialog = new OpenFileDialog();
			openFileDialog.AddToRecent = true;
			openFileDialog.Filter = "RMG template files (*.txt)|*.txt|All files (*.*)|*.*";
			openFileDialog.Title = "Import RMG template file";
			var result = openFileDialog.ShowDialog() ?? false;
			if (!result)
			{
				return;
			}

			ViewModel.ImportTemplatePack(openFileDialog.FileName);
		}

		private void SaveTemplatePack(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrWhiteSpace(ViewModel.OriginalPath))
			{
				SaveAsTemplatePack(sender, e);
			}
			else
			{
				ViewModel.SaveTemplatePack();
			}
		}

		private void SaveAsTemplatePack(object sender, RoutedEventArgs e)
		{
			var saveFileDialog = new SaveFileDialog
			{
				AddToRecent = true,
				Filter = "RMG template files (*.txt)|*.txt|All files (*.*)|*.*",
				Title = "Open RMG template file",
				FileName = "rmg.txt"
			};

			var result = saveFileDialog.ShowDialog() ?? false;
			if (!result)
			{
				return;
			}

			ViewModel.SaveTemplatePack(saveFileDialog.FileName);
		}

		private void CloseTemplatePack(object sender, RoutedEventArgs e)
		{
			if (ViewModel.IsTemplateModified)
			{
				string messageBoxText = "Do you want to save changes?";

				string caption = string.IsNullOrWhiteSpace(ViewModel.OriginalPath)
					? "New template"
					: "Modified template";

				MessageBoxButton button = MessageBoxButton.YesNoCancel;
				MessageBoxImage icon = MessageBoxImage.Question;
				MessageBoxResult result;

				result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
				if (result == MessageBoxResult.Yes)
				{
					if (string.IsNullOrWhiteSpace(ViewModel.OriginalPath))
					{
						SaveAsTemplatePack(sender, e);
					}
					else
					{
						SaveTemplatePack(sender, e);
					}
				}
				else if (result == MessageBoxResult.Cancel)
				{
					return;
				}
			}

			ViewModel.CreateNewTemplatePack();
		}

		private void TemplateSettingsMenuItem_Click(object sender, RoutedEventArgs e)
		{
			var window = new TemplateSettings();
			var vm = new TemplateSettingsViewModel(ViewModel.TemplatePack.GetBasePack(), ViewModel.TemplatePack.ActiveTemplate.GetBaseTemplate());
			window.DataContext = vm;
			window.Owner = this;
			window.ShowDialog();
		}

		private void TemplatePackSettingsMenuItem_Click(object sender, RoutedEventArgs e)
		{
			var window = new TemplatePackSettings();
			var vm = new TemplatePackSettingsViewModel(ViewModel.TemplatePack.GetBasePack(), ViewModel.TemplatePack.ActiveTemplate.GetBaseTemplate());
			window.DataContext = vm;
			window.Owner = this;
			window.ShowDialog();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			return;
			//var rect = new Rect(canvas.RenderSize);
			//var rtb = new RenderTargetBitmap((int)rect.Right,
			//  (int)rect.Bottom, 96d, 96d, PixelFormats.Default);
			//rtb.Render(canvas);
			////endcode as PNG
			//var pngEncoder = new PngBitmapEncoder();
			//pngEncoder.Frames.Add(BitmapFrame.Create(rtb));
			//
			////save to memory stream
			//using var ms = new MemoryStream();
			//
			//pngEncoder.Save(ms);
			//ms.Close();
			//System.IO.File.WriteAllBytes("c:\\tmp\\test.png", ms.ToArray());
			//Console.WriteLine("Done");
		}

		private void AboutMenuItem_Click(object sender, RoutedEventArgs e)
		{
			var window = new About
			{
				Owner = this
			};
			window.ShowDialog();
		}

		private void ZoomInButton_Click(object sender, RoutedEventArgs e)
		{
			templateControl.ZoomIn();
		}

		private void ZoomOutButton_Click(object sender, RoutedEventArgs e)
		{
			templateControl.ZoomOut();
		}

		private void ZoomToFitButton_Click(object sender, RoutedEventArgs e)
		{
			templateControl.ZoomToFit();
		}
	}
}