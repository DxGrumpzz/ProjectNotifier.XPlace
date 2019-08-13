namespace XPlace_ProjectNotifier
{
	using System;
	using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;
	using System.Windows;
	using System.Windows.Controls;


	/// <summary>
	/// 
	/// </summary>
	public class IsOnlyNumericTextBlock
	{
		private const string NUMERIC_REGEX_PATTERN = "[0-9]";

		public static bool GetValue(DependencyObject obj)
		{
			return (bool)obj.GetValue(MyPropertyProperty);
		}

		public static void SetValue(DependencyObject obj, bool value)
		{
			obj.SetValue(MyPropertyProperty, value);
		}


		public static readonly DependencyProperty MyPropertyProperty =
			DependencyProperty.RegisterAttached("Value",
				typeof(bool),
				typeof(IsOnlyNumericTextBlock),
				new PropertyMetadata(false, new PropertyChangedCallback(ValueCallback)));

		private static void ValueCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			// Validation
			if(!(d is TextBox textBox))
				return;

			textBox.TextChanged += (sender, _e) =>
			{
				textBox.IsTextValid(NUMERIC_REGEX_PATTERN);
			};
		}


	};
}
