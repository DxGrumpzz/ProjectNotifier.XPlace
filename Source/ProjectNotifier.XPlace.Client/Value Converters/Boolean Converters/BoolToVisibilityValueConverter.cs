namespace ProjectNotifier.XPlace.Client
{
	using System;
	using System.Globalization;
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
