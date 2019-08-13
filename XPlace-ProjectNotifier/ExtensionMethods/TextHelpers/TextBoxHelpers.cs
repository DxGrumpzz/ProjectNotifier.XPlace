namespace XPlace_ProjectNotifier
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
                    // Remove that character
                    textBox.Text = textBox.Text.Remove(a, 1);

                    // Sets the caret index to the correct position
                    // if the index of the caret is 0, set it a 0 as to not cause an exception
                    // If the index is bigger than 0 remove 1 to save the location after deleting 1 character
                    textBox.CaretIndex = (textBox.CaretIndex == 0) ? 0 : textBox.CaretIndex - 1;

                    // Returns false because text contains invalid characters
                    return false;
                };
            };
            // Returns true because the text was valid
            return true;
        }
    }
}
