using System;


namespace HotaRmgTemplateEditor.ViewModels
{
	public class ConnectionViewModel : ViewModelBase
	{
		public List<LinkViewModel> Links { get; set; }

		public ConnectionViewModel()
		{
			Links = [];			
		}
	}
}
