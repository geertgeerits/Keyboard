namespace Keyboard
{
    public partial class PageKeyboardMixedSample : ContentPage
    {
        // Declare variables
        private string cEntryAutomationId = string.Empty;
        private Entry? _focusedEntry;
        private bool bShiftKeyEnabled;

        public PageKeyboardMixedSample()
    	{
    		InitializeComponent();

            // Subscribe to orientation changes
            DeviceDisplay.MainDisplayInfoChanged += OnMainDisplayInfoChanged;
        }

        /// <summary>
        /// To do when the page is appearing
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Subscribe to orientation changes
            DeviceDisplay.MainDisplayInfoChanged += OnMainDisplayInfoChanged;

            // Register to receive messages of type StringMessage from the keyboard bottom sheet
            WeakReferenceMessenger.Default.Register<StringMessage>(this, (recipient, message) =>
            {
                // Display the received message in the UI, this method is called when a message is received
                _ = BtnKeyboardClicked(message.Value);
                Debug.WriteLine($"Received message: {message.Value}");
            });
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

            // Unregister the message receiver to avoid memory leaks - if you don't do this, this receiver will be called if you are on another page
            WeakReferenceMessenger.Default.Unregister<StringMessage>(this);
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
            if (_focusedEntry is not null)
            {
                await ShowKeyboard(_focusedEntry);
            }

            if (sender is Entry entry)
            {
#if IOS
                entry.Focus();              // This will trigger the Focused event
#endif
            }
        }

        /// <summary>
        /// Show the keyboard bottom sheet
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        private async Task ShowKeyboard(Entry entry)
        {
            if (entry is not null)
            {
                switch (entry.AutomationId)
                {
                    case "entTest3":
                    case "entTest4":
                        await ClassKeyboardMethods.ShowBottomSheet(CustomKeyboardAlphanumericPortrait, CustomKeyboardAlphanumericLandscape);
                        break;
                    case "entTest1-Percentage":
                    case "entTest2":
                    case "entTest6":
                        await ClassKeyboardMethods.ShowBottomSheet(CustomKeyboardDecimalPortrait, CustomKeyboardDecimalLandscape);
                        break;
                    case "entTest5":
                        await ClassKeyboardMethods.ShowBottomSheet(CustomKeyboardHexadecimalPortrait, CustomKeyboardHexadecimalLandscape);
                        break;
                }
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
                cEntryAutomationId = entry.AutomationId;
                bShiftKeyEnabled = false;

                // Hide/Show the custom keyboard bottom sheet when the entry field is focused
                await ClassKeyboardMethods.HideBottomSheet(CustomKeyboardDecimalPortrait, CustomKeyboardDecimalLandscape);
                await ClassKeyboardMethods.HideBottomSheet(CustomKeyboardHexadecimalPortrait, CustomKeyboardHexadecimalLandscape);
                await Task.Delay(200);  // Small delay to let the bottom sheet hide animation complete
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
                _focusedEntry = null;
                cEntryAutomationId = entry.AutomationId;
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
                cEntryAutomationId = entry.AutomationId;
                bShiftKeyEnabled = false;

                // Set the unformatted number in the entry field
                await ClassEntryMethods.FormatDecimalNumberEntryFocused(entry);

                // Set the color of the entry field
                ClassKeyboardMethods.SetEntryColorFocused(entry);

                // Hide/Show the custom keyboard bottom sheet when the entry field is focused
                await ClassKeyboardMethods.HideBottomSheet(CustomKeyboardAlphanumericPortrait, CustomKeyboardAlphanumericLandscape);
                await ClassKeyboardMethods.HideBottomSheet(CustomKeyboardHexadecimalPortrait, CustomKeyboardHexadecimalLandscape);
                await Task.Delay(200);  // Small delay to let the bottom sheet hide animation complete
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
                _focusedEntry = null;
                cEntryAutomationId = entry.AutomationId;

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
                cEntryAutomationId = entry.AutomationId;
                bShiftKeyEnabled = false;

                // Set the color of the entry field
                ClassKeyboardMethods.SetEntryColorFocused(entry);

                // Hide/Show the custom keyboard bottom sheet when the entry field is focused
                await ClassKeyboardMethods.HideBottomSheet(CustomKeyboardAlphanumericPortrait, CustomKeyboardAlphanumericLandscape);
                await ClassKeyboardMethods.HideBottomSheet(CustomKeyboardDecimalPortrait, CustomKeyboardDecimalLandscape);
                await Task.Delay(200);  // Small delay to let the bottom sheet hide animation complete
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
                _focusedEntry = null;
                cEntryAutomationId = entry.AutomationId;

                // Restore the color of the entry field and format the number
                ClassKeyboardMethods.SetEntryColorUnfocused(entry);
            }
        }

        /// <summary>
        /// Check if the value is decimal numeric and clear result fields if the text have changed
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
        /// Check if the value is hexadecimal numeric and clear result fields if the text have changed
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
            // Format the number
            if (sender is Entry entry)
            {
#if WINDOWS
                ClassEntryMethods.FormatDecimalNumberEntryUnfocused(entry);
#endif
            }

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
            Debug.WriteLine($"BtnKeyboardClicked: {cKey}, Focused Entry AutomationId: {cEntryAutomationId}");

            Entry? focusedEntry = cEntryAutomationId switch
            {
                "entTest1-Percentage" => entTest1,
                "entTest2" => entTest2,
                "entTest3" => entTest3,
                "entTest4" => entTest4,
                "entTest5" => entTest5,
                "entTest6" => entTest6,
                _ => null
            };

            if (focusedEntry != null)
            {
                if (cKey == "btnKeyboardHide")
                {
                    await ClassKeyboardMethods.HideBottomSheet(CustomKeyboardAlphanumericPortrait, CustomKeyboardAlphanumericLandscape);
                    await ClassKeyboardMethods.HideBottomSheet(CustomKeyboardDecimalPortrait, CustomKeyboardDecimalLandscape);
                    await ClassKeyboardMethods.HideBottomSheet(CustomKeyboardHexadecimalPortrait, CustomKeyboardHexadecimalLandscape);
                }
                else if (cKey == "btnShift")
                {
                    bShiftKeyEnabled = !bShiftKeyEnabled;
                }
                else if (cKey == "btnReturn")
                {
                    GoToNextField(focusedEntry, null);
                }
                else
                {
                    if (bShiftKeyEnabled)
                    {
                        // Validate input: must be exactly one character
                        if (!string.IsNullOrEmpty(cKey) && cKey.Length == 1)
                        {
                            // Convert to lowercase
                            cKey = cKey.ToLower();
                        }
                    }

                    ClassKeyboardMethods.KeyboardKeyClicked(focusedEntry, cKey);
                }
            }
        }
    }
}