/* Program .....: Keyboard.sln
   Author ......: Geert Geerits - E-mail: geertgeerits@gmail.com
   Copyright ...: (C) 2025-2025
   Version .....: 1.0.24
   Date ........: 2025-11-02 (YYYY-MM-DD)
   Language ....: Microsoft Visual Studio 2026: .NET 10.0 MAUI C# 14.0
   Description .: Custom keyboard for decimal and hexadecimal entry fields
   Note:........: This app is an example and experimental.
                  It is a custom keyboard that uses a ContentView as overlay page.
                  In iOS the 'await scrollView.ScrollToAsync(label, ScrollToPosition.Center, true)' does not work like in Android.
                  It centers horizontally and vertically for all the Entry controls in iOS even though the Orientation is only set to Vertical.
   Dependencies : NuGet Package: CommunityToolkit.Mvvm version 8.4.0 ; https://github.com/CommunityToolkit/dotnet
*/

using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System.Diagnostics;

namespace Keyboard
{
    public partial class MainPage : ContentPage
    {
        // Declare variables
        private string cEntryAutomationId = string.Empty;
        private bool bEntryCompleted;

        public MainPage()
        {
            // Initialize the number format settings based on the current culture - must be placed on the MainPage before InitializeComponent()
            ClassEntryMethods.InitializeNumberFormat();

            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error initializing MainPage: {ex.Message}\n{ex.StackTrace}");
            }

            // Select all the text in the entry field - works for all pages in the app
            ClassEntryMethods.ModifyEntrySelectAllText();

            // Set the theme
            ClassKeyboardMethods.SetTheme();

            // Set the image source for the keyboard toggle button
            ClassKeyboardMethods.SetImageKeyboardButtonSheetOpened(imgbtnToggleKeyboard);

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

            // Register to receive messages of type StringMessage from the keyboard bottom sheet
            WeakReferenceMessenger.Default.Register<StringMessage>(this, (recipient, message) =>
            {
                // Display the received message in the UI, this method is called when a message is received
                BtnKeyboardClicked(message.Value);
                Debug.WriteLine($"Received message: {message.Value}");
            });

            // Show the bottom sheet when the page is appearing
            ClassKeyboardMethods.ShowBottomSheet(CustomKeyboardDecimalPortrait, CustomKeyboardDecimalLandscape, imgbtnToggleKeyboard);
        }

        /// <summary>
        /// To do when the page is disappearing
        /// </summary>
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            // Hide the bottom sheet when the page is disappearing
            ClassKeyboardMethods.HideBottomSheet(CustomKeyboardDecimalPortrait, CustomKeyboardDecimalLandscape, imgbtnToggleKeyboard);

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
        private void OnMainDisplayInfoChanged(object? sender, DisplayInfoChangedEventArgs e)
        {
            ClassKeyboardMethods.ShowBottomSheet(CustomKeyboardDecimalPortrait, CustomKeyboardDecimalLandscape, imgbtnToggleKeyboard);
        }

        /// <summary>
        /// Handles the focus event for an entry field, performing actions
        /// </summary>
        /// <param name="sender">The entry field that triggered the focus event.</param>
        /// <param name="e">The event data associated with the focus event.</param>
        private void NumberEntryFocused(object sender, FocusEventArgs e)
        {
            if (sender is Entry entry)
            {
                // Set the color of the entry field
                ClassKeyboardMethods.SetEntryColorFocused(entry);

                // Hide the Android and iOS system keyboard
                // !!!BUG!!!?: when this method is called here, the entry field loses focus on iOS
                // The entry control's unfocused event is executed immediately after the focused event in net maui, only on iOS
                //ClassKeyboardMethods.HideSystemKeyboard(entry);

                // Show the keyboard bottom sheet when the entry field is focused and the keyboard toggle button is not visible
                if (!ClassKeyboardMethods.bKeyboardToggleButton)
                {
                    ClassKeyboardMethods.ShowBottomSheet(CustomKeyboardDecimalPortrait, CustomKeyboardDecimalLandscape, imgbtnToggleKeyboard);
                }

                if (bEntryCompleted)
                {
                    ClassEntryMethods.FormatDecimalNumberEntryFocused(entry);
                }

                cEntryAutomationId = entry.AutomationId;
                bEntryCompleted = false;

                //Point point = ClassKeyboardMethods.GetEntryScreenPosition(entry);
                //Debug.WriteLine($"Entry '{cEntryAutomationId}' position: {point.X}, {point.Y}");

                // Scroll to the focused entry field in the scroll view
                ClassKeyboardMethods.ScrollEntryToPosition(scrollView, entry, "grdTitleView", RootKeyboardDecimalPortrait.HeightRequest, RootKeyboardDecimalLandscape.HeightRequest);
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
                cEntryAutomationId = entry.AutomationId;

                //entry.MaxLength = -1;

                ClassKeyboardMethods.SetEntryColorUnfocused(entry);

                // Restore the color of the entry field and format the number
                if (bEntryCompleted)
                {
                    ClassEntryMethods.FormatDecimalNumberEntryUnfocused(entry);
                }
            }
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
        /// Go to the next field when the return key have been pressed 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoToNextField(object sender, EventArgs? e)
        {
            // Format the number
            if (sender is Entry entry)
            {
                bEntryCompleted = true;
                ClassEntryMethods.FormatDecimalNumberEntryUnfocused(entry);
            }

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
        private void BtnKeyboardClicked(string cKey)
        {
            Entry? focusedEntry = cEntryAutomationId switch
            {
                "entTest1-Percentage" => entTest1,
                "entTest2" => entTest2,
                "entTest3" => entTest3,
                "entTest4" => entTest4,
                "entTest5" => entTest5,
                _ => null
            };

            if (focusedEntry != null)
            {
                if (cKey == "btnKeyboardHide")
                {
                    ClassKeyboardMethods.ChangeKeyboardOrientation(CustomKeyboardDecimalPortrait, CustomKeyboardDecimalLandscape, imgbtnToggleKeyboard);
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

        /// <summary>
        /// Toggles the visibility of the numeric keyboard based on the current device orientation and theme.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImgbtnToggleKeyboard_Clicked(object sender, EventArgs e)
        {
            ClassKeyboardMethods.ChangeKeyboardOrientation(CustomKeyboardDecimalPortrait, CustomKeyboardDecimalLandscape, imgbtnToggleKeyboard);
        }

        /// <summary>
        /// Open the page with the decimal keyboard when the button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnDecimal_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageKeyboardDecimalSample());
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

    /// <summary>
    /// This class is used to send a message with a string value when a key is pressed on the keyboard - only used in MainPage.xaml.cs
    /// </summary>
    /// <param name="value"></param>
    public class StringMessage(string value) : ValueChangedMessage<string>(value)
    {
    }
}
