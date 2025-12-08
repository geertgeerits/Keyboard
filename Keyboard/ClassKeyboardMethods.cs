namespace Keyboard
{
    internal static class ClassKeyboardMethods
    {
        // Keyboard layouts for alphanumeric input
        public static string[] cAlphaNumCharacters = new string[51];
        public static string? cCurrentKeyboardLayout;

        // Enable color change on focused Entry fields
        private static readonly bool bEnableColorOnFocused = true;

        // Default theme for the application (Light, Dark, System)
        private static readonly string cTheme = "System";

        /// <summary>
        /// Disables the system keyboard for all Entry controls in the application by updating the platform-specific
        /// handler mapping.
        /// </summary>
        /// <remarks>This method modifies the default behavior of Entry controls so that the system
        /// keyboard does not appear when an Entry receives focus. The change applies globally and affects all Entry
        /// instances created after this method is called. To restore the default keyboard behavior, the handler mapping
        /// must be reset or the application restarted.
        /// Put this before the InitializeComponent() on the MainPage: ClassKeyboardMethods.DisableSystemKeyboard();</remarks>
        /* If you need the system software keyboard (works for Android and Windows, not for iOS) for some entry controls
           put these in the 'Focused event' of the entry control:
           'ClassKeyboardMethods.HideBottomSheet(...)' and 'entry.ShowSoftInputAsync(...)'.
           In the 'Unfocused event' of the entry control call 'entry.HideSoftInputAsync(...)' and ClassKeyboardMethods.ShowBottomSheet().
           Re-enabling the system keyboard for all entry controls: set ShowSoftInputOnFocus to true on Android and reset the InputView to null on iOS.
           However, it is not recommended to turn the system keyboard back on after disabling it, as this may not produce the desired result.
           It is better not to mix custom and system keyboards in the same application.
        */
        public static void DisableSystemKeyboard()
        {
            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("NoKeyboardEntry", static (handler, entry) =>
            {
#if ANDROID
                handler.PlatformView.ShowSoftInputOnFocus = false;
#elif IOS
                handler.PlatformView.InputView = [];                // Hide keyboard
                handler.PlatformView.InputAccessoryView = null;     // Hide accessory bar ('Done' key)
#endif
            });
        }
        
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
                string theme = GetTheme();
                switch (theme)
                {
                    case "Dark":
                        if (Application.Current?.Resources != null && Application.Current.Resources.TryGetValue("EntryBackgroundFocusedDark", out var darkColor) && darkColor is Color darkColorValue)
                        {
                            entry.BackgroundColor = darkColorValue;
                        }
                        break;
                    default:
                        if (Application.Current?.Resources != null && Application.Current.Resources.TryGetValue("EntryBackgroundFocusedLight", out var lightColor) && lightColor is Color lightColorValue)
                        {
                            entry.BackgroundColor = lightColorValue;
                        }
                        break;
                }
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
                string theme = GetTheme();
                switch (theme)
                {
                    case "Dark":
                        if (Application.Current?.Resources != null && Application.Current.Resources.TryGetValue("EntryBackgroundUnfocusedDark", out var darkColor) && darkColor is Color darkColorValue)
                        {
                            entry.BackgroundColor = darkColorValue;
                        }
                        break;
                    default:
                        if (Application.Current?.Resources != null && Application.Current.Resources.TryGetValue("EntryBackgroundUnfocusedLight", out var lightColor) && lightColor is Color lightColorValue)
                        {
                            entry.BackgroundColor = lightColorValue;
                        }
                        break;
                }
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
        /// Selects the active alphanumeric keyboard layout based on the specified layout identifier
        /// </summary>
        /// <remarks>Use this method to change the set of alphanumeric characters available for input
        /// according to the desired keyboard layout. Only supported layouts can be selected; unsupported values will
        /// result in the default layout being applied.</remarks>
        /// <param name="cLayout">The identifier of the keyboard layout to activate. For example, "AZERTY_BE" selects the Belgian AZERTY
        /// layout. If an unrecognized value is provided, the default layout is used.</param>
        public static void SelectAlphanumericKeyboardLayout(string cLayout)
        {
            // Lookup keyboard characters from the shared dictionary
            if (!ClassKeyboardLayouts.KeyboardAlphanumericLayouts.TryGetValue(cLayout, out string[]? layoutChars))
            {
                // No keyboard characters for other keys
                return;
            }

            cAlphaNumCharacters = layoutChars;

            // Convert array to ReadOnlySpan array for performance optimization (no difference here)
            // ReadOnlySpan<string> cAlphaNumCharacter = cAlphaNumCharacters;

            Debug.WriteLine($"Selected keyboard layout: {cLayout}\nArray length: {cAlphaNumCharacters.Length}");
        }

        /// <summary>
        /// Handles the click event for the keyboard buttons
        /// </summary>
        /// <param name="focusedEntry"></param>
        /// <param name="cKey"></param>
        public static void KeyboardKeyClicked(Entry focusedEntry, string cKey)
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
                        else
                        {
                            focusedEntry.Text = focusedEntry.Text.Replace(ClassEntryMethods.cNumNegativeSign, string.Empty);
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
//#if WINDOWS                
//                // Restore focus so the Entry keeps the caret on Windows - MAUI issue - Does not shows the cursor !
//                // Use the UI thread to ensure the native focus/caret is updated.
//                MainThread.BeginInvokeOnMainThread(() =>
//                {
//                    //focusedEntry.Focus();
//                    //Task.Delay(50).Wait();  // Small delay to ensure focus is set before updating selection
//                    focusedEntry.SelectionLength = 0;
//                    Task.Delay(50).Wait();  // Small delay to ensure focus is set before updating selection
//                    // Force a small update to help WinUI render the caret
//                    focusedEntry.CursorPosition = focusedEntry.CursorPosition;
//                });
//#endif
            }
        }

        /// <summary>
        /// Inserts a character (also the composed characters like C̨ ) at the current cursor position in the Entry field.
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
                ? entry.CursorPosition : currentText.Length;

            // Insert the character at the cursor position
            string newText = currentText.Insert(cursorPosition, cCharacter);

            // Update the Entry's text
            entry.Text = newText;

            // Move the cursor to the position after the inserted character
            // The character length can be more than one for special characters
            // cCharacter is a composed sequence like C̨  - not a single precomposed character
            entry.CursorPosition = cursorPosition + cCharacter.Length;

            return newText;
        }

        /// <summary>
        /// Deletes the character (also the composed characters like C̨ ) before the current cursor position in the Entry field
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        private static string DeleteCharacterBeforeCursor(Entry entry)
        {
            if (entry == null)
            {
                return string.Empty;
            }

            string currentText = entry.Text ?? string.Empty;

            // Nothing to delete
            if (string.IsNullOrEmpty(currentText) || entry.CursorPosition <= 0)
            {
                return currentText;
            }

            int cursorPosition = entry.CursorPosition;

            // Get the indexes of each text element (grapheme cluster) start in the string
            int[] textElementStarts = System.Globalization.StringInfo.ParseCombiningCharacters(currentText);
            if (textElementStarts == null || textElementStarts.Length == 0)
            {
                return currentText;
            }

            // Find the index of the text element that contains (or is immediately before) the cursor
            int elementIndex = Array.BinarySearch(textElementStarts, cursorPosition);

            if (elementIndex >= 0)
            {
                // Cursor is exactly at the start of an element -> remove the previous element (if any)
                elementIndex--;
            }
            else
            {
                // If not an exact match, BinarySearch returns the bitwise complement of the next element index.
                // ~result gives the index of the first element start greater than cursorPosition.
                // We want the element start immediately before the cursor, so subtract 1.
                elementIndex = ~elementIndex - 1;
            }

            // If there's no previous element to delete, return current text
            if (elementIndex < 0)
            {
                return currentText;
            }

            int removeStart = textElementStarts[elementIndex];

            // Determine length of this text element
            int removeLength;
            if (elementIndex + 1 < textElementStarts.Length)
            {
                removeLength = textElementStarts[elementIndex + 1] - removeStart;
            }
            else
            {
                removeLength = currentText.Length - removeStart;
            }

            // Remove the whole grapheme cluster
            string newText = currentText.Remove(removeStart, removeLength);

            // Update the Entry content and cursor position
            entry.Text = newText;
            entry.CursorPosition = removeStart;

            return newText;
        }

        /// <summary>
        /// Change the numeric keyboard based on the current device orientation and theme
        /// </summary>
        /// <param name="bottomSheetPortrait"></param>
        /// <param name="bottomSheetLandscape"></param>
        public static async Task ChangeKeyboardOrientation(ContentView bottomSheetPortrait, ContentView bottomSheetLandscape)
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
                            MainThread.BeginInvokeOnMainThread(() =>
                            {
                                // Move off-screen then hide so next show is consistent
                                //await bottomSheetLandscape.TranslateToAsync(x: 0, y: 250, length: 10, Easing.Linear);    // Slide down
                                bottomSheetLandscape.TranslationY = 250;
                                bottomSheetLandscape.IsVisible = false;
                            });
                        }
                        else
                        {
                            MainThread.BeginInvokeOnMainThread(() =>
                            {
                                // Ensure off-screen position for hide state, then show at 0 when visible
                                //await bottomSheetLandscape.TranslateToAsync(0, 0, length: 10, Easing.Linear);   // Slide up
                                bottomSheetLandscape.TranslationY = 0;
                                bottomSheetLandscape.IsVisible = true;
                            });
                        }
                        break;
                    }
                default:
                    {
                        if (bottomSheetPortrait.IsVisible)
                        {
                            MainThread.BeginInvokeOnMainThread(() =>
                            {
                                // Move off-screen then hide so next show is consistent
                                //await bottomSheetPortrait.TranslateToAsync(0, 250, length: 10, Easing.Linear);   // Slide down
                                bottomSheetPortrait.TranslationY = 250;
                                bottomSheetPortrait.IsVisible = false;
                            });
                        }
                        else
                        {
                            MainThread.BeginInvokeOnMainThread(() =>
                            {
                                // Ensure off-screen position for hide state, then show at 0 when visible
                                //await bottomSheetPortrait.TranslateToAsync(0, 0, length: 10, Easing.Linear);    // Slide up
                                bottomSheetPortrait.TranslationY = 0;
                                bottomSheetPortrait.IsVisible = true;
                            });
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
        public static async Task ShowBottomSheet(ContentView bottomSheetPortrait, ContentView bottomSheetLandscape)
        {
            if (bottomSheetPortrait == null || bottomSheetLandscape == null || ClassEntryMethods.cKeyboard != "Custom")
            {
                return;
            }

            // Get the current device orientation
            string cOrientation = GetDeviceOrientation();

            // Show the keyboard bottom sheet
            switch (cOrientation)
            {
                case "Landscape":
                    {
                        if (bottomSheetPortrait.IsVisible)
                        {
                            MainThread.BeginInvokeOnMainThread(() =>
                            {
                                // Move off-screen then hide so next show is consistent
                                //await bottomSheetPortrait.TranslateToAsync(0, 250, length: 10, Easing.Linear);  // Slide down
                                bottomSheetPortrait.TranslationY = 250;
                                bottomSheetPortrait.IsVisible = false;
                            });
                        }

                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            // Ensure off-screen position for hide state, then show at 0 when visible
                            //await bottomSheetLandscape.TranslateToAsync(0, 0, length: 10, Easing.Linear);      // Slide up
                            bottomSheetLandscape.TranslationY = 0;
                            bottomSheetLandscape.IsVisible = true;
                        });

                        break;
                    }
                default:
                    {
                        if (bottomSheetLandscape.IsVisible)
                        {
                            MainThread.BeginInvokeOnMainThread(() =>
                            {
                                // Move off-screen then hide so next show is consistent
                                //await bottomSheetLandscape.TranslateToAsync(0, 250, length: 10, Easing.Linear);
                                bottomSheetLandscape.TranslationY = 250;
                                bottomSheetLandscape.IsVisible = false;
                            });
                        }

                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            // Ensure off-screen position for hide state, then show at 0 when visible
                            //await bottomSheetPortrait.TranslateToAsync(0, 0, length: 10, Easing.Linear);
                            bottomSheetPortrait.TranslationY = 0;
                            bottomSheetPortrait.IsVisible = true;
                        });
                        break;
                    }
            }
        }

        /// <summary>
        /// Hide the bottom sheet depending on the device orientation
        /// </summary>
        /// <param name="bottomSheetPortrait"></param>
        /// <param name="bottomSheetLandscape"></param>
        public static async Task HideBottomSheet(ContentView bottomSheetPortrait, ContentView bottomSheetLandscape)
        {
            if (bottomSheetPortrait == null || bottomSheetLandscape == null)
            {
                return;
            }

            if (bottomSheetLandscape.IsVisible)
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    // Move off-screen then hide so next show is consistent
                    //await bottomSheetLandscape.TranslateToAsync(0, 250, length: 20, Easing.SpringIn);
                    bottomSheetLandscape.TranslationY = 250;
                    bottomSheetLandscape.IsVisible = false;
                });
            }
            else if (bottomSheetPortrait.IsVisible)
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    // Move off-screen then hide so next show is consistent
                    //await bottomSheetPortrait.TranslateToAsync(0, 250, length: 20, Easing.SpringIn);
                    bottomSheetPortrait.TranslationY = 250;
                    bottomSheetPortrait.IsVisible = false;
                });
            }
        }

        /// <summary>
        /// Scrolls the specified 'Entry' into view within the given 'ScrollView'
        /// </summary>
        /// <param name="scrollView"></param>
        /// <param name="entry"></param>
        /// <param name="nKeyboardHeightPortrait"></param>
        /// <param name="nKeyboardHeightLandscape"></param>
        public static async Task ScrollEntryToPosition(ScrollView scrollView, Entry entry, string cTitleViewName, double nKeyboardHeightPortrait, double nKeyboardHeightLandscape)
        {
            // Ensure the scrollView and entry are not null before attempting to scroll
            if (scrollView == null || entry == null)
            {
                return;
            }

#if ANDROID || WINDOWS
            await scrollView.ScrollToAsync(entry, ScrollToPosition.Center, false);
            //await CalculateScrollEntryToPosition2(scrollView, entry, cTitleViewName, nKeyboardHeightPortrait, nKeyboardHeightLandscape);
#else
            // !!!BUG!!! in iOS: 'await scrollView.ScrollToAsync(label, ScrollToPosition.Center, false)' does not work like in Android
            // It centers horizontally and vertically for all the Entry controls in iOS even though the Orientation is only set to Vertical
            // Put a comment before one of the methods that you not want to use
            //await scrollView.ScrollToAsync(entry, ScrollToPosition.Center, false);

            // For iOS, we need to calculate the position of the Entry within the ScrollView
            await CalculateScrollEntryToPosition1(scrollView, entry, cTitleViewName, nKeyboardHeightPortrait, nKeyboardHeightLandscape);
#endif
        }

        /// <summary>
        /// Calculates the position of the specified 'Entry' within the 'ScrollView' based on the current device orientation and keyboard height
        /// </summary>
        /// <param name="scrollView"></param>
        /// <param name="entry"></param>
        /// <param name="cTitleViewName"></param>
        /// <param name="nKeyboardHeightPortrait"></param>
        /// <param name="nKeyboardHeightLandscape"></param>
        /// <returns></returns>
        private static async Task CalculateScrollEntryToPosition1(ScrollView scrollView, Entry entry, string cTitleViewName, double nKeyboardHeightPortrait, double nKeyboardHeightLandscape)
        {
            Debug.WriteLine($"scrollView.Height: {scrollView.Height}");
            Debug.WriteLine($"scrollView.ContentSize: {scrollView.ContentSize}");
            Debug.WriteLine($"scrollView.ScrollX/ScrollY: {scrollView.ScrollX} / {scrollView.ScrollY}");
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
                nPadding = cOrientation == "Landscape" ? 0 : 0;
            }
            else if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                nPadding = cOrientation == "Landscape" ? 100 : 0;
            }
            Debug.WriteLine($"Padding Value: {nPadding}");

            // Calculate the height of the ScrollView, excluding the keyboard, entry, title and any additional padding
            double nVisibleHeight = nDisplayHeight - nKeyboardHeight - entry.Height - nTitleViewHeight - nPadding;
            //double nVisibleHeight = scrollView.Height - nKeyboardHeight - entry.Height - nTitleViewHeight - nPadding;
            Debug.WriteLine($"Visible Height: {nVisibleHeight}");

            if (entryPosition.Y >= nVisibleHeight)
            {
                // If the entry is below the visible area, scroll it into view
                await scrollView.ScrollToAsync(0, entryPosition.Y, false);
                //await scrollView.ScrollToAsync(0, nVisibleHeight, false);
                Debug.WriteLine($"Scrolling to position: {entryPosition.Y}");
            }
            else if (entryPosition.Y < nVisibleHeight)
            {
                // If the entry is above the visible area, scroll it to the top
                await scrollView.ScrollToAsync(0, 0, false);
                Debug.WriteLine("Scrolling to position: 0");
            }
            else
            {
                // If the entry is within the visible area, no scrolling is needed
                Debug.WriteLine("No scrolling needed, entry is already in view.");
            }
        }

        /// <summary>
        /// Calculates the position of the specified 'Entry' within the 'ScrollView' based on the current device orientation and keyboard height
        /// </summary>
        /// <param name="scrollView"></param>
        /// <param name="entry"></param>
        /// <param name="cTitleViewName"></param>
        /// <param name="nKeyboardHeightPortrait"></param>
        /// <param name="nKeyboardHeightLandscape"></param>
        /// <returns></returns>
        private static async Task CalculateScrollEntryToPosition2(ScrollView scrollView, Entry entry, string cTitleViewName, double nKeyboardHeightPortrait, double nKeyboardHeightLandscape)
        {
            if (scrollView == null || entry == null)
            {
                return;
            }

            // Small margin so the entry isn't flush against the keyboard/title
            const double margin = 12.0;

            // Ensure layout has been measured (avoid zero heights)
            await Task.Yield();

            // Orientation & keyboard height
            string cOrientation = GetDeviceOrientation();
            double nKeyboardHeight = cOrientation == "Landscape" ? nKeyboardHeightLandscape : nKeyboardHeightPortrait;

            // Display / window height
            double nDisplayHeight = DeviceInfo.Platform == DevicePlatform.WinUI ? GetWindowHeight() : GetDisplayHeight();

            // Title view height and per-platform padding
            double nTitleViewHeight = GetTitleViewHeight(cTitleViewName);
            double nPadding = 0;
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                nPadding = cOrientation == "Landscape" ? -180 : 90;
            }
            else if (DeviceInfo.Platform == DevicePlatform.iOS)
            {
                nPadding = cOrientation == "Landscape" ? 0 : 100;
            }
            else if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                nPadding = cOrientation == "Landscape" ? -50 : 0;
            }

            // Visible area height (content area not covered by keyboard/title/padding)
            double visibleHeight = nDisplayHeight - nKeyboardHeight - nTitleViewHeight - nPadding;
            if (visibleHeight <= 0)
            {
                return;
            }

            // Compute entry Y relative to scrollView content: entryScreenY - scrollViewScreenY + currentScrollY
            Point scrollViewScreen = GetEntryScreenPosition(scrollView);
            Point entryScreen = GetEntryScreenPosition(entry);
            double relativeY = entryScreen.Y - scrollViewScreen.Y + scrollView.ScrollY;

            // Current visible top/bottom in content coordinates
            double topVisible = scrollView.ScrollY;
            double bottomVisible = topVisible + visibleHeight;

            // Heights of content and entry for clamping
            double contentHeight = scrollView.Content?.Height ?? (visibleHeight + entry.Height);
            double entryBottom = relativeY + entry.Height;

            // Decide if scroll needed:
            double targetY = topVisible;  // default no change
            if (entryBottom > bottomVisible - margin)
            {
                // Entry is partially/fully below visible area -> scroll down so entry bottom is visible
                targetY = relativeY + entry.Height - visibleHeight + margin;
            }
            else if (relativeY < topVisible + margin)
            {
                // Entry is above visible area -> scroll up so entry top is visible with margin
                targetY = relativeY - margin;
            }
            else
            {
                // Already visible
                return;
            }

            // Clamp targetY to valid range
            double maxScroll = Math.Max(0, contentHeight - visibleHeight);
            targetY = Math.Max(0, Math.Min(targetY, maxScroll));

            await scrollView.ScrollToAsync(0, targetY, false);
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
                // Access the first window directly without using LINQ methods
                Window window = Application.Current.Windows[0];
                if (window != null)
                {
                    double width = window.Width;
                    double height = window.Height;
                    Debug.WriteLine($"Window Width: {width}, Window Height: {height}");
                    return height;
                }
            }
            
            return 0;       // Return 0 if the window is not found
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
        /// Get the height of the TitleView in the current page
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
                Page? currentPageNav = null;
                if (Application.Current?.Windows != null && Application.Current.Windows.Count > 0)
                {
                    currentPageNav = Application.Current.Windows[0].Page as NavigationPage;
                }
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
        private static string GetNavigationType()
        {
            if (Shell.Current != null)
            {
                return "Shell";
            }

            if (Application.Current?.Windows != null && Application.Current.Windows.Count > 0 &&
                Application.Current.Windows[0].Page is NavigationPage)
            {
                return "NavigationPage";
            }

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

        /// <summary>
        /// Get the main display info
        /// </summary>
        public static void ReadDeviceDisplay()
        {
            System.Text.StringBuilder sb = new();

            sb.AppendLine($"Pixel width: {DeviceDisplay.Current.MainDisplayInfo.Width} / Pixel Height: {DeviceDisplay.Current.MainDisplayInfo.Height}");
            sb.AppendLine($"Density: {DeviceDisplay.Current.MainDisplayInfo.Density}");
            sb.AppendLine($"Orientation: {DeviceDisplay.Current.MainDisplayInfo.Orientation}");
            sb.AppendLine($"Rotation: {DeviceDisplay.Current.MainDisplayInfo.Rotation}");
            sb.AppendLine($"Refresh Rate: {DeviceDisplay.Current.MainDisplayInfo.RefreshRate}");

            //DisplayDetailsLabel.Text = sb.ToString();
            Debug.WriteLine($"ReadDeviceDisplay:\n{sb}");
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
