/* Program .....: Keyboard.sln
   Author ......: Geert Geerits - E-mail: geertgeerits@gmail.com
   Copyright ...: (C) 2025-2026
   Version .....: 1.0.32
   Date ........: 2025-12-03 (YYYY-MM-DD)
   Language ....: Microsoft Visual Studio 2026: .NET 10.0 MAUI C# 14.0
   Description .: Custom keyboard for decimal and hexadecimal entry fields
   Note:........: This app is a sample, experimental and still in development.
                  It is a custom keyboard for numeric and hex values that uses a ContentView as overlay page.
                  Hiding the Android and iOS system keyboard happens in the 'MauiProgram.cs' file, method: Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping
                  In iOS the 'await scrollView.ScrollToAsync(label, ScrollToPosition.Center, true)' does not work like in Android.
                  It centers horizontally and vertically for all the Entry controls in iOS even though the Orientation is only set to Vertical.
   Dependencies : 
*/

namespace Keyboard
{
    public partial class MainPage : ContentPage
    {
        // Declare variables
        private Entry? _focusedEntry;                       // Used to store the currently focused entry field

        public MainPage()
        {
            // Initialize the number format settings based on the current culture and 
            // Must be placed on the MainPage before InitializeComponent()
            ClassEntryMethods.InitializeNumberFormat();

            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error initializing MainPage: {ex.Message}\n{ex.StackTrace}");
            }

            // Attach ICommand to receive key presses from the decimal keyboard control
            RootKeyboardDecimalPortrait.KeyPressedCommand = new Command<string>(async key =>
            {
                // Reuse existing handler that expects the key id or character
                await BtnKeyboardClicked(key);
            });
            RootKeyboardDecimalLandscape.KeyPressedCommand = new Command<string>(async key =>
            {
                await BtnKeyboardClicked(key);
            });

            // Set the default alphanumeric keyboard layout
            ClassKeyboardMethods.cCurrentKeyboardLayout = Preferences.Default.Get("SettingKeyboardLayout", "QWERTY_US");
            //ClassKeyboardMethods.cCurrentKeyboardLayout = "ABCDEF_XX";
            ClassKeyboardMethods.SelectAlphanumericKeyboardLayout(ClassKeyboardMethods.cCurrentKeyboardLayout);

            // Set the placeholder text for the entry fields if the Placeholder property is empty or null and
            // the ValidationTriggerActionDecimal MinValue and MaxValue are set
            ClassEntryMethods.SetNumberEntryProperties(entTest1);
            ClassEntryMethods.SetNumberEntryProperties(entTest4);

            // Select all the text in the entry field - works for all pages in the app
            ClassEntryMethods.ModifyEntrySelectAllText();

            // Set the theme
            ClassKeyboardMethods.SetTheme();

            // Respond to theme changes
            Application.Current?.RequestedThemeChanged += static (s, a) =>
            {
                // Set the entry text color to a different color for a negative and a positive number
                ClassEntryMethods.SetNumberColor();
            };

            // Reads and logs the current device display information
            //ClassKeyboardMethods.ReadDeviceDisplay();
        }

        /// <summary>
        /// To do when the page is appearing
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Subscribe to orientation changes
            DeviceDisplay.MainDisplayInfoChanged += OnMainDisplayInfoChanged;

