namespace XPlace_ProjectNotifier
{
	using System.Windows;
	using System.Windows.Controls;

	/// <summary>
	/// Allows only numeric characters to be accepted by a <see cref="TextBox"/>
	/// </summary>
	public class IsOnlyNumericTextBlock : BaseAttachedProperty<IsOnlyNumericTextBlock, bool>
	{
		private const string NUMERIC_REGEX_PATTERN = "[0-9]";

		public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			// Validation
			if(!(sender is TextBox textBox))
				return;

			textBox.TextChanged += (sender, _e) =>
			{
				textBox.IsTextValid(NUMERIC_REGEX_PATTERN);
			};
		}
	};
};
