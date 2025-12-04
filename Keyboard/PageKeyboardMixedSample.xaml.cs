namespace Keyboard
{
    public partial class PageKeyboardMixedSample : ContentPage
    {
        // Declare variables
        private Entry? _focusedEntry;
        private bool bRestartApplication;

        public PageKeyboardMixedSample()
    	{
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
#if DEBUG                
                _ = DisplayAlertAsync("InitializeComponent: PageKeyboardMixedSample", $"{ex.Message}\nPossible cause: the number of the keyboard characters are not exactly 53 per keyboard layout (0-52)!", "OK");
#endif                
                return;
            }

            // Attach ICommand to receive key presses from the alphanumeric keyboard control
            RootKeyboardAlphanumericPortrait.KeyPressedCommand = new Command<string>(async key =>
            {
                // Reuse existing handler that expects the key id or character
                await BtnKeyboardClicked(key);
            });
            RootKeyboardAlphanumericLandscape.KeyPressedCommand = new Command<string>(async key =>
            {
                await BtnKeyboardClicked(key);
            });

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

            // Initialize the keyboard layout picker
            pckKeyboardLayout.SelectedIndex = Preferences.Default.Get("SettingKeyboardLayoutSelectedIndex", 3); ;
        }

        /// <summary>
        /// To do when the page is appearing
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Subscribe to orientation changes
            DeviceDisplay.MainDisplayInfoChanged += OnMainDisplayInfoChanged;
        }

        /// <summary>
        /// To do when the page is disappearing
        /// </summary>
        protected async override void OnDisappearing()
        {
            base.OnDisappearing();

            // Hide the bottom sheet when the page is disappearing
            await ClassKeyboardMethods.HideBottomSheet(CustomKeyboardAlphanumericPortrait, CustomKeyboardAlphanumericLandscape);
            await ClassKeyboardMethods.HideBottomSheet(CustomKeyboardDecimalPortrait, CustomKeyboardDecimalLandscape);
            await ClassKeyboardMethods.HideBottomSheet(CustomKeyboardHexadecimalPortrait, CustomKeyboardHexadecimalLandscape);

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
            _ = pckKeyboardLayout.Focus();
            //_ = entTest1.Focus();
        }

        /// <summary>
        /// This method is called when the display information changes, it handles the orientation change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnMainDisplayInfoChanged(object? sender, DisplayInfoChangedEventArgs e)
        {
            if (_focusedEntry is not null)
            {
                // Show the appropriate keyboard bottom sheet for the focused entry field
                await ShowKeyboard(_focusedEntry);

                // Scroll to the focused entry field in the scroll view
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
                _focusedEntry = entry;

                // Show the custom keyboard for this entry immediately (Windows often doesn't fire Focused reliably).
                await ShowKeyboard(entry);
#if IOS
                // On iOS ensure the control actually gets native focus (this will trigger Focused event there).
                entry.Focus();
#endif
                return;
            }

            // Fallback: if we already know the focused entry, show its keyboard.
            if (_focusedEntry is not null)
            {
                await ShowKeyboard(_focusedEntry);
            }
        }

        /// <summary>
        /// Show the keyboard bottom sheet
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        private async Task ShowKeyboard(Entry entry)
        {
            if (entry is null)
            {
                return;
            }

            // Keyboard.Text = Microsoft.Maui.TextKeyboard = Alphanumeric
            // Keyboard.Numeric = Microsoft.Maui.NumericKeyboard = Decimal
            // Keyboard.Default = Microsoft.Maui.Keyboard = Hexadecimal
            Debug.WriteLine($"ShowKeyboard: {entry.Keyboard}");
            
            string cKeyboardType = entry.Keyboard?.ToString() ?? string.Empty;

            switch (cKeyboardType)
            {
                case "Microsoft.Maui.TextKeyboard":
                    await ClassKeyboardMethods.ShowBottomSheet(CustomKeyboardAlphanumericPortrait, CustomKeyboardAlphanumericLandscape);
                    break;
                case "Microsoft.Maui.NumericKeyboard":
                    await ClassKeyboardMethods.ShowBottomSheet(CustomKeyboardDecimalPortrait, CustomKeyboardDecimalLandscape);
                    break;
                default:
                    await ClassKeyboardMethods.ShowBottomSheet(CustomKeyboardHexadecimalPortrait, CustomKeyboardHexadecimalLandscape);
                    break;
            }
        }

        /// <summary>
        /// Handles the focus event for a text entry control, displaying the system soft input keyboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void TextEntryFocused(object sender, FocusEventArgs e)
        {
            if (sender is Entry entry)
            {
                _focusedEntry = entry;

                // Hide/Show the custom keyboard bottom sheet when the entry field is focused
                await ClassKeyboardMethods.HideBottomSheet(CustomKeyboardDecimalPortrait, CustomKeyboardDecimalLandscape);
                await ClassKeyboardMethods.HideBottomSheet(CustomKeyboardHexadecimalPortrait, CustomKeyboardHexadecimalLandscape);
                await Task.Delay(100);  // Small delay to let the bottom sheet hide animation complete
                await ClassKeyboardMethods.ShowBottomSheet(CustomKeyboardAlphanumericPortrait, CustomKeyboardAlphanumericLandscape);

                // Scroll to the focused entry field in the scroll view
                await ClassKeyboardMethods.ScrollEntryToPosition(scrollView, entry, "grdTitleView", RootKeyboardAlphanumericPortrait.HeightRequest, RootKeyboardAlphanumericLandscape.HeightRequest);
            }
        }

        /// <summary>
        /// Handles the unfocused event for a text entry control, hiding the system soft input keyboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void TextEntryUnfocused(object sender, FocusEventArgs e)
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
            }
        }

        /// <summary>
        /// Handles the focus event for the numeric entry field, performing actions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void DecimalNumberEntryFocused(object sender, FocusEventArgs e)
        {
            if (sender is Entry entry)
            {
                _focusedEntry = entry;

                // Set the unformatted number in the entry field
                await ClassEntryMethods.FormatDecimalNumberEntryFocused(entry);

                // Set the color of the entry field
                ClassKeyboardMethods.SetEntryColorFocused(entry);

                // Hide/Show the custom keyboard bottom sheet when the entry field is focused
                await ClassKeyboardMethods.HideBottomSheet(CustomKeyboardAlphanumericPortrait, CustomKeyboardAlphanumericLandscape);
                await ClassKeyboardMethods.HideBottomSheet(CustomKeyboardHexadecimalPortrait, CustomKeyboardHexadecimalLandscape);
                await Task.Delay(100);  // Small delay to let the bottom sheet hide animation complete
                await ClassKeyboardMethods.ShowBottomSheet(CustomKeyboardDecimalPortrait, CustomKeyboardDecimalLandscape);

                // Scroll to the focused entry field in the scroll view
                await ClassKeyboardMethods.ScrollEntryToPosition(scrollView, entry, "grdTitleView", RootKeyboardDecimalPortrait.HeightRequest, RootKeyboardDecimalLandscape.HeightRequest);
            }
        }

        /// <summary>
        /// Entry unfocused event: format the text value for a numeric entry field with the number separator
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
                // Set the formatted number in the entry field
                ClassEntryMethods.FormatDecimalNumberEntryUnfocused(entry);

                // Restore the color of the entry field and format the number
                ClassKeyboardMethods.SetEntryColorUnfocused(entry);
            }
        }

        /// <summary>
        /// Handles the focus event for the hexadecimal numeric entry field, performing actions
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

                // Hide/Show the custom keyboard bottom sheet when the entry field is focused
                await ClassKeyboardMethods.HideBottomSheet(CustomKeyboardAlphanumericPortrait, CustomKeyboardAlphanumericLandscape);
                await ClassKeyboardMethods.HideBottomSheet(CustomKeyboardDecimalPortrait, CustomKeyboardDecimalLandscape);
                await Task.Delay(100);  // Small delay to let the bottom sheet hide animation complete
                await ClassKeyboardMethods.ShowBottomSheet(CustomKeyboardHexadecimalPortrait, CustomKeyboardHexadecimalLandscape);

                // Scroll to the focused entry field in the scroll view
                await ClassKeyboardMethods.ScrollEntryToPosition(scrollView, entry, "grdTitleView", RootKeyboardHexadecimalPortrait.HeightRequest, RootKeyboardHexadecimalLandscape.HeightRequest);
            }
        }

        /// <summary>
        /// Entry unfocused event: format the text value for a hexadecimal numeric entry field with the number separator
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
                // Restore the color of the entry field and format the number
                ClassKeyboardMethods.SetEntryColorUnfocused(entry);
            }
        }

        /// <summary>
        /// Check if the value is decimal numeric
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
        /// Check if the value is hexadecimal numeric
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
                if (sender == entTest1 || sender == entTest2 || sender == entTest6)
                {
                    ClassEntryMethods.FormatDecimalNumberEntryUnfocused(entry);
                }
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
                _ = entTest6.Focus();
            }
            else if (sender == entTest6)
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
            Debug.WriteLine($"BtnKeyboardClicked: {cKey}");

            if (_focusedEntry != null)
            {
                if (cKey == "btnKeyboardHide")
                {
                    await ClassKeyboardMethods.HideBottomSheet(CustomKeyboardAlphanumericPortrait, CustomKeyboardAlphanumericLandscape);
                    await ClassKeyboardMethods.HideBottomSheet(CustomKeyboardDecimalPortrait, CustomKeyboardDecimalLandscape);
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

        private void pckKeyboardLayout_SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                ClassKeyboardMethods.cCurrentKeyboardLayout = picker.ItemsSource[selectedIndex] as string;

                // Restart the application
                try
                {
                    if (bRestartApplication)
                    {
                        // Save the selected keyboard layout in the application preferences
                        Preferences.Default.Set("SettingKeyboardLayoutSelectedIndex", selectedIndex);
                        Preferences.Default.Set("SettingKeyboardLayout", ClassKeyboardMethods.cCurrentKeyboardLayout);

                        // Give it some time to save the settings
                        Task.Delay(300).Wait();

                        // Restart the application
                        Application.Current!.Windows[0].Page = new AppShell();
                        //Application.Current!.Windows[0].Page = new NavigationPage(new MainPage());

                        Application.Current!.Windows[0].Page?.DisplayAlertAsync("Keyboard layout changed", "The app had to be restarted for the change to take effect.", "OK");
                    }
                }
                catch (Exception ex)
                {
#if DEBUG
                    DisplayAlertAsync("pckKeyboardLayout_SelectedIndexChanged", ex.Message, "OK");
#endif
                }

                bRestartApplication = true;

                Debug.WriteLine($"selectedIndex: {selectedIndex}");
            }
        }
    }
}