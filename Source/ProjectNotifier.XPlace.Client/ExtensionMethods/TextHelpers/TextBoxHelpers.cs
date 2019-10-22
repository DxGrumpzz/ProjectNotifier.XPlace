namespace ProjectNotifier.XPlace.Client
{
    using System.Windows.Controls;
    using System.Text.RegularExpressions;

    public static class TextBoxHelpers
    {
        /// <summary>
        /// A helper method to check if the text is valid or contains illigal characters
        /// </summary>
        /// <param name="textBox"> The text box as sender object </param>
        /// <param name="regexPattern"> The pattern to detect the the characters </param>
        /// <returns> Returns true of false whether the text contaned any illegal characters </returns>
        public static bool IsTextValid(this TextBox textBox, string regexPattern)
        {

			// Loops through the text
			for (int a = 0; a < textBox.Text.Length; a++)
            {
                // If the character matches the regex pattern
                if (Regex.IsMatch(textBox.Text[a].ToString(), regexPattern) == false)
                {
					// Returns false because text contains invalid characters
					return false;
                };
            };

            // Returns true because the text was valid
            return true;
        }


        /// <summary>
        /// A helper method that removes any characters deemed "unnecesarry" or illegal  by the <paramref name="regexPattern"/>
        /// </summary>
        /// <param name="textBox"> The text box as sender object </param>
        /// <param name="regexPattern"> The pattern to detect the the characters </param>
        /// <returns> Returns true of false whether the text contaned any illegal characters </returns>
        public static void ValidateText(this TextBox textBox, string regexPattern)
        {
            // Loops through the text
            for (int a = 0; a < textBox.Text.Length; a++)
            {
                // If the character matches the regex pattern
                if (Regex.IsMatch(textBox.Text[a].ToString(), regexPattern) == false)
                {
                    // Remove that character
                    textBox.Text = textBox.Text.Remove(a, 1);

                    // Sets the caret index to the correct position
                    textBox.CaretIndex = textBox.Text.Length;
                };
            };
        }
    }
}
