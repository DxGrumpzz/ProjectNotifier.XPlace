namespace ProjectNotifier.XPlace.Client
{
    using System;
    using System.Globalization;


    /// <summary>
    /// A value converter that takes a boolean value and converts it to an opacity value.
    /// Can take a boolean value to indicate if to invert the values
    /// </summary>
    public class BooleanToOpacityValueConverter : BaseValueConverterT<BooleanToOpacityValueConverter, bool, bool>
    {

        public override object ConvertT(bool value, Type targetType, bool parameter, CultureInfo culture)
        {
            if (parameter == true)
                return value == true ? 0.0 : 1.0;
            else
                return value == true ? 1.0 : 0.0;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }


    };
};
