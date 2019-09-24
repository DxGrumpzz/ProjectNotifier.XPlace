namespace ProjectNotifier.XPlace.Client
{
    using System;
    using System.Diagnostics;
    using System.Globalization;

    /// <summary>
    /// 
    /// </summary>
    public class FirstViewPageValueConverter : TestBaseValueConverter<FirstViewPageValueConverter, FirstViewViews, BaseViewModel>
    {

        public override object ConvertT(FirstViewViews value, Type targetType, BaseViewModel parameter, CultureInfo culture)
        {
            Debugger.Break();

            return new object();
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    };
};