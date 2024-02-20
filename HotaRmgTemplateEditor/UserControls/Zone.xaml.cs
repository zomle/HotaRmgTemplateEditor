using HotaRmgTemplateEditor.Domain.RmgFormat;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HotaRmgTemplateEditor.UserControls
{
	/// <summary>
	/// Interaction logic for Zone.xaml
	/// </summary>
	public partial class Zone : UserControl
	{
		public int ZoneValue
		{
			get { return (int)GetValue(ZoneValueProperty); }
			set { SetValue(ZoneValueProperty, value); }
		}
		public static readonly DependencyProperty ZoneValueProperty =
			DependencyProperty.Register("ZoneValue", typeof(int), typeof(Zone), new PropertyMetadata(55));

		public int ZoneId
		{
			get { return (int)GetValue(ZoneIdProperty); }
			set { SetValue(ZoneIdProperty, value); }
		}
		public static readonly DependencyProperty ZoneIdProperty =
			DependencyProperty.Register("ZoneId", typeof(int), typeof(Zone), new PropertyMetadata(0));

		public ZoneMonsterStrength  MonsterStrength
		{
			get { return (ZoneMonsterStrength)GetValue(MonsterStrengthProperty); }
			set { SetValue(MonsterStrengthProperty, value); }
		}
		public static readonly DependencyProperty MonsterStrengthProperty =
			DependencyProperty.Register("MonsterStrength", typeof(ZoneMonsterStrength), typeof(Zone), new PropertyMetadata(ZoneMonsterStrength.Average));

		public ZoneBackgroundType ZoneType
		{
			get { return (ZoneBackgroundType)GetValue(ZoneTypeProperty); }
			set { SetValue(ZoneTypeProperty, value); }
		}
		public static readonly DependencyProperty ZoneTypeProperty =
			DependencyProperty.Register("ZoneType", typeof(ZoneBackgroundType), typeof(Zone), new PropertyMetadata(ZoneBackgroundType.Player1));

		public Brush BorderColor
		{
			get { return (Brush)GetValue(BorderColorProperty); }
			set { SetValue(BorderColorProperty, value); }
		}
		public static readonly DependencyProperty BorderColorProperty =
			DependencyProperty.Register("BorderBrush", typeof(Brush), typeof(Zone), new PropertyMetadata(Brushes.Black));

		public bool IsMirrorZone { get; set; }
		public Zone? MirrorZone { get; set; }

		public Zone()
		{
			InitializeComponent();
		}
	}
}
