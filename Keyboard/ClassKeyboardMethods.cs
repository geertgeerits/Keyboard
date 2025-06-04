using System.Diagnostics;

namespace Keyboard
{
    internal static class ClassKeyboardMethods
    {
#if IOS
        // Do not use the custom keyboard for iOS
        // !!!BUG!!!? When the custom keyboard is enabled, the Entry properties like Selection, Cursor position, Placeholder and Border color, will not showing
        private static bool bUseCustomKeyboardForIOS = true;

        // Default value for keyboard toggle button
        public static bool bKeyboardToggleButton = true;
#else
        // Use the custom keyboard for iOS
        private static bool bUseCustomKeyboardForIOS = true;

        // Default value for keyboard toggle button
        public static bool bKeyboardToggleButton = true;
#endif
        
        // Enable border color change on focused Entry fields
        public static bool bEnableBorderColorOnFocused = true;

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
        /// Get the current theme
        /// </summary>
        public static string GetTheme()
        {
            // Ensure Application.Current is not null before accessing RequestedTheme
            if (Application.Current != null)
            {
                AppTheme currentTheme = Application.Current.RequestedTheme;

                return currentTheme.ToString();
            }

            return "Unspecified";
        }

        /// <summary>
        /// Set the border color and thickness of the entry field based on the theme when the entry is focused
        /// </summary>
        /// <param name="entry"></param>
        public static void SetEntryBorderColorFocused(Entry entry)
        {
            if (bEnableBorderColorOnFocused && entry != null)
            {
                Border border = (Border)entry.Parent;

                if (border != null)
                {
                    border.Stroke = GetTheme() switch
                    {
                        "Dark" => (Brush)Colors.LightBlue,
                        _ => (Brush)Colors.Blue,
                    };

                    //border.StrokeThickness = 2;
                }
            }
        }

        /// <summary>
        /// Set the border color and thickness of the entry field based on the theme when the entry is unfocused
        /// </summary>
        /// <param name="entry"></param>
        public static void SetEntryBorderColorUnfocused(Entry entry)
        {
            if (bEnableBorderColorOnFocused && entry != null)
            {
                Border border = (Border)entry.Parent;

                if (border != null)
                {
                    border.Stroke = GetTheme() switch
                    {
                        "Dark" => Application.Current?.Resources["Gray200"] is Color gray200Color ? new SolidColorBrush(gray200Color) : new SolidColorBrush(Colors.Transparent),
                        _ => Application.Current?.Resources["Gray400"] is Color gray400Color ? new SolidColorBrush(gray400Color) : new SolidColorBrush(Colors.Transparent),
                    };

                    //border.StrokeThickness = 1;
                }
            }
        }

        /// <summary>
        /// Set the image source for the keyboard toggle button depending on the theme when the keyboard is opened
        /// </summary>
        /// <param name="imageButton">The source of the event.</param>
        public static void SetImageKeyboardButtonSheetOpened(ImageButton imageButton)
        {
            if (imageButton == null)
            {
                return;
            }
            
            // The keyboard is not enabled for iOS
            if (!bUseCustomKeyboardForIOS)
            {
                imageButton.IsVisible = false;      // Hide the keyboard toggle button for iOS
                return;
            }

            // Show or hide the keyboard toggle button visibility
            imageButton.IsVisible = bKeyboardToggleButton;

            if (bKeyboardToggleButton && imageButton != null)
            {
                imageButton.Source = Application.Current?.RequestedTheme switch
                {
                    AppTheme.Dark => (ImageSource)cImageKeyboardHideDark,
                    _ => (ImageSource)cImageKeyboardHideLight,
                };
            }
        }

