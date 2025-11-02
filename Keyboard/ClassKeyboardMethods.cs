using System.Diagnostics;

namespace Keyboard
{
    internal static class ClassKeyboardMethods
    {
        // Default value for keyboard toggle button
        public static bool bKeyboardToggleButton = true;
        
        // Enable color change on focused Entry fields
        public static bool bEnableColorOnFocused = true;

        // Colors for the entry field based on the theme
        //private static readonly Color originalEntryBackgroundColorLight = (Color)Application.Current.Resources["Gray050"];
        //private static readonly Color originalEntryBackgroundColorDark = (Color)Application.Current.Resources["Gray600"];
        //private static readonly Color originalEntryTextColorLight = (Color)Application.Current.Resources["Black"];
        //private static readonly Color originalEntryTextColorDark = (Color)Application.Current.Resources["White"];

        // Image source for the keyboard toggle button
        private static readonly string cImageKeyboardHideDark = "keyboard_hide_32p_white.png";  // Dark theme image
        private static readonly string cImageKeyboardHideLight = "keyboard_hide_32p_black.png"; // Light theme image
        private static readonly string cImageKeyboardShowDark = "keyboard_32p_white.png";       // Dark theme image
        private static readonly string cImageKeyboardShowLight = "keyboard_32p_black.png";      // Light theme image

        // Default theme for the application (Light, Dark, System)
        private static string cTheme = "System";

        // Current background color of the entry field
        private static readonly Dictionary<Entry, Color> _originalEntryBackgroundColor = [];
        //private static readonly Dictionary<Entry, Color> _originalEntryTextColor = [];

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
        /// Set the color of the entry field based on the theme when the entry is focused
        /// </summary>
        /// <param name="entry"></param>
        public static void SetEntryColorFocused(Entry entry)
        {
            if (bEnableColorOnFocused && entry != null)
            {
                SaveOriginalEntryColors(entry);
                
                entry.BackgroundColor = GetTheme() switch
                {
                    "Dark" => Colors.LightGreen,
                    _ => Colors.LightBlue,
                };

                //Border border = (Border)entry.Parent;
                //border?.Stroke = GetTheme() switch
                //{
                //    "Dark" => (Brush)Colors.LightBlue,
                //    _ => (Brush)Colors.Blue,
                //};
            }
        }

        /// <summary>
        /// Set the color of the entry field based on the theme when the entry is unfocused
        /// </summary>
        /// <param name="entry"></param>
        public static void SetEntryColorUnfocused(Entry entry)
        {
            if (bEnableColorOnFocused && entry != null)
            {
                RestoreOriginalEntryColors(entry);

                //Border border = (Border)entry.Parent;
                //border?.Stroke = GetTheme() switch
                //{
                //    "Dark" => Application.Current?.Resources["Gray200"] is Color gray200Color ? new SolidColorBrush(gray200Color) : new SolidColorBrush(Colors.Transparent),
                //    _ => Application.Current?.Resources["Gray400"] is Color gray400Color ? new SolidColorBrush(gray400Color) : new SolidColorBrush(Colors.Transparent),
                //};
            }
        }

        /// <summary>
        /// Save the original Entry colors
        /// </summary>
        /// <param name="entry"></param>
        private static void SaveOriginalEntryColors(Entry entry)
        {
            if (entry != null && !_originalEntryBackgroundColor.ContainsKey(entry))
            {
                _originalEntryBackgroundColor[entry] = entry.BackgroundColor;
                //_originalEntryTextColor[entry] = entry.TextColor;
            }
        }

