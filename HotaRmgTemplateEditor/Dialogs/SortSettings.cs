using System.ComponentModel;
using System.Windows.Controls;


namespace HotaRmgTemplateEditor.Dialogs
{
	public class SortSettings
	{
		public GridViewColumnHeader? LastHeaderClicked { get; set; }
		public ListSortDirection LastDirection { get; set; }
		public SortDescription[] SortDescriptions { get; set; }

		public SortSettings()
		{
			SortDescriptions = [];
			LastDirection = ListSortDirection.Ascending;
		}
	}
}
