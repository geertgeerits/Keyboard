/* Program .....: Keyboard.sln
   Author ......: Geert Geerits - E-mail: geertgeerits@gmail.com
   Copyright ...: (C) 2025-2025
   Version .....: 1.0.18
   Date ........: 2025-05-22 (YYYY-MM-DD)
   Language ....: Microsoft Visual Studio 2022: .NET 9.0 MAUI C# 13.0
   Description .: Custom keyboard for decimal and hexadecimal entry fields
   Note:........: This app is an example and experimental.
                  It is a custom keyboard that uses a bottomsheet.
                  This works reasonably well in Android but less so in iOS.
                  The bottomsheet is apparently always modal in iOS and does not always open when switching from
                  portrait to landscape (and vice versa) if a different keyboard layout is used for portrait and landscape.
   Dependencies : NuGet Package: CommunityToolkit.Mvvm version 8.4.0 ; https://github.com/CommunityToolkit/dotnet
                  NuGet Package: Plugin.Maui.BottomSheet by Luca Civale version 9.1.5; https://github.com/lucacivale/Maui.BottomSheet
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

            // Show or hide the keyboard toggle button visibility
            imgbtnToggleKeyboard.IsVisible = ClassKeyboardMethods.bKeyboardToggleButton;

            // Set the theme
            ClassKeyboardMethods.SetTheme();
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

            // Delay to ensure the bottom sheet is shown after the page is fully loaded - needed for iOS
            _ = Task.Delay(200).ContinueWith(_ =>
            {
                // Show the bottom sheet when the page is appearing
                ClassKeyboardMethods.ShowBottomSheet(CustomKeyboardDecimalPortrait, CustomKeyboardDecimalLandscape);
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// To do when the page is disappearing
        /// </summary>
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            
            // Unsubscribe to orientation changes - if you don't do this, this event will be called if you are on another page
            DeviceDisplay.MainDisplayInfoChanged -= OnMainDisplayInfoChanged;

            // Unregister the message receiver to avoid memory leaks - if you don't do this, this receiver will be called if you are on another page
            WeakReferenceMessenger.Default.Unregister<StringMessage>(this);

            // Hide the bottom sheet when the page is disappearing
            CustomKeyboardDecimalPortrait.IsOpen = false;
            CustomKeyboardDecimalLandscape.IsOpen = false;
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
            ClassKeyboardMethods.ShowBottomSheet(CustomKeyboardDecimalPortrait, CustomKeyboardDecimalLandscape);
        }

        // Fix for CS1002 and CS0029 errors in the NumberEntryFocused method
        private async void NumberEntryFocused(object sender, FocusEventArgs e)
        {
            if (sender is Entry entry)
            {
                // Hide the Android and iOS keyboard (method is in the class MauiProgram (MauiProgram.cs)
                _ = await entry.HideSoftInputAsync(System.Threading.CancellationToken.None);

                // Show the keyboard bottom sheet when the entry field is focused and the keyboard toggle button is not visible
                if (!ClassKeyboardMethods.bKeyboardToggleButton)
                {
                    ClassKeyboardMethods.ShowBottomSheet(CustomKeyboardDecimalPortrait, CustomKeyboardDecimalLandscape);
                }

                // Set the border color if the entry is focused
                Border border = (Border)entry.Parent;
                border.Stroke = Colors.DarkGreen;

                if (bEntryCompleted)
                {
                    ClassEntryMethods.FormatDecimalNumberEntryFocused(entry);
                }

                cEntryAutomationId = entry.AutomationId;
                bEntryCompleted = false;
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

                if (bEntryCompleted)
                {
                    ClassEntryMethods.FormatDecimalNumberEntryUnfocused(entry);
                }

                // Set the border color if the entry is unfocused
                Border border = (Border)entry.Parent;
                border.Stroke = Colors.Blue;
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
                _ => null
            };

            if (focusedEntry != null)
            {
                if (cKey == "btnReturn")
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
        /// Event when the keyboard bottom sheet is opened
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An object that contains the event data.</param>
        private void KeyboardDecimal_Opened(object sender, EventArgs e)
        {
            ClassKeyboardMethods.KeyboardBottomSheetOpened(imgbtnToggleKeyboard);
        }

        /// <summary>
        /// Event when the keyboard bottom sheet is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyboardDecimal_Closed(object sender, EventArgs e)
        {
            ClassKeyboardMethods.KeyboardBottomSheetClosed(imgbtnToggleKeyboard);
        }

        /// <summary>
        /// Toggles the visibility of the numeric keyboard based on the current device orientation and theme.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImgbtnToggleKeyboard_Clicked(object sender, EventArgs e)
        {
            ClassKeyboardMethods.ImgbtnToggleKeyboardClicked(CustomKeyboardDecimalPortrait, CustomKeyboardDecimalLandscape);
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
