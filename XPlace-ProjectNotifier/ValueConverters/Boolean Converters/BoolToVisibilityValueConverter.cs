namespace XPlace_ProjectNotifier
{
	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Text;
    using System.Windows;


    /// <summary>
    /// Convert a Boolean value to visibility 
    /// </summary>
    public class BoolToVisibilityValueConverter : BaseValueConverter<BoolToVisibilityValueConverter>
	{
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return (bool)value == true ? Visibility.Visible : Visibility.Hidden;
		}

		public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	};
}
