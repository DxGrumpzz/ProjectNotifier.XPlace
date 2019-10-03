namespace ProjectNotifier.XPlace.Client
{
    using System;
    using System.Globalization;
    using System.Windows;

    /// <summary>
    /// 
    /// </summary>
    public class BoolToVisibilityCollapsedValueConverter : BaseValueConverterT<BoolToVisibilityCollapsedValueConverter, bool, bool>
    {

        public override object ConvertT(bool value, Type targetType, bool parameter, CultureInfo culture)
        {
            if (parameter == true)
                return value == true ? Visibility.Collapsed : Visibility.Visible;
            else
                return value == true ? Visibility.Visible : Visibility.Collapsed;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    };
};
