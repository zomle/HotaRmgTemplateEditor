using System.Globalization;
using System.Windows.Data;

namespace HotaRmgTemplateEditor.Helpers
{
	[ValueConversion(typeof(Enum), typeof(string))]
	public class EnumNameConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return ((Enum)value).ToString();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var str = (string)value;
			return Enum.Parse(targetType, str, true);
		}
	}
}
