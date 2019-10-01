namespace ProjectNotifier.XPlace.Client
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Weird name, it takes the width and height of a <see cref="Border"/> and rounds it's bottom
    /// </summary>
    public class SizeToBottomCornerRadiusValueConverter : BaseValueConverter<SizeToBottomCornerRadiusValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new CornerRadius(0, 0, (double)value/2, (double)value/2);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    };
};
