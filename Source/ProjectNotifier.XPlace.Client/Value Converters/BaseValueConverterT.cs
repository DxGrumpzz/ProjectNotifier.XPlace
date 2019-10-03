namespace ProjectNotifier.XPlace.Client
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Markup;

    /// <summary>
    /// A base ValueConverter for all <see cref="IValueConverter"/> classes to inherit.
    /// Allows for a more simple implmentation in Xaml
    /// </summary>
    /// <typeparam name="TParent"> The type of class or ValueConverter </typeparam>
    public abstract class BaseValueConverterT<TParent, TValue, TParam> : MarkupExtension, IValueConverter
        where TParent : class, new()
    {

        /// <summary>
        /// A single static instance of this value converter
        /// </summary>
        private static TParent Converter = null;


        /// <summary>
        /// Converts a value into another value with a possibility to pass a specifc value with the parameter
        /// </summary>
        /// <param name="value"> The value to convert </param>
        /// <param name="targetType"></param>
        /// <param name="parameter"> </param>
        /// <param name="culture"></param>
        /// <returns> Returns an <see cref="object"/> that can be cast into any other object </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Convert value and parameter to correct types
            TParam newParamValue = (TParam)System.Convert.ChangeType(parameter, typeof(TParam));
            TValue newValue = (TValue)System.Convert.ChangeType(value, typeof(TValue));

            // If one of the converted values is null, Break
            if ((newParamValue is null) || (newValue is null))
                Debugger.Break();

            return ConvertT(newValue, targetType, newParamValue, culture);
        }


        public abstract object ConvertT(TValue value, Type targetType, TParam parameter, CultureInfo culture);

        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);



        /// <summary>
        /// Provides a static instance of the value converter 
        /// </summary>
        /// <param name="serviceProvider">The service provider</param>
        /// <returns></returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            // If the converter instance is null create a new type of what ever type passed in the type argument
            return Converter ?? (Converter = new TParent());
        }

    };
};