            // Show the bottom sheet when the page is appearing
            _ = ClassKeyboardMethods.ShowBottomSheet(CustomKeyboardDecimalPortrait, CustomKeyboardDecimalLandscape);
        }

        /// <summary>
        /// To do when the page is disappearing
        /// </summary>
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            // Hide the bottom sheet when the page is disappearing
            _ = ClassKeyboardMethods.HideBottomSheet(CustomKeyboardDecimalPortrait, CustomKeyboardDecimalLandscape);

            // Unsubscribe to orientation changes - if you don't do this, this event will be called if you are on another page
            DeviceDisplay.MainDisplayInfoChanged -= OnMainDisplayInfoChanged;
        }

        /// <summary>
        /// Set focus to the first entry field - Add in the header of the xaml page: 'Loaded="OnPageLoaded"' 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPageLoaded(object sender, EventArgs e)
        {
            _ = entTest1.Focus();
        }

        /// <summary>
        /// This method is called when the display information changes, it handles the orientation change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnMainDisplayInfoChanged(object? sender, DisplayInfoChangedEventArgs e)
        {
            await ClassKeyboardMethods.ShowBottomSheet(CustomKeyboardDecimalPortrait, CustomKeyboardDecimalLandscape);

            // Scroll to the focused entry field in the scroll view
            if (_focusedEntry is not null)
            {
                await ClassKeyboardMethods.ScrollEntryToPosition(scrollView, _focusedEntry, "grdTitleView", RootKeyboardDecimalPortrait.HeightRequest, RootKeyboardDecimalLandscape.HeightRequest);
            }
        }

        /// <summary>
        /// Show the keyboard bottom sheet when the entry control is tapped
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private async void OnTapShowKeyboardTapped(object sender, TappedEventArgs args)
        {
            if (sender is Entry entry)
            {
                await ClassKeyboardMethods.ShowBottomSheet(CustomKeyboardDecimalPortrait, CustomKeyboardDecimalLandscape);
#if IOS
                entry.Focus();              // This will trigger the Focused event
#endif
            }
        }

        /// <summary>
        /// Handles the focus event for the decimal numeric entry field, performing actions
        /// </summary>
        /// <param name="sender">The entry field that triggered the focus event.</param>
        /// <param name="e">The event data associated with the focus event.</param>
        private async void DecimalNumberEntryFocused(object sender, FocusEventArgs e)
        {
            if (sender is Entry entry)
            {
                _focusedEntry = entry;

                // Set the unformatted number in the entry field
                await ClassEntryMethods.FormatDecimalNumberEntryFocused(entry);

                // Set the color of the entry field
                ClassKeyboardMethods.SetEntryColorFocused(entry);

                // Show the keyboard bottom sheet when the entry field is focused
                await ClassKeyboardMethods.ShowBottomSheet(CustomKeyboardDecimalPortrait, CustomKeyboardDecimalLandscape);

                // Scroll to the focused entry field in the scroll view
                await ClassKeyboardMethods.ScrollEntryToPosition(scrollView, entry, "grdTitleView", RootKeyboardDecimalPortrait.HeightRequest, RootKeyboardDecimalLandscape.HeightRequest);
            }
        }

        /// <summary>
        /// Entry unfocused event: format the text value for a decimal numeric entry field with the number separator
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DecimalNumberEntryUnfocused(object sender, FocusEventArgs e)
        {
            if (sender is Entry entry)
            {
#if WINDOWS
                // Ignore false Unfocused events on Windows
                if (entry.IsFocused)
                {
                    return;
                }
#endif
                _focusedEntry = null;

                // Set the formatted number in the entry field
                ClassEntryMethods.FormatDecimalNumberEntryUnfocused(entry);

                // Restore the color of the entry field
                ClassKeyboardMethods.SetEntryColorUnfocused(entry);
            }
        }

        /// <summary>
        /// Check if the value is numeric and clear result fields if the text have changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DecimalNumberEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!ClassEntryMethods.IsDecimalNumber((Entry)sender, e.NewTextValue))
            {
                ((Entry)sender).Text = e.OldTextValue;
            }
        }

        /// <summary>
        /// Go to the next field when the next or return key have been pressed 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoToNextField(object sender, EventArgs? e)
        {
#if WINDOWS
            // Format the number for decimal number entries for the Windows platform
            // The first time a decimal entry field get unfocused the Unfocused event is not triggered on Windows
            if (sender is Entry entry)
            {
                ClassEntryMethods.FormatDecimalNumberEntryUnfocused(entry);
            }
#endif
            if (sender == entTest1)
            {
                _ = entTest2.Focus();
            }
            else if (sender == entTest2)
            {
                _ = entTest3.Focus();
            }
            else if (sender == entTest3)
            {
                _ = entTest4.Focus();
            }
            else if (sender == entTest4)
            {
                _ = entTest5.Focus();
            }
            else if (sender == entTest5)
            {
                _ = entTest1.Focus();
            }
        }

        /// <summary>
        /// Handles the click event for the keyboard buttons.
        /// </summary>
        /// <param name="cKey"></param>
        private async Task BtnKeyboardClicked(string cKey)
        {
            if (_focusedEntry != null)
            {
                if (cKey == "btnKeyboardHide")
                {
                    await ClassKeyboardMethods.ChangeKeyboardOrientation(CustomKeyboardDecimalPortrait, CustomKeyboardDecimalLandscape);
                }
                else if (cKey == "btnReturn")
                {
                    GoToNextField(_focusedEntry, null);
                }
                else
                {
                    ClassKeyboardMethods.KeyboardKeyClicked(_focusedEntry, cKey);
                }
            }
        }

        /// <summary>
        /// Open the page with the decimal keyboard when the button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnDecimal_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageKeyboardMixedSample());
        }

        /// <summary>
        /// Open the page with the hexadecimal keyboard when the button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnHexadecimal_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageKeyboardHexadecimalSample());
        }
    }
}
