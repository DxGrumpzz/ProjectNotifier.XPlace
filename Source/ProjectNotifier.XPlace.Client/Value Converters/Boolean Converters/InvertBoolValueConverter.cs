namespace ProjectNotifier.XPlace.Client
{
    using System;
    using System.Globalization;

    /// <summary>
    /// 
    /// </summary>
    public class InvertBoolValueConverter : BaseValueConverter<InvertBoolValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    };
};
