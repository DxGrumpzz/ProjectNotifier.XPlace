namespace ProjectNotifier.XPlace.Client
{
	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Text;


	/// <summary>
	/// Increments an index or a count by some number
	/// </summary>
	public class IncrementNumberValueConverter : BaseValueConverter<IncrementNumberValueConverter>
	{
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return (int)value + System.Convert.ToInt32(parameter);
		}

		public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	};
}