        /// <summary>
        /// Restore the original Entry colors
        /// </summary>
        /// <param name="entry"></param>
        private static void RestoreOriginalEntryColors(Entry entry)
        {
            if (entry != null && _originalEntryBackgroundColor.TryGetValue(entry, out var color))
            {
                entry.BackgroundColor = color;
                _originalEntryBackgroundColor.Remove(entry);
            }

            //if (entry != null && _originalEntryTextColor.TryGetValue(entry, out color))
            //{
            //    entry.TextColor = color;
            //    _originalEntryTextColor.Remove(entry);
            //}
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
            DisplayInfo displayInfo = DeviceDisplay.MainDisplayInfo;

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
            focusedEntry?.Text = cKey switch
            {
                "btnBackspace" => DeleteCharacterBeforeCursor(focusedEntry),
                _ => InsertCharacterInEntryField(focusedEntry, cKey),
            };
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
            entry.CursorPosition = cursorPosition + 1;

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

            // Get the current device orientation
            string cOrientation = GetDeviceOrientation();

            // Hide or show the keyboard
            /* Animates an elements TranslationX and TranslationY properties from their current values to the new values. This ensures that the input layout is in the same position as the visual layout.
               public static System.Threading.Tasks.Task<bool> TranslateToAsync(this Microsoft.Maui.Controls.VisualElement view, double x, double y, uint length = 250, Microsoft.Maui.Easing? easing = default);
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
                            await bottomSheetLandscape.TranslateToAsync(x: 0, y: 250, length: 250, Easing.SinIn);    // Slide down
                            bottomSheetLandscape.IsVisible = false;
                            SetImageKeyboardButtonSheetClosed(imageButton);                                     // Set the image source for the keyboard toggle button
                        }
                        else
                        {
                            bottomSheetLandscape.IsVisible = true;
                            await bottomSheetLandscape.TranslateToAsync(0, 0, length: 250, Easing.SinOut);   // Slide up
                            SetImageKeyboardButtonSheetOpened(imageButton);
                        }
                        break;
                    }
                default:
                    {
                        if (bottomSheetPortrait.IsVisible)
                        {
                            await bottomSheetPortrait.TranslateToAsync(0, 250, length: 250, Easing.SinIn);   // Slide down
                            bottomSheetPortrait.IsVisible = false;
                            SetImageKeyboardButtonSheetClosed(imageButton);
                        }
                        else
                        {
                            bottomSheetPortrait.IsVisible = true;
                            await bottomSheetPortrait.TranslateToAsync(0, 0, length: 250, Easing.SinOut);    // Slide up
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
                            await bottomSheetPortrait.TranslateToAsync(0, 250, length: 250, Easing.SinIn);   // Slide down
                            bottomSheetPortrait.IsVisible = false;
                            await Task.Delay(300);                                                      // Wait for the slide down animation to complete
                        }

                        bottomSheetLandscape.IsVisible = true;
                        await bottomSheetLandscape.TranslateToAsync(0, 0, length: 250, Easing.SinOut);       // Slide up
                        break;
                    }
                default:
                    {
                        if (bottomSheetLandscape.IsVisible)
                        {
                            await bottomSheetLandscape.TranslateToAsync(0, 250, length: 250, Easing.SinIn);
                            bottomSheetLandscape.IsVisible = false;
                            await Task.Delay(300);
                        }

                        bottomSheetPortrait.IsVisible = true;
                        await bottomSheetPortrait.TranslateToAsync(0, 0, length: 250, Easing.SinOut);
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

            if (bottomSheetLandscape.IsVisible)
            {
                await bottomSheetLandscape.TranslateToAsync(0, 250, length: 20, Easing.SpringIn);
                bottomSheetLandscape.IsVisible = false;
            }
            else if (bottomSheetPortrait.IsVisible)
            {
                await bottomSheetPortrait.TranslateToAsync(0, 250, length: 20, Easing.SpringIn);
                bottomSheetPortrait.IsVisible = false;
            }

            SetImageKeyboardButtonSheetClosed(imageButton);
            await Task.Delay(400);
        }

        /// <summary>
        /// Hide the system keyboard
        /// </summary>
        /// <param name="entry"></param>
        public async static void HideSystemKeyboard(Entry entry)
        {
            // !!!BUG!!!: when this method is called, the entry field loses focus on iOS
            // The entry control's unfocused event is executed immediately after the focused event in net maui, only on iOS
            // The keyboard has been disabled for all Entry controls in the MauiProgram.cs

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


    /// <summary>
    /// Scrolls the specified 'Entry' into view within the given 'ScrollView'
    /// </summary>
    /// <param name="scrollView"></param>
    /// <param name="entry"></param>
    /// <param name="nKeyboardHeightPortrait"></param>
    /// <param name="nKeyboardHeightLandscape"></param>
    public static async void ScrollEntryToPosition(ScrollView scrollView, Entry entry, string cTitleViewName, double nKeyboardHeightPortrait, double nKeyboardHeightLandscape)
        {
            // Ensure the scrollView and entry are not null before attempting to scroll
            if (scrollView == null || entry == null)
            {
                return;
            }

#if ANDROID || WINDOWS
            await scrollView.ScrollToAsync(entry, ScrollToPosition.Center, true);
            //CalculateScrollEntryToPosition(scrollView, entry, cTitleViewName, nKeyboardHeightPortrait, nKeyboardHeightLandscape);
#else
            // !!!BUG!!! in iOS: 'await scrollView.ScrollToAsync(label, ScrollToPosition.Center, true)' does not work like in Android
            // It centers horizontally and vertically for all the Entry controls in iOS even though the Orientation is only set to Vertical
            // Put a comment before one of the methods that you not want to use
            //await scrollView.ScrollToAsync(entry, ScrollToPosition.Center, true);

            // For iOS, we need to calculate the position of the Entry within the ScrollView
            CalculateScrollEntryToPosition(scrollView, entry, cTitleViewName, nKeyboardHeightPortrait, nKeyboardHeightLandscape);
#endif
        }

        /// <summary>
        /// Calculates the position of the specified 'Entry' within the 'ScrollView' based on the current device orientation and keyboard height
        /// </summary>
        /// <param name="scrollView"></param>
        /// <param name="entry"></param>
        /// <param name="nKeyboardHeightPortrait"></param>
        /// <param name="nKeyboardHeightLandscape"></param>
        public static async void CalculateScrollEntryToPosition(ScrollView scrollView, Entry entry, string cTitleViewName, double nKeyboardHeightPortrait, double nKeyboardHeightLandscape)
        {
            Debug.WriteLine($"nKeyboardHeightPortrait: {nKeyboardHeightPortrait}, nKeyboardHeightLandscape: {nKeyboardHeightLandscape}");

            // Get the current device orientation
            string cOrientation = GetDeviceOrientation();
            double nKeyboardHeight = cOrientation == "Landscape" ? nKeyboardHeightLandscape : nKeyboardHeightPortrait;
            Debug.WriteLine($"App Orientation: {cOrientation}, nKeyboardHeight {nKeyboardHeight}");

            // Get the window height for Windows WinUI
            double nDisplayHeight;
            if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                nDisplayHeight = GetWindowHeight();
                Debug.WriteLine($"Window Height: {nDisplayHeight}");
            }
            // Get the display height for Android and iOS
            else
            {
                nDisplayHeight = GetDisplayHeight();
                Debug.WriteLine($"Display Height: {nDisplayHeight}");
            }

            // Get the position of the Entry within the ScrollView
            Point entryPosition = GetEntryScreenPosition(entry);
            Debug.WriteLine($"Entry Position: {entryPosition.X}, {entryPosition.Y}");

            // Get the height of the TitleView
            double nTitleViewHeight = GetTitleViewHeight(cTitleViewName);
            Debug.WriteLine($"TitleView Height: {nTitleViewHeight}");

            // Adjust the Padding value for each platform in portrait and landscape mode
            double nPadding = 0;
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                nPadding = cOrientation == "Landscape" ? 40 : 0;
            }
            else if (DeviceInfo.Platform == DevicePlatform.iOS)
            {
                nPadding = cOrientation == "Landscape" ? 30 : 0;
            }
            else if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                nPadding = cOrientation == "Landscape" ? 100 : 0;
            }
            Debug.WriteLine($"Padding Value: {nPadding}");

            // Calculate the height of the ScrollView, excluding the keyboard, entry, title and any additional padding
            double nViewHeight = nDisplayHeight - nKeyboardHeight - entry.Height - nTitleViewHeight - nPadding;
            Debug.WriteLine($"View Height: {nViewHeight}");

            if (entryPosition.Y >= nViewHeight)
            {
                // If the entry is below the visible area, scroll it into view
                await scrollView.ScrollToAsync(0, entryPosition.Y, true);
                Debug.WriteLine($"Scrolling to position: {entryPosition.Y}");
            }
            else if (entryPosition.Y < nViewHeight)
            {
                // If the entry is above the visible area, scroll it to the top
                await scrollView.ScrollToAsync(0, 0, true);
                Debug.WriteLine("Scrolling to position: 0");
            }
            else
            {
                // If the entry is within the visible area, no scrolling is needed
                Debug.WriteLine("No scrolling needed, entry is already in view.");
            }
        }

        /// <summary>
        /// Get the height of the current window in Windows WinUI applications
        /// Can not be used in Android and iOS applications because the height does not change when in landscape mode
        /// </summary>
        /// <returns></returns>
        private static double GetWindowHeight()
        {
            // Ensure Application.Current and its Windows collection are not null
            if (Application.Current?.Windows != null && Application.Current.Windows.Count > 0)
            {
                Window? window = Application.Current.Windows[0];    // Access the first window directly
                
                if (window != null)
                {
                    double width = window.Width;
                    double height = window.Height;
                    Debug.WriteLine($"Window Width: {width}, Window Height: {height}");
                    
                    return height;
                }
            }
            
            return 0;                                               // Return 0 if the window is not found
        }

        /// <summary>
        /// Get the height of the display in Android and iOS applications
        /// </summary>
        /// <returns></returns>
        private static double GetDisplayHeight()
        {
            double width = DeviceDisplay.Current.MainDisplayInfo.Width;
            double height = DeviceDisplay.Current.MainDisplayInfo.Height;
            double density = DeviceDisplay.Current.MainDisplayInfo.Density;
            Debug.WriteLine($"Display Width: {width}, Display Height: {height}, Density: {density}");

            return height / density;
        }

        /// <summary>
        /// Get the height of the Shell.TitleView in the current page
        /// </summary>
        /// <returns></returns>
        private static double GetTitleViewHeight(string cTitleViewName)
        {
            if (GetNavigationType() == "Shell")
            {
                // Shell.TitleView
                Page? currentPage = Shell.Current?.CurrentPage;
                if (currentPage != null && !string.IsNullOrEmpty(cTitleViewName))
                {
                    View titleView = currentPage.FindByName<View>(cTitleViewName);
                    if (titleView != null)
                    {
                        return titleView.Height;
                    }
                }
            }

            else if (GetNavigationType() == "NavigationPage")
            {
                // NavigationPage.TitleView - NOT tested yet
                Page? currentPageNav = Application.Current?.Windows.FirstOrDefault()?.Page as NavigationPage;
                if (currentPageNav != null)
                {
                    View titleView = currentPageNav.FindByName<View>("grdTitleView");
                    if (titleView != null)
                    {
                        return titleView.Height;
                    }
                }
            }

            return 0;
        }

        /// <summary>
        /// Get navigation type
        /// </summary>
        /// <returns></returns>
        public static string GetNavigationType()
        {
            if (Shell.Current != null)
                return "Shell";
            if (Application.Current?.Windows != null && Application.Current.Windows.Count > 0 &&
                Application.Current.Windows[0].Page is NavigationPage)
                return "NavigationPage";
            return "Other";
        }

        /// <summary>
        /// Calculates the screen position of the specified visual element
        /// </summary>
        /// <param name="entry"></param>
        /// <returns>Returns a point representing the screen position of the specified visual element</returns>
        private static Point GetEntryScreenPosition(VisualElement entry)
        {
            if (entry == null)
            {
                return new Point(0, 0);
            }

            // Get the absolute position relative to the window
            Point location = entry.GetAbsolutePosition();
            return location;
        }
    }

    /// <summary>
    /// Extension method for VisualElement
    /// </summary>
    public static class VisualElementExtensions
    {
        /// <summary>
        /// Calculates the absolute position of a 'VisualElement' relative to the root of the visual hierarchy
        /// </summary>
        /// <param name="element"></param>
        /// <returns>A 'Point' representing the absolute position of the 'element' in the visual hierarchy</returns>
        public static Point GetAbsolutePosition(this VisualElement element)
        {
            double x = element.X;
            double y = element.Y;
            Element parent = element.Parent;

            while (parent is VisualElement parentVisual)
            {
                x += parentVisual.X;
                y += parentVisual.Y;
                parent = parentVisual.Parent;
            }

            return new Point(x, y);
        }
    }
}
