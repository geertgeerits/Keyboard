using System.Diagnostics;

namespace Keyboard
{
    internal class ClassKeyboardMethods
    {
        // Global variables
        public static readonly string cImageKeyboardHideDark = "keyboard_hide_32p_white.png";
        public static readonly string cImageKeyboardHideLight = "keyboard_hide_32p_black.png";
        public static readonly string cImageKeyboardShowDark = "keyboard_32p_white.png";
        public static readonly string cImageKeyboardShowLight = "keyboard_32p_black.png";
        public static bool bKeyboardToggleButton = true;

        /// <summary>
        /// Get the current device orientation
        /// </summary>
        /// <returns></returns>
        public static string GetDeviceOrientation()
        {
            // Get the current display information
            var displayInfo = DeviceDisplay.MainDisplayInfo;

            // Return the orientation, ensuring a non-null value
            Debug.WriteLine($"DisplayOrientation: {displayInfo.Orientation}");
            return displayInfo.Orientation.ToString() ?? "Unknown";
        }

        /// <summary>
        /// Handles the click event for the decimal keyboard buttons
        /// </summary>
        /// <param name="cKey"></param>
        public void KeyboardDecimalClicked(Entry focusedEntry, string cKey)
        {
            if (focusedEntry != null)
            {
                switch (cKey)
                {
                    case "btnBackspace":
                        focusedEntry.Text = DeleteCharacterBeforeCursor(focusedEntry);
                        break;
                    case "btnMinus":
                        if (!focusedEntry.Text.Contains(ClassEntryMethods.cNumNegativeSign))
                        {
                            focusedEntry.Text = ClassEntryMethods.cNumNegativeSign + focusedEntry.Text;
                        }
                        break;
                    case "btnDecimalPoint":
                        if (!focusedEntry.Text.Contains(ClassEntryMethods.cNumDecimalSeparator))
                        {
                            focusedEntry.Text = InsertCharacterInEntryField(focusedEntry, ClassEntryMethods.cNumDecimalSeparator);
                        }
                        break;
                    default:
                        focusedEntry.Text = InsertCharacterInEntryField(focusedEntry, cKey);
                        break;
                }
            }
        }

        /// <summary>
        /// Handles the click event for the hexadecimal keyboard buttons
        /// </summary>
        /// <param name="cKey"></param>
        public void KeyboardHexadecimalClicked(Entry focusedEntry, string cKey)
        {
            if (focusedEntry != null)
            {
                switch (cKey)
                {
                    case "btnBackspace":
                        focusedEntry.Text = DeleteCharacterBeforeCursor(focusedEntry);
                        break;
                    default:
                        focusedEntry.Text = InsertCharacterInEntryField(focusedEntry, cKey);
                        break;
                }
            }
        }

        /// <summary>
        /// Inserts a character at the current cursor position in the Entry field.
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="cCharacter"></param>
        /// <returns></returns>
        public static string InsertCharacterInEntryField(Entry entry, string cCharacter)
        {
            // Get the current text in the Entry
            string currentText = entry.Text ?? string.Empty;

            // Insert the character at the cursor position
            string newText = currentText.Insert(entry.CursorPosition, cCharacter);

            // Update the Entry's text
            entry.Text = newText;

            // Move the cursor to the position after the inserted character
            entry.CursorPosition++;

            return newText;
        }

        /// <summary>
        /// Deletes the character before the current cursor position in the Entry field.
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public static string DeleteCharacterBeforeCursor(Entry entry)
        {
            // Get the current text in the Entry
            string currentText = entry.Text ?? string.Empty;

            // Ensure there is a character to delete and the cursor is not at the start
            if (entry.CursorPosition > 0 && currentText.Length > 0)
            {
                // Remove the character before the cursor position
                string newText = currentText.Remove(entry.CursorPosition - 1, 1);

                // Update the Entry's text
                entry.Text = newText;

                // Move the cursor to the position before the deleted character
                //entry.CursorPosition--;

                return newText;
            }

            return currentText;
        }
    }
}
