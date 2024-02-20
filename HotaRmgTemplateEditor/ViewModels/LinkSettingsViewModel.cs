using System.Collections.ObjectModel;

namespace HotaRmgTemplateEditor.ViewModels
{
	public class LinkSettingsViewModel : ViewModelBase
	{
		public ObservableCollection<LinkSettingsItemViewModel> Links { get; set; }

		public LinkSettingsViewModel(ConnectionViewModel connection)
		{
			Links = new ObservableCollection<LinkSettingsItemViewModel>();
			foreach (var link in connection.Links)
			{
				Links.Add(new LinkSettingsItemViewModel(link));
			}
		}
	}

	public class LinkSettingsItemViewModel : ViewModelBase
	{
		private int guardValue;

		public int GuardValue
		{
			get { return guardValue; }
			set { guardValue = value; }
		}
	
		public LinkSettingsItemViewModel(LinkViewModel baseVm)
		{
			GuardValue = 2353;
		}
	}
}
