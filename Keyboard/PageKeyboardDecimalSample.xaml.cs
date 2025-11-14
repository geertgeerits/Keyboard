
namespace Keyboard
{
    public partial class PageKeyboardDecimalSample : ContentPage
    {
        // Declare variables
        private string cEntryAutomationId = string.Empty;
        private Entry? _focusedEntry;

        public PageKeyboardDecimalSample()
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
        /// Handles the focus event for the numeric entry field, performing actions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void NumberEntryFocused(object sender, FocusEventArgs e)
        {
            if (sender is Entry entry)
            {
                _focusedEntry = entry;
                cEntryAutomationId = entry.AutomationId;

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
        /// Entry unfocused event: format the text value for a numeric entry field with the number separator
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumberEntryUnfocused(object sender, FocusEventArgs e)
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
        /// Handles the focus event for a text entry control, displaying the system soft input keyboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void TextEntryFocused(object sender, FocusEventArgs e)
        {
            // Hide the bottom sheet with the custom keyboard
            await ClassKeyboardMethods.HideBottomSheet(CustomKeyboardDecimalPortrait, CustomKeyboardDecimalLandscape);

            // Show the custom alphanumeric keyboard
            //await ClassKeyboardMethods.ShowBottomSheet(CustomKeyboardAlphanumericPortrait, CustomKeyboardAlphanumericLandscape);

            if (sender is Entry entry)
            {
                // Show the system soft input keyboard
                await entry.ShowSoftInputAsync(System.Threading.CancellationToken.None);
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
                // Hide the system soft input keyboard
                await entry.HideSoftInputAsync(System.Threading.CancellationToken.None);
            }

            // Show the bottom sheet with the custom keyboard
            // Needed for iOS, on Android and Windows the bottom sheet is already shown when the next entry field is focused
            await ClassKeyboardMethods.ShowBottomSheet(CustomKeyboardDecimalPortrait, CustomKeyboardDecimalLandscape);
        }

        /// <summary>
        /// Check if the value is numeric and clear result fields if the text have changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumberEntryTextChanged(object sender, TextChangedEventArgs e)
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
                    await ClassKeyboardMethods.ChangeKeyboardOrientation(CustomKeyboardDecimalPortrait, CustomKeyboardDecimalLandscape);
                }
                else if (cKey == "btnReturn")
                {
                    GoToNextField(focusedEntry, null);
                }
                else
                {
                    ClassKeyboardMethods.KeyboardDecimalClicked(focusedEntry, cKey);
                }
            }
        }

    }
}