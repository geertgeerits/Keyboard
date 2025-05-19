/* Program .....: Keyboard.sln
   Author ......: Geert Geerits - E-mail: geertgeerits@gmail.com
   Copyright ...: (C) 2025-2025
   Version .....: 1.0.16
   Date ........: 2025-05-19 (YYYY-MM-DD)
   Language ....: Microsoft Visual Studio 2022: .NET 9.0 MAUI C# 13.0
   Description .: Custom keyboard for numeric entry fields
   Dependencies : NuGet Package: CommunityToolkit.Mvvm version 8.4.0 ; https://github.com/CommunityToolkit/dotnet
                  NuGet Package: Plugin.Maui.BottomSheet by Luca Civale version 9.1.5; https://github.com/lucacivale/Maui.BottomSheet
   Thanks to ...: Gerald Versluis for his video's on YouTube about .NET MAUI - https://www.youtube.com/watch?v=bdKWnddRDY0&t=856s
*/

using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System.Diagnostics;
using Plugin.Maui.BottomSheet;
using Plugin.Maui.BottomSheet.Hosting;

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

            // Register to receive messages of type StringMessage from the keyboard bottom sheet
            WeakReferenceMessenger.Default.Register<StringMessage>(this, (recipient, message) =>
            {
                // Display the received message in the UI, this method is called when a message is received
                BtnKeyboardClicked(message.Value);

                Debug.WriteLine($"Received message: {message.Value}");
            });

            // Select all the text in the entry field - works for all pages in the app
            ClassEntryMethods.ModifyEntrySelectAllText();

            // Show or hide the keyboard toggle button visibility
            imgbtnToggleKeyboard.IsVisible = ClassEntryMethods.bKeyboardToggleButton;

            // Set the theme
            //Globals.SetTheme();

            // Open the bottom sheet when this page appears depending on the device orientation
            string cOrientation = Convert.ToString(GetDeviceOrientation()) ?? "Unknown";
            Debug.WriteLine($"MainPage - Orientation: {cOrientation}");

            switch (cOrientation)
            {
                case "Landscape":
                    CustomKeyboardDecimalLandscape.IsOpen = true;
                    break;
                default:
                    CustomKeyboardDecimalPortrait.IsOpen = true;
                    break;
            }
        }

        // Subscribe to orientation changes
        protected override void OnAppearing()
        {
            base.OnAppearing();
            DeviceDisplay.MainDisplayInfoChanged += OnMainDisplayInfoChanged;
        }

        // Unsubscribe to orientation changes - if you don't do this, the event will be called if you are on another page
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            DeviceDisplay.MainDisplayInfoChanged -= OnMainDisplayInfoChanged;
        }

        /// <summary>
        /// Show the bottom sheet when the page is appearing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            ShowBottomSheet();
        }

        /// <summary>
        /// Hide the bottom sheet when the page is disappearing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContentPage_Disappearing(object sender, EventArgs e)
        {
            CustomKeyboardDecimalPortrait.IsOpen = false;
            CustomKeyboardDecimalLandscape.IsOpen = false;
        }

        /// <summary>
        /// Get the current device orientation
        /// </summary>
        /// <returns></returns>
        public static DisplayOrientation GetDeviceOrientation()
        {
            // Get the current display information
            var displayInfo = DeviceDisplay.MainDisplayInfo;

            // Return the orientation
            Debug.WriteLine($"DisplayOrientation: {displayInfo.Orientation}");
            return displayInfo.Orientation;
        }

        /// <summary>
        /// This method is called when the display information changes, it handles the orientation change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMainDisplayInfoChanged(object? sender, DisplayInfoChangedEventArgs e)
        {
            ShowBottomSheet();
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
        /// Entry focused event: format the text value for a numeric entry without the number separator
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void NumberEntryFocused(object sender, FocusEventArgs e)
        {
            if (sender is Entry entry)
            {
                // Hide the Android and iOS keyboard (method is in the class MauiProgram (MauiProgram.cs)
                _ = await entry.HideSoftInputAsync(System.Threading.CancellationToken.None);

                // Show the keyboard bottom sheet when the entry field is focused and the keyboard toggle button is not visible
                if (!ClassEntryMethods.bKeyboardToggleButton)
                {
                    ShowBottomSheet();
                }

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

                entry.MaxLength = -1;

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
                switch (cKey)
                {
                    case "btnReturn":
                        GoToNextField(focusedEntry, null);
                        return;
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
        /// Inserts a character at the current cursor position in the Entry field.
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="cCharacter"></param>
        /// <returns></returns>
        private static string InsertCharacterInEntryField(Entry entry, string cCharacter)
        {
            // Get the current text in the Entry
            string currentText = entry.Text ?? string.Empty;

            // Insert the character at the cursor position
            string newText = currentText.Insert(entry.CursorPosition, cCharacter);

            // Update the Entry's text
            entry.Text = newText;

            // Move the cursor to the position after the inserted character
            entry.CursorPosition++;

            return newText;
        }

        /// <summary>
        /// Deletes the character before the current cursor position in the Entry field.
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        private static string DeleteCharacterBeforeCursor(Entry entry)
        {
            // Get the current text in the Entry
            string currentText = entry.Text ?? string.Empty;

            // Ensure there is a character to delete and the cursor is not at the start
            if (entry.CursorPosition > 0 && currentText.Length > 0)
            {
                // Remove the character before the cursor position
                string newText = currentText.Remove(entry.CursorPosition - 1, 1);

                // Update the Entry's text
                entry.Text = newText;

                // Move the cursor to the position before the deleted character
                //entry.CursorPosition--;

                return newText;
            }

            return currentText;
        }

        /// <summary>
        /// Set the image source for the keyboard toggle button depending on the theme when the keyboard is opened
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An object that contains the event data.</param>
        private void KeyboardDecimal_Opened(object sender, EventArgs e)
        {
            if (ClassEntryMethods.bKeyboardToggleButton)
            {
                imgbtnToggleKeyboard.Source = Application.Current?.RequestedTheme switch
                {
                    AppTheme.Dark => (ImageSource)ClassEntryMethods.cImageKeyboardHideDark,
                    _ => (ImageSource)ClassEntryMethods.cImageKeyboardHideLight,
                };
            }
        }

        /// <summary>
        /// Set the image source for the keyboard toggle button depending on the theme when the keyboard is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyboardDecimal_Closed(object sender, EventArgs e)
        {
            if (ClassEntryMethods.bKeyboardToggleButton)
            {
                imgbtnToggleKeyboard.Source = Application.Current?.RequestedTheme switch
                {
                    AppTheme.Dark => (ImageSource)ClassEntryMethods.cImageKeyboardShowDark,
                    _ => (ImageSource)ClassEntryMethods.cImageKeyboardShowLight,
                };
            }
        }

        /// <summary>
        /// Toggles the visibility of the numeric keyboard based on the current device orientation and theme.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImgbtnToggleKeyboard_Clicked(object sender, EventArgs e)
        {
            // Get the current device orientation
            string cOrientation = Convert.ToString(GetDeviceOrientation()) ?? "Unknown";

            // Hide or show the keyboard
            switch (cOrientation)
            {
                case "Landscape":
                    {
                        CustomKeyboardDecimalLandscape.IsOpen = !CustomKeyboardDecimalLandscape.IsOpen;
                        break;
                    }
                default:
                    {
                        CustomKeyboardDecimalPortrait.IsOpen = !CustomKeyboardDecimalPortrait.IsOpen;
                        break;
                    }
            }
        }

        /// <summary>
        /// Show the bottom sheet depending on the device orientation
        /// </summary>
        private void ShowBottomSheet()
        {
            // Get the current device orientation
            string cOrientation = Convert.ToString(GetDeviceOrientation()) ?? "Unknown";

            // Show the keyboard bottom sheet
            switch (cOrientation)
            {
                case "Landscape":
                    {
                        CustomKeyboardDecimalPortrait.IsOpen = false;
                        CustomKeyboardDecimalLandscape.IsOpen = true;
                        break;
                    }
                default:
                    {
                        CustomKeyboardDecimalLandscape.IsOpen = false;
                        CustomKeyboardDecimalPortrait.IsOpen = true;
                        break;
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
    /// This class is used to send a message with a string value when a key is pressed on the keyboard - only used in the MainPage.xaml.cs
    /// </summary>
    /// <param name="value"></param>
    public class StringMessage(string value) : ValueChangedMessage<string>(value)
    {
    }
}
