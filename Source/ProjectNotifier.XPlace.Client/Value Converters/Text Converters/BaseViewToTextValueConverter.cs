namespace ProjectNotifier.XPlace.Client
{
    using System;
    using System.Diagnostics;
    using System.Globalization;


    /// <summary>
    ///A value converter that takes a <see cref="BaseView"/> and returns a string depending on the type
    /// </summary>
    public class BaseViewToTextValueConverter : BaseValueConverter<BaseViewToTextValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case SettingsListView view:
                {
                    return "הגדרות";
                };

                case AppSettingsView view:
                {
                    return "הגדרות אפליקצייה";
                };

                case UserSettingsView view:
                {
                    return "הגדרות משתמש";
                };

                default:
                {
                    Debugger.Break();
                    return null;
                };
            };

        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    };
};
