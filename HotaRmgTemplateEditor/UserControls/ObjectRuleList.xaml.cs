using HotaRmgTemplateEditor.ViewModels;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;


namespace HotaRmgTemplateEditor.UserControls
{
	/// <summary>
	/// Interaction logic for ObjectRuleList.xaml
	/// </summary>
	public partial class ObjectRuleList : UserControl
	{
		public ObservableCollection<ObjectRuleItemViewModel> RuleItems
		{
			get { return (ObservableCollection<ObjectRuleItemViewModel>)GetValue(RuleItemsProperty); }
			set { SetValue(RuleItemsProperty, value); }
		}
		public static readonly DependencyProperty RuleItemsProperty =
			DependencyProperty.Register("RuleItems", typeof(ObservableCollection<ObjectRuleItemViewModel>), typeof(ObjectRuleList), new PropertyMetadata(new ObservableCollection<ObjectRuleItemViewModel>()));


		public ObjectRuleList()
		{
			InitializeComponent();
		}
	}
}
