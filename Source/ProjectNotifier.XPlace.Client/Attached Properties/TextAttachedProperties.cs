namespace ProjectNotifier.XPlace.Client
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
            if (!(sender is TextBox textBox))
                return;

            textBox.TextChanged += (sender, _e) =>
            {
                textBox.ValidateText(NUMERIC_REGEX_PATTERN);
                
                // If textbox is marked as numeric only
                if ((bool)e.NewValue == true)
                {
                    // And the user deleted every character
                    if (textBox.Text.Length == 0)
                    {
                        // Set the default text to 0
                        textBox.Text = "0";

                        // Move caret to the end
                        textBox.CaretIndex = 1;
                    };
                }
            };
        }
    };
};
