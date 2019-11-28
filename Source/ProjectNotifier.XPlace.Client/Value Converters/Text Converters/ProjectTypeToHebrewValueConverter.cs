namespace ProjectNotifier.XPlace.Client
{
    using ProjectNotifier.XPlace.Core;

    using System;
    using System.Globalization;

    /// <summary>
    /// A value converter that takes a <see cref="ProjectType"/> and returns it's Hebrew equivilant
    /// </summary>
    public class ProjectTypeToHebrewValueConverter : BaseValueConverter<ProjectTypeToHebrewValueConverter>
    {

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
                return ((ProjectType)value).ToHebrewString();
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    };
};
