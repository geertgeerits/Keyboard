/* Program .....: Keyboard.sln
   Author ......: Geert Geerits - E-mail: geertgeerits@gmail.com
   Copyright ...: (C) 2025-2025
   Version .....: 1.0.15
   Date ........: 2025-05-14 (YYYY-MM-DD)
   Language ....: Microsoft Visual Studio 2022: .NET 9.0 MAUI C# 13.0
   Description .: Custom keyboard for numeric entry fields
   Dependencies : NuGet Package: CommunityToolkit.Mvvm version 8.4.0 ; https://github.com/CommunityToolkit/dotnet
                  NuGet Package: Plugin.Maui.BottomSheet by Luca Civale version 9.1.5; https://github.com/lucacivale/Maui.BottomSheet
   Thanks to ...: Gerald Versluis for his video's on YouTube about .NET MAUI - https://www.youtube.com/watch?v=bdKWnddRDY0&t=856s
*/

using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Animations;
using Microsoft.Maui.Controls;
using System.Diagnostics;
using Plugin.Maui.BottomSheet;
using Plugin.Maui.BottomSheet.Hosting;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Keyboard
{
    public partial class MainPage : ContentPage
    {
        private string cEntryAutomationId = string.Empty;
        private bool bEntryCompleted;

        private string _buttonZeroText;
        private string _buttonOneText;
        private string _buttonTwoText;
        private string _buttonThreeText;
        private string _buttonFourText;
        private string _buttonFiveText;
        private string _buttonSixText;
        private string _buttonSevenText;
        private string _buttonEightText;
        private string _buttonNineText;
        private string _buttonDecimalPointText;
        private string _buttonMinusText;

        public string ButtonZeroText
        {
            get => _buttonZeroText;
            set
            {
                _buttonZeroText = value;
                OnPropertyChanged();
            }
        }

        public string ButtonOneText
        {
            get => _buttonOneText;
            set
            {
                _buttonOneText = value;
                OnPropertyChanged();
            }
        }
        public string ButtonTwoText
        {
            get => _buttonTwoText;
            set
            {
                _buttonTwoText = value;
                OnPropertyChanged();
            }
        }
        public string ButtonThreeText
        {
            get => _buttonThreeText;
            set
            {
                _buttonThreeText = value;
                OnPropertyChanged();
            }
        }
        public string ButtonFourText
        {
            get => _buttonFourText;
            set
            {
                _buttonFourText = value;
                OnPropertyChanged();
            }
        }
        public string ButtonFiveText
        {
            get => _buttonFiveText;
            set
            {
                _buttonFiveText = value;
                OnPropertyChanged();
            }
        }

        public string ButtonSixText
        {
            get => _buttonSixText;
            set
            {
                _buttonSixText = value;
                OnPropertyChanged();
            }
        }

        public string ButtonSevenText
        {
            get => _buttonSevenText;
            set
            {
                _buttonSevenText = value;
                OnPropertyChanged();
            }
        }

        public string ButtonEightText
        {
            get => _buttonEightText;
            set
            {
                _buttonEightText = value;
                OnPropertyChanged();
            }
        }

        public string ButtonNineText
        {
            get => _buttonNineText;
            set
            {
                _buttonNineText = value;
                OnPropertyChanged();
            }
        }

        public string ButtonDecimalPointText
        {
            get => _buttonDecimalPointText;
            set
            {
                _buttonDecimalPointText = value;
                OnPropertyChanged();
            }
        }

        public string ButtonMinusText
        {
            get => _buttonMinusText;
            set
            {
                _buttonMinusText = value;
                OnPropertyChanged();
            }
        }

        public MainPage()
        {
            InitializeComponent();

            // Set background color of the bottomsheet - if BottomSheet is non-modal no window color is applied
            //KeyboardNumericPortrait.WindowBackgroundColor = Colors.DarkGray;

            // Subscribe to orientation changes
            DeviceDisplay.MainDisplayInfoChanged += OnMainDisplayInfoChanged;

            // Register to receive messages of type StringMessage from the KeyboardNumericPortrait page
            WeakReferenceMessenger.Default.Register<StringMessage>(this, (recipient, message) =>
            {
                // Display the received message in the UI, this method is called when a message is received
                BtnKeyboardClicked(message.Value);
                
                Debug.WriteLine($"Received message: {message.Value}");
            });
            
            // Initialize the number format settings based on the current culture
            ClassEntryMethods.InitializeNumberFormat();

            // Set the BindingContext to this (the current page)
            this.BindingContext = this;

            ButtonZeroText = ClassEntryMethods.cNumNativeDigits[..1];
            ButtonOneText = ClassEntryMethods.cNumNativeDigits.Substring(1, 1);
            ButtonTwoText = ClassEntryMethods.cNumNativeDigits.Substring(2, 1);
            ButtonThreeText = ClassEntryMethods.cNumNativeDigits.Substring(3, 1);
            ButtonFourText = ClassEntryMethods.cNumNativeDigits.Substring(4, 1);
            ButtonFiveText = ClassEntryMethods.cNumNativeDigits.Substring(5, 1);
            ButtonSixText = ClassEntryMethods.cNumNativeDigits.Substring(6, 1);
            ButtonSevenText = ClassEntryMethods.cNumNativeDigits.Substring(7, 1);
            ButtonEightText = ClassEntryMethods.cNumNativeDigits.Substring(8, 1);
            ButtonNineText = ClassEntryMethods.cNumNativeDigits.Substring(9, 1);
            ButtonDecimalPointText = ClassEntryMethods.cNumDecimalSeparator;
            ButtonMinusText = ClassEntryMethods.cNumNegativeSign;

            // Set the theme and the number color
            //Globals.SetTheme();
            ClassEntryMethods.SetNumberColor();

            // Open the bottom sheet when the page appears depending on the device orientation
            string cOrientation = Convert.ToString(GetDeviceOrientation()) ?? "Unknown";
            Debug.WriteLine($"MainPage - Orientation: {cOrientation}");

            if (cOrientation == "Portrait")
            {
                KeyboardNumericPortrait.IsModal = false;
                KeyboardNumericPortrait.IsOpen = true;
            }
            else if (cOrientation == "Landscape")
            {
                KeyboardNumericLandscape.IsModal = false;
                KeyboardNumericLandscape.IsOpen = true;
            }
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
            OnHideBottomSheetClicked(sender, e);
            OnShowBottomSheetClicked(sender, e);
        }

        /// <summary>
        /// Set focus to the first entry field 
        /// Add in the header of the xaml page: 'Loaded="OnPageLoaded"' 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPageLoaded(object sender, EventArgs e)
        {
            _ = entTest1.Focus();
        }

        /// <summary>
        /// Entry focused event: format the text value for a numeric entry without the number separator and select the entire text value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void NumberEntryFocused(object sender, FocusEventArgs e)
        {
            if (sender is Entry entry)
            {
                entry.MaxLength = 18;

                if (bEntryCompleted)
                {
                    ClassEntryMethods.FormatNumberEntryFocused(entry);
                }

                cEntryAutomationId = entry.AutomationId;
                bEntryCompleted = false;

                // Hide the Android and iOS keyboard
                _ = await entry.HideSoftInputAsync(System.Threading.CancellationToken.None);

                //entry.CursorPosition = entry.Text.Length;
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
                    ClassEntryMethods.FormatNumberEntryUnfocused(entry);
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
            if (!ClassEntryMethods.IsNumeric((Entry)sender, e.NewTextValue))
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
                ClassEntryMethods.FormatNumberEntryUnfocused(entry);

                //ClassEntryMethods.HideKeyboard(entry);
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

            //bEntryCompleted = false;
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
        /// Handles the click event for the button to show the bottom sheet depending on the device orientation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnShowBottomSheetClicked(object? sender, EventArgs e)
        {
            // Get the current device orientation
            string cOrientation = Convert.ToString(GetDeviceOrientation()) ?? "Unknown";
            Debug.WriteLine($"OnShowBottomSheetClicked - Orientation changed to: {cOrientation}");

            // Show the keyboard
            switch (cOrientation)
            {
                case "Portrait":
                    {
                        KeyboardNumericLandscape.IsOpen = false;
                        KeyboardNumericPortrait.IsModal = false;
                        KeyboardNumericPortrait.IsOpen = true;
                        break;
                    }

                case "Landscape":
                    {
                        KeyboardNumericPortrait.IsOpen = false;
                        KeyboardNumericLandscape.IsModal = false;
                        KeyboardNumericLandscape.IsOpen = true;
                        break;
                    }
            }
        }

        /// <summary>
        /// Handles the click event for the button to hide the bottom sheet - only works when HasBackdrop is enabled
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnHideBottomSheetClicked(object? sender, EventArgs e)
        {
            // Get the current device orientation
            string cOrientation = Convert.ToString(GetDeviceOrientation()) ?? "Unknown";
            Debug.WriteLine($"OnHideBottomSheetClicked - Orientation changed to: {cOrientation}");

            // Hide the keyboard
            switch (cOrientation)
            {
                case "Portrait":
                    {
                        KeyboardNumericPortrait.IsOpen = false;
                        break;
                    }

                case "Landscape":
                    {
                        KeyboardNumericLandscape.IsOpen = false;
                        break;
                    }
            }

            //KeyboardNumeric.IsOpen = false;
        }

        /// <summary>
        /// This method is called when a button is clicked, it sends a message with the key pressed to the MainPage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnKey_Clicked(object sender, EventArgs e)
        {
            string cKeyPressed = string.Empty;

            if (sender is Button button && !string.IsNullOrEmpty(button.AutomationId))
            {
                cKeyPressed = button.AutomationId switch
                {
                    "btnZero" => ClassEntryMethods.cNumNativeDigits[..1],
                    "btnOne" => ClassEntryMethods.cNumNativeDigits.Substring(1, 1),
                    "btnTwo" => ClassEntryMethods.cNumNativeDigits.Substring(2, 1),
                    "btnThree" => ClassEntryMethods.cNumNativeDigits.Substring(3, 1),
                    "btnFour" => ClassEntryMethods.cNumNativeDigits.Substring(4, 1),
                    "btnFive" => ClassEntryMethods.cNumNativeDigits.Substring(5, 1),
                    "btnSix" => ClassEntryMethods.cNumNativeDigits.Substring(6, 1),
                    "btnSeven" => ClassEntryMethods.cNumNativeDigits.Substring(7, 1),
                    "btnEight" => ClassEntryMethods.cNumNativeDigits.Substring(8, 1),
                    "btnNine" => ClassEntryMethods.cNumNativeDigits.Substring(9, 1),
                    _ => button.AutomationId,
                };
            }

            if (sender is ImageButton imageButton && !string.IsNullOrEmpty(imageButton.AutomationId))
            {
                cKeyPressed = imageButton.AutomationId;
            }

            // Send the message with the key pressed to the MainPage
            try
            {
                WeakReferenceMessenger.Default.Send(new StringMessage(cKeyPressed));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error sending message: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// This class is used to send a message with a string value when a key is pressed on the keyboard
    /// </summary>
    /// <param name="value"></param>
    public class StringMessage(string value) : ValueChangedMessage<string>(value)
    {
    }
}
