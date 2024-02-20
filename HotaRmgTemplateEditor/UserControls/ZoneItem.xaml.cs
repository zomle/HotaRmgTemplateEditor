using System;
using System.Windows;
using System.Windows.Controls;

namespace HotaRmgTemplateEditor.UserControls
{
	/// <summary>
	/// Interaction logic for ZoneItem.xaml
	/// </summary>
	public partial class ZoneItem : UserControl
	{
		public bool HasNonZeroAmount
		{
			get { return (bool)GetValue(HasNonZeroAmountPropertyKey.DependencyProperty); }
			private set { SetValue(HasNonZeroAmountPropertyKey, value); }
		}
		
		private static readonly DependencyPropertyKey HasNonZeroAmountPropertyKey
			= DependencyProperty.RegisterReadOnly(
				"HasNonZeroAmount",
				typeof(bool),
				typeof(ZoneItem),
				new PropertyMetadata(false));

		public static readonly DependencyProperty HasNonZeroAmountProperty = HasNonZeroAmountPropertyKey.DependencyProperty;

		public int Amount
		{
			get { return (int)GetValue(AmountProperty); }
			set
			{
				SetValue(AmountProperty, value);
				HasNonZeroAmount = value != 0;
			}
		}

		public static readonly DependencyProperty AmountProperty
			= DependencyProperty.Register(
				"Amount",
				typeof(int),
				typeof(ZoneItem),
				new PropertyMetadata(0, AmountChanged));
		
		private static void AmountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			d.SetValue(HasNonZeroAmountPropertyKey, (int)e.NewValue != 0);
		}

		public ZoneItemType ItemType
		{
			get { return (ZoneItemType)GetValue(ItemTypeProperty); }
			set { SetValue(ItemTypeProperty, value); }
		}
		public static readonly DependencyProperty ItemTypeProperty
			= DependencyProperty.Register(
				"ItemType",
				typeof(ZoneItemType),
				typeof(ZoneItem),
				new PropertyMetadata(ZoneItemType.ZoneValue));

		public ZoneItem()
		{
			InitializeComponent();
		}
	}
}
