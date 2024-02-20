using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Windows.Data;

namespace HotaRmgTemplateEditor.Helpers
{
	[ValueConversion(typeof(bool), typeof(bool))]
	public class InverseBooleanConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (targetType != typeof(bool))
			{
				throw new InvalidOperationException("The target must be a boolean.");
			}

			var boolVal = (bool)value;

			return !boolVal;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}
}