        /// <summary>
        /// Set the image source for the keyboard toggle button depending on the theme when the keyboard is closed
        /// </summary>
        /// <param name="imageButton"></param>
        private static void SetImageKeyboardButtonSheetClosed(ImageButton imageButton)
        {
            if (bKeyboardToggleButton && imageButton != null)
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
        /// <param name="focusedEntry"></param>
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
        /// <param name="focusedEntry"></param>
        /// <param name="cKey"></param>
        public static void KeyboardHexadecimalClicked(Entry focusedEntry, string cKey)
        {
            if (focusedEntry != null)
            {
                focusedEntry.Text = cKey switch
                {
                    "btnBackspace" => DeleteCharacterBeforeCursor(focusedEntry),
                    _ => InsertCharacterInEntryField(focusedEntry, cKey),
                };
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
            if (entry == null)
            {
                return string.Empty;
            }

            // Get the current text in the Entry
            string currentText = entry.Text ?? string.Empty;

            // Ensure CursorPosition is valid and entry.Text is not null
            int cursorPosition = entry.CursorPosition >= 0 && entry.CursorPosition <= currentText.Length
                ? entry.CursorPosition
                : currentText.Length;

            // Insert the character at the cursor position
            string newText = currentText.Insert(cursorPosition, cCharacter);

            // Update the Entry's text
            entry.Text = newText;

            // Move the cursor to the position after the inserted character
#if IOS
            // !!!BUG!!!: iOS does not support CursorPosition, so we need to set it to the end of the text            
            entry.CursorPosition = entry.Text.Length;
#else
            entry.CursorPosition = cursorPosition + 1;
#endif
            return newText;
        }

        /// <summary>
        /// Deletes the character before the current cursor position in the Entry field.
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        private static string DeleteCharacterBeforeCursor(Entry entry)
        {
            if (entry == null)
            {
                return string.Empty;
            }

            // Get the current text in the Entry
            string currentText = entry.Text ?? string.Empty;

            // Ensure there is a character to delete and the cursor is not at the start
            if (entry.CursorPosition > 0 && currentText.Length > 0)
            {
                // Ensure CursorPosition is valid
                int cursorPosition = entry.CursorPosition >= 0 && entry.CursorPosition <= currentText.Length
                    ? entry.CursorPosition
                    : currentText.Length;

                // Remove the character before the cursor position
                string newText = currentText.Remove(cursorPosition - 1, 1);

                // Update the Entry's text
                entry.Text = newText;

                // Move the cursor to the position before the deleted character
                entry.CursorPosition = cursorPosition - 1;

                return newText;
            }

            return currentText;
        }

        /// <summary>
        /// Change the numeric keyboard based on the current device orientation and theme
        /// </summary>
        /// <param name="bottomSheetPortrait"></param>
        /// <param name="bottomSheetLandscape"></param>
        /// <param name="imageButton"></param>
        public async static void ChangeKeyboardOrientation(ContentView bottomSheetPortrait, ContentView bottomSheetLandscape, ImageButton imageButton)
        {
            if (bottomSheetPortrait == null || bottomSheetLandscape == null)
            {
                return;
            }

            // The keyboard is not enabled for iOS
            if (!bUseCustomKeyboardForIOS)
            {
                return;
            }

            // Get the current device orientation
            string cOrientation = GetDeviceOrientation();

            // Hide or show the keyboard
            /* Animates an elements TranslationX and TranslationY properties from their current values to the new values. This ensures that the input layout is in the same position as the visual layout.
               public static System.Threading.Tasks.Task<bool> TranslateTo(this Microsoft.Maui.Controls.VisualElement view, double x, double y, uint length = 250, Microsoft.Maui.Easing? easing = default);
               Parameters:
               view, VisualElement, the view on which this method operates
               x, Double, the x component of the final translation vector
               y, Double, the y component of the final translation vector
               length, UInt32, the time, in milliseconds, over which to animate the transition, the default is 250
               easing, Easing, the easing function to use for the animation
             */
            switch (cOrientation)
            {
                case "Landscape":
                    {
                        if (bottomSheetLandscape.IsVisible)
                        {
                            await bottomSheetLandscape.TranslateTo(x: 0, y: 250, length: 250, Easing.SinIn);    // Slide down
                            bottomSheetLandscape.IsVisible = false;
                            SetImageKeyboardButtonSheetClosed(imageButton);                                     // Set the image source for the keyboard toggle button
                        }
                        else
                        {
                            bottomSheetLandscape.IsVisible = true;
                            await bottomSheetLandscape.TranslateTo(0, 0, length: 250, Easing.SinOut);   // Slide up
                            SetImageKeyboardButtonSheetOpened(imageButton);
                        }
                        break;
                    }
                default:
                    {
                        if (bottomSheetPortrait.IsVisible)
                        {
                            await bottomSheetPortrait.TranslateTo(0, 250, length: 250, Easing.SinIn);   // Slide down
                            bottomSheetPortrait.IsVisible = false;
                            SetImageKeyboardButtonSheetClosed(imageButton);
                        }
                        else
                        {
                            bottomSheetPortrait.IsVisible = true;
                            await bottomSheetPortrait.TranslateTo(0, 0, length: 250, Easing.SinOut);    // Slide up
                            SetImageKeyboardButtonSheetOpened(imageButton);
                        }
                        break;
                    }
            }
        }

        /// <summary>
        /// Show the bottom sheet depending on the device orientation
        /// </summary>
        /// <param name="bottomSheetPortrait"></param>
        /// <param name="bottomSheetLandscape"></param>
        /// <param name="imageButton"></param>
        public async static void ShowBottomSheet(ContentView bottomSheetPortrait, ContentView bottomSheetLandscape, ImageButton imageButton)
        {
            if (bottomSheetPortrait == null || bottomSheetLandscape == null)
            {
                return;
            }

            // The keyboard is not enabled for iOS
            if (!bUseCustomKeyboardForIOS)
            {
                return;
            }

            // Hide the bottom sheet if it is already visible
            HideBottomSheet(bottomSheetPortrait, bottomSheetLandscape, imageButton);

            // Get the current device orientation
            string cOrientation = GetDeviceOrientation();

            // Show the keyboard bottom sheet
            switch (cOrientation)
            {
                case "Landscape":
                    {
                        if (bottomSheetPortrait.IsVisible)
                        {
                            await bottomSheetPortrait.TranslateTo(0, 250, length: 250, Easing.SinIn);   // Slide down
                            bottomSheetPortrait.IsVisible = false;
                            await Task.Delay(300);                                                      // Wait for the slide down animation to complete
                        }

                        bottomSheetLandscape.IsVisible = true;
                        await bottomSheetLandscape.TranslateTo(0, 0, length: 250, Easing.SinOut);       // Slide up
                        break;
                    }
                default:
                    {
                        if (bottomSheetLandscape.IsVisible)
                        {
                            await bottomSheetLandscape.TranslateTo(0, 250, length: 250, Easing.SinIn);
                            bottomSheetLandscape.IsVisible = false;
                            await Task.Delay(300);
                        }

                        bottomSheetPortrait.IsVisible = true;
                        await bottomSheetPortrait.TranslateTo(0, 0, length: 250, Easing.SinOut);
                        break;
                    }
            }

            SetImageKeyboardButtonSheetOpened(imageButton);
        }

        /// <summary>
        /// Hide the bottom sheet depending on the device orientation
        /// </summary>
        /// <param name="bottomSheetPortrait"></param>
        /// <param name="bottomSheetLandscape"></param>
        /// <param name="imageButton"></param>
        public async static void HideBottomSheet(ContentView bottomSheetPortrait, ContentView bottomSheetLandscape, ImageButton imageButton)
        {
            if (bottomSheetPortrait == null || bottomSheetLandscape == null)
            {
                return;
            }

            // The keyboard is not enabled for iOS
            if (!bUseCustomKeyboardForIOS)
            {
                return;
            }

            if (bottomSheetLandscape.IsVisible)
            {
                await bottomSheetLandscape.TranslateTo(0, 250, length: 10, Easing.SinIn);
                bottomSheetLandscape.IsVisible = false;
            }
            else if (bottomSheetPortrait.IsVisible)
            {
                await bottomSheetPortrait.TranslateTo(0, 250, length: 10, Easing.SinIn);
                bottomSheetPortrait.IsVisible = false;
            }

            SetImageKeyboardButtonSheetClosed(imageButton);
            await Task.Delay(300);
        }

        /// <summary>
        /// Hide system the keyboard
        /// </summary>
        /// <param name="entry"></param>
        public async static void HideSystemKeyboard(Entry entry)
        {
            // The keyboard is not enabled for iOS
            if (!bUseCustomKeyboardForIOS || entry == null)
            {
                return;
            }

            try
            {
                if (entry.IsSoftInputShowing())
                {
#if ANDROID
                    // Android !!!BUG!!!: entry.Unfocus() must be called before HideSoftInputAsync() otherwise entry.Unfocus() is not called
                    entry.Unfocus();
#endif
                    _ = await entry.HideSoftInputAsync(System.Threading.CancellationToken.None);
                }
            }
            catch (Exception)
            {
                entry.IsEnabled = false;
                entry.IsEnabled = true;
            }
        }
    }
}
