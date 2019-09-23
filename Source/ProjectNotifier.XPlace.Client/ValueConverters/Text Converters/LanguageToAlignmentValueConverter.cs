namespace ProjectNotifier.XPlace.Client
{
    using System;
    using System.Globalization;
    using System.Text.RegularExpressions;
    using System.Windows;

    public class LanguageToAlignmentValueConverter : BaseValueConverter<LanguageToAlignmentValueConverter>
    {

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Gets the text as a string
            string text = (string)value;

            // Checks if the string is empty null or contains only white spaces
            if (string.IsNullOrWhiteSpace(text))
                // Returns the default value for the control which is right to left because Israel
                return HorizontalAlignment.Right;

            // Checks if the first characters matches the pattern 
            bool regexMatch = Regex.Match(text[0].ToString(), @"[\u0590-\u05FF]").Success;


            if(regexMatch == true)
            {
                return HorizontalAlignment.Left;
            }
            else
            {
                return HorizontalAlignment.Right;
            };
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
