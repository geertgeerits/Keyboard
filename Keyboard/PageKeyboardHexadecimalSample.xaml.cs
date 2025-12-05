namespace Keyboard
{
    public partial class PageKeyboardHexadecimalSample : ContentPage
    {
        // Declare variables
        private Entry? _focusedEntry;

        public PageKeyboardHexadecimalSample()
    	{
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error initializing PageKeyboardHexadecimalSample: {ex.Message}\n{ex.StackTrace}");
            }

            // Attach ICommand to receive key presses from the hexadecimal keyboard control
            RootKeyboardHexadecimalPortrait.KeyPressedCommand = new Command<string>(async key =>
            {
                // Reuse existing handler that expects the key id or character
                await BtnKeyboardClicked(key);
            });
            RootKeyboardHexadecimalLandscape.KeyPressedCommand = new Command<string>(async key =>
            {
                await BtnKeyboardClicked(key);
            });
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
            _ = ClassKeyboardMethods.ShowBottomSheet(CustomKeyboardHexadecimalPortrait, CustomKeyboardHexadecimalLandscape);
        }

        /// <summary>
        /// To do when the page is disappearing
        /// </summary>
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            // Hide the bottom sheet when the page is disappearing
            _ = ClassKeyboardMethods.HideBottomSheet(CustomKeyboardHexadecimalPortrait, CustomKeyboardHexadecimalLandscape);

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
            await ClassKeyboardMethods.ShowBottomSheet(CustomKeyboardHexadecimalPortrait, CustomKeyboardHexadecimalLandscape);

            // Scroll to the focused entry field in the scroll view
            if (_focusedEntry is not null)
            {
                await ClassKeyboardMethods.ScrollEntryToPosition(scrollView, _focusedEntry, "grdTitleView", RootKeyboardHexadecimalPortrait.HeightRequest, RootKeyboardHexadecimalLandscape.HeightRequest);
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
                await ClassKeyboardMethods.ShowBottomSheet(CustomKeyboardHexadecimalPortrait, CustomKeyboardHexadecimalLandscape);
#if IOS
                entry.Focus();              // This will trigger the Focused event
#endif
            }
        }

        /// <summary>
        /// Handles the focus event for an entry field, performing actions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void HexadecimalNumberEntryFocused(object sender, FocusEventArgs e)
        {
            if (sender is Entry entry)
            {
                _focusedEntry = entry;

                // Set the color of the entry field
                ClassKeyboardMethods.SetEntryColorFocused(entry);

                // Show the keyboard bottom sheet when the entry field is focused
                await ClassKeyboardMethods.ShowBottomSheet(CustomKeyboardHexadecimalPortrait, CustomKeyboardHexadecimalLandscape);

                // Scroll to the focused entry field in the scroll view
                await ClassKeyboardMethods.ScrollEntryToPosition(scrollView, entry, "grdTitleView", RootKeyboardHexadecimalPortrait.HeightRequest, RootKeyboardHexadecimalLandscape.HeightRequest);
            }
        }

        /// <summary>
        /// Entry unfocused event: format the text value for a numeric entry field with the number separator
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HexadecimalNumberEntryUnfocused(object sender, FocusEventArgs e)
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
                // Restore the color of the entry field
                ClassKeyboardMethods.SetEntryColorUnfocused(entry);
            }
        }

        /// <summary>
        /// Check if the value is hexadecimal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HexadecimalNumberEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!ClassEntryMethods.IsHexadecimalNumber(e.NewTextValue))
            {
                ((Entry)sender).Text = e.OldTextValue;
            }
        }

        /// <summary>
        /// Go to the next field when the return key have been pressed 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoToNextField(object sender, EventArgs? e)
        {
            // Go to the next field
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
                    await ClassKeyboardMethods.HideBottomSheet(CustomKeyboardHexadecimalPortrait, CustomKeyboardHexadecimalLandscape);
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
    }
}
