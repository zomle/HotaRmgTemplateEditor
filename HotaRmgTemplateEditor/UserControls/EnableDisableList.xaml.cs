using HotaRmgTemplateEditor.Dialogs;
using HotaRmgTemplateEditor.Helpers;
using HotaRmgTemplateEditor.ViewModels;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace HotaRmgTemplateEditor.UserControls
{
	/// <summary>
	/// Interaction logic for EnableDisableList.xaml
	/// </summary>
	public partial class EnableDisableList : UserControl
	{
		public ViewBase View
		{
			get { return (ViewBase)GetValue(ViewProperty); }
			set { SetValue(ViewProperty, value); }
		}
		public static readonly DependencyProperty ViewProperty =
			DependencyProperty.Register("View", typeof(ViewBase), typeof(EnableDisableList), new PropertyMetadata(null, View_Changed));

		public ObservableCollection<EnableDisableItemViewModel> DisabledItems
		{
			get { return (ObservableCollection<EnableDisableItemViewModel>)GetValue(DisabledItemsProperty); }
			set { SetValue(DisabledItemsProperty, value); }
		}
		public static readonly DependencyProperty DisabledItemsProperty =
			DependencyProperty.Register("DisabledItems", typeof(ObservableCollection<EnableDisableItemViewModel>), typeof(EnableDisableList), new PropertyMetadata(null));

		public ObservableCollection<EnableDisableItemViewModel> DefaultItems
		{
			get { return (ObservableCollection<EnableDisableItemViewModel>)GetValue(DefaultItemsProperty); }
			set { SetValue(DefaultItemsProperty, value); }
		}
		public static readonly DependencyProperty DefaultItemsProperty =
			DependencyProperty.Register("DefaultItems", typeof(ObservableCollection<EnableDisableItemViewModel>), typeof(EnableDisableList), new PropertyMetadata(null));

		public ObservableCollection<EnableDisableItemViewModel> EnabledItems
		{
			get { return (ObservableCollection<EnableDisableItemViewModel>)GetValue(EnabledItemsProperty); }
			set { SetValue(EnabledItemsProperty, value); }
		}
		public static readonly DependencyProperty EnabledItemsProperty =
			DependencyProperty.Register("EnabledItems", typeof(ObservableCollection<EnableDisableItemViewModel>), typeof(EnableDisableList), new PropertyMetadata(null));


		public RelayCommand DefaultToDisabledItemsCommand { get; }
		public RelayCommand DisabledToDefaultItemsCommand { get; }
		public RelayCommand DefaultToEnabledItemsCommand { get; }
		public RelayCommand EnabledToDefaultItemsCommand { get; }

		private Dictionary<ListView, SortSettings> ListSortSettings { get; set; }
		private Dictionary<string, int> ColumnHeaderIndices { get; set; }

		public EnableDisableList()
		{
			ListSortSettings = [];
			ColumnHeaderIndices = [];

			DefaultToDisabledItemsCommand = new RelayCommand(lst =>
			{
				MoveSelectedItems(DefaultItems, DisabledItems, lst as IList);
				ResortList(lvDisabledItems);
			});

			DisabledToDefaultItemsCommand = new RelayCommand(lst =>
			{
				MoveSelectedItems(DisabledItems, DefaultItems, lst as IList);
				ResortList(lvDefaultItems);
			});

			DefaultToEnabledItemsCommand = new RelayCommand(lst =>
			{
				MoveSelectedItems(DefaultItems, EnabledItems, lst as IList);
				ResortList(lvEnabledItems);
			});

			EnabledToDefaultItemsCommand = new RelayCommand(lst =>
			{
				MoveSelectedItems(EnabledItems, DefaultItems, lst as IList);
				ResortList(lvDefaultItems);
			});

			InitializeComponent();
		}

		private void MoveSelectedItems(ObservableCollection<EnableDisableItemViewModel> source, ObservableCollection<EnableDisableItemViewModel> destination, IList? selectedItems)
		{
			if (selectedItems == null)
			{
				return;
			}

			var vms = selectedItems.Cast<EnableDisableItemViewModel>().ToList();
			foreach (var vm in vms)
			{
				source.Remove(vm);
				destination.Add(vm);
			}
		}

		private static void View_Changed(DependencyObject o, DependencyPropertyChangedEventArgs args)
		{
			var lst = (EnableDisableList)o;

			lst.lvDisabledItems.View = XamlHelper.CloneXamlObject<GridView>(args.NewValue);
			lst.lvDefaultItems.View = XamlHelper.CloneXamlObject<GridView>(args.NewValue);
			lst.lvEnabledItems.View = XamlHelper.CloneXamlObject<GridView>(args.NewValue);

			lst.SetupDefaultSortSettings((GridView)args.NewValue);
		}

		private void SetupDefaultSortSettings(GridView gridView)
		{
			if (gridView.Columns.Count == 0)
			{
				return;
			}

			if (gridView.Columns[^1].Header is not GridViewColumnHeader lastGvHeader)
			{
				throw new InvalidOperationException("Last column header should be of type GridViewColumnHeader.");
			}

			var defaultSortDescription = new SortDescription(lastGvHeader.Tag as string, ListSortDirection.Ascending);
			ListSortSettings = new Dictionary<ListView, SortSettings>
			{
				{ lvDisabledItems, new SortSettings { SortDescriptions = [defaultSortDescription] } },
				{  lvDefaultItems, new SortSettings { SortDescriptions = [defaultSortDescription] } },
				{  lvEnabledItems, new SortSettings { SortDescriptions = [defaultSortDescription] } }
			};

			ColumnHeaderIndices = [];
			for (int i = 0; i < gridView.Columns.Count; i++)
			{
				var column = gridView.Columns[i];
				var header = (GridViewColumnHeader)column.Header;

				if (header?.Tag is string tag)
				{
					ColumnHeaderIndices[tag] = i;
				}
			}
		}

		private void ColumnHeader_Click(object sender, RoutedEventArgs e)
		{
			if (e.OriginalSource is not GridViewColumnHeader headerClicked)
			{
				return;
			}

			if (headerClicked.Tag is not string tag)
			{
				return;
			}

			if (headerClicked.Role == GridViewColumnHeaderRole.Padding)
			{
				return;
			}

			var listView = (ListView)sender;
			var settings = ListSortSettings[listView];

			var gridView = (GridView)listView.View;
			
			var clickedIndex = ColumnHeaderIndices[tag];
			bool isLastHeaderClicked = clickedIndex == gridView.Columns.Count - 1;

			ListSortDirection direction;
			if (settings.LastHeaderClicked == null && isLastHeaderClicked)
			{
				direction = ListSortDirection.Descending;
			}
			else if (headerClicked != settings.LastHeaderClicked)
			{
				direction = ListSortDirection.Ascending;
			}
			else
			{
				direction = settings.LastDirection == ListSortDirection.Ascending
					? ListSortDirection.Descending
					: ListSortDirection.Ascending;
			}

			var tmpSdList = new List<SortDescription>();
			for (int i = clickedIndex; i < gridView.Columns.Count; ++i)
			{
				var colHeader = gridView.Columns[i].Header as GridViewColumnHeader;
				var sd = new SortDescription(colHeader?.Tag as string, i == clickedIndex ? direction : ListSortDirection.Ascending);
				tmpSdList.Add(sd);
			}
			settings.SortDescriptions = tmpSdList.ToArray();
			Sort(listView, settings);

			settings.LastHeaderClicked = headerClicked;
			settings.LastDirection = direction;
		}

		private void ResortList(ListView listView)
		{
			var settings = ListSortSettings[listView];
			Sort(listView, settings);
		}

		private static void Sort(ListView lv, SortSettings settings)
		{
			var dataView = CollectionViewSource.GetDefaultView(lv.ItemsSource);

			dataView.SortDescriptions.Clear();
			foreach (var desc in settings.SortDescriptions)
			{
				dataView.SortDescriptions.Add(desc);
			}
			dataView.Refresh();
		}

		private void ListView_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			if (sender is not ListView listView)
			{
				return;
			}

			if (listView.View is not GridView gridView)
			{
				return;
			}

			if (gridView.Columns.Count == 0)
			{
				return;
			}

			var actualWidth = listView.ActualWidth - SystemParameters.VerticalScrollBarWidth;
			for (int i = 0; i < gridView.Columns.Count - 1; i++)
			{
				actualWidth -= gridView.Columns[i].ActualWidth;
			}
			gridView.Columns[^1].Width = actualWidth;
		}
	}
}
