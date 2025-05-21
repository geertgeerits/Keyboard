using Plugin.Maui.BottomSheet;
using System.Diagnostics;

namespace Keyboard
{
    internal static class ClassKeyboardMethods
    {
        // Default value for keyboard toggle button
        public static bool bKeyboardToggleButton = true;

        // Image source for the keyboard toggle button
        private static readonly string cImageKeyboardHideDark = "keyboard_hide_32p_white.png";  // Dark theme image
        private static readonly string cImageKeyboardHideLight = "keyboard_hide_32p_black.png"; // Light theme image
        private static readonly string cImageKeyboardShowDark = "keyboard_32p_white.png";       // Dark theme image
        private static readonly string cImageKeyboardShowLight = "keyboard_32p_black.png";      // Light theme image

        // Default theme for the application (Light, Dark, System)
        private static string cTheme = "System";

        /// <summary>
        /// Set the theme
        /// </summary>
        public static void SetTheme()
        {
            Application.Current!.UserAppTheme = cTheme switch
            {
                "Light" => AppTheme.Light,
                "Dark" => AppTheme.Dark,
                _ => AppTheme.Unspecified,
            };
        }

        /// <summary>
        /// Event when the keyboard bottom sheet is opened
        /// </summary>
        /// <param name="imageButton">The source of the event.</param>
        public static void KeyboardBottomSheetOpened(ImageButton imageButton)
        {
            // Set the image source for the keyboard toggle button depending on the theme when the keyboard is opened
            if (bKeyboardToggleButton)
            {
                imageButton.Source = Application.Current?.RequestedTheme switch
                {
                    AppTheme.Dark => (ImageSource)cImageKeyboardHideDark,
                    _ => (ImageSource)cImageKeyboardHideLight,
                };
            }
        }

        /// <summary>
        /// Event when the keyboard bottom sheet is closed
        /// </summary>
        /// <param name="imageButton"></param>
        public static void KeyboardBottomSheetClosed(ImageButton imageButton)
        {
            // Set the image source for the keyboard toggle button depending on the theme when the keyboard is closed
            if (bKeyboardToggleButton)
            {
                imageButton.Source = Application.Current?.RequestedTheme switch
                {
                    AppTheme.Dark => (ImageSource)cImageKeyboardShowDark,
                    _ => (ImageSource)cImageKeyboardShowLight,
                };
            }
        }

        /// <summary>
        /// Get the current device orientation
        /// </summary>
        /// <returns></returns>
        private static string GetDeviceOrientation()
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
        public static void KeyboardDecimalClicked(Entry focusedEntry, string cKey)
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
        public static void KeyboardHexadecimalClicked(Entry focusedEntry, string cKey)
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
        private static string InsertCharacterInEntryField(Entry entry, string cCharacter)
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
        private static string DeleteCharacterBeforeCursor(Entry entry)
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

        /// <summary>
        /// Toggles the visibility of the numeric keyboard based on the current device orientation and theme.
        /// </summary>
        /// <param name="bottomSheetPortrait"></param>
        /// <param name="bottomSheetLandscape"></param>
        public static void ImgbtnToggleKeyboardClicked(BottomSheet bottomSheetPortrait, BottomSheet bottomSheetLandscape)
        {
            // Get the current device orientation
            string cOrientation = GetDeviceOrientation();

            // Hide or show the keyboard
            switch (cOrientation)
            {
                case "Landscape":
                    {
                        bottomSheetLandscape.IsOpen = !bottomSheetLandscape.IsOpen;
                        break;
                    }
                default:
                    {
                        bottomSheetPortrait.IsOpen = !bottomSheetPortrait.IsOpen;
                        break;
                    }
            }
        }

        /// <summary>
        /// Show the bottom sheet depending on the device orientation
        /// </summary>
        /// <param name="bottomSheetPortrait"></param>
        /// <param name="bottomSheetLandscape"></param>
        public static void ShowBottomSheet(BottomSheet bottomSheetPortrait, BottomSheet bottomSheetLandscape)
        {
            // Get the current device orientation
            string cOrientation = GetDeviceOrientation();

            // Show the keyboard bottom sheet
            switch (cOrientation)
            {
                case "Landscape":
                    {
                        bottomSheetPortrait.IsOpen = false;
                        bottomSheetLandscape.IsOpen = true;
                        break;
                    }
                default:
                    {
                        bottomSheetLandscape.IsOpen = false;
                        bottomSheetPortrait.IsOpen = true;
                        break;
                    }
            }
        }
    }
}
