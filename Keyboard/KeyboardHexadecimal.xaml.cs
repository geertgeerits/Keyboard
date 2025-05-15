using CommunityToolkit.Mvvm.Messaging;
using System.Diagnostics;
using Plugin.Maui.BottomSheet;
using Plugin.Maui.BottomSheet.Hosting;

namespace Keyboard;

public partial class KeyboardHexadecimal : ContentPage
{
    // Declare variables
    private string cEntryAutomationId = string.Empty;
    private bool bEntryCompleted;

    private string _buttonDecimalPointText = string.Empty;
    private string _buttonMinusText = string.Empty;

    // Properties for the button texts of the keyboard
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

    public KeyboardHexadecimal()
	{
		InitializeComponent();

        // Subscribe to orientation changes
        DeviceDisplay.MainDisplayInfoChanged += OnMainDisplayInfoChanged;

        // Register to receive messages of type StringMessage from the Keyboard page
        WeakReferenceMessenger.Default.Register<StringMessage>(this, (recipient, message) =>
        {
            // Display the received message in the UI, this method is called when a message is received
            BtnKeyboardClicked(message.Value);

            Debug.WriteLine($"Received message: {message.Value}");
        });

        // Set the BindingContext to this (the current page)
        this.BindingContext = this;

        ButtonDecimalPointText = ClassEntryMethods.cNumDecimalSeparator;
        ButtonMinusText = ClassEntryMethods.cNumNegativeSign;

        // Set the theme and the number color
        //Globals.SetTheme();
        ClassEntryMethods.SetNumberColor();

        // Open the bottom sheet when the page appears depending on the device orientation
        string cOrientation = Convert.ToString(GetDeviceOrientation()) ?? "Unknown";
        Debug.WriteLine($"MainPage - Orientation: {cOrientation}");

        switch (cOrientation)
        {
            case "Landscape":
                KeyboardHexadecimalLandscape.IsOpen = true;
                break;
            default:
                KeyboardHexadecimalPortrait.IsOpen = true;
                break;
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
        HideBottomSheet();
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
            entry.MaxLength = 18;

            if (bEntryCompleted)
            {
                //ClassEntryMethods.FormatNumberEntryFocused(entry);
            }

            cEntryAutomationId = entry.AutomationId;
            bEntryCompleted = false;

            // Hide the Android and iOS keyboard (method is in the class MauiProgram (MauiProgram.cs)
            _ = await entry.HideSoftInputAsync(System.Threading.CancellationToken.None);
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
                //ClassEntryMethods.FormatNumberEntryUnfocused(entry);
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
        //if (!ClassEntryMethods.IsNumeric((Entry)sender, e.NewTextValue))
        //{
        //    ((Entry)sender).Text = e.OldTextValue;
        //}
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
            //ClassEntryMethods.FormatNumberEntryUnfocused(entry);
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
    private void KeyboardHexadecimal_Opened(object sender, EventArgs e)
    {
        // Set the image source for the keyboard toggle button depending on the theme
        imgbtnToggleKeyboard.Source = Application.Current?.RequestedTheme switch
        {
            AppTheme.Dark => (ImageSource)"keyboard_hide_32p_white.png",
            _ => (ImageSource)"keyboard_hide_32p_black.png",
        };
    }

    /// <summary>
    /// Set the image source for the keyboard toggle button depending on the theme when the keyboard is closed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void KeyboardHexadecimal_Closed(object sender, EventArgs e)
    {
        // Set the image source for the keyboard toggle button depending on the theme
        imgbtnToggleKeyboard.Source = Application.Current?.RequestedTheme switch
        {
            AppTheme.Dark => (ImageSource)"keyboard_32p_white.png",
            _ => (ImageSource)"keyboard_32p_black.png",
        };
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

        // Hide the keyboard
        switch (cOrientation)
        {
            case "Landscape":
                {
                    KeyboardHexadecimalLandscape.IsOpen = !KeyboardHexadecimalLandscape.IsOpen;
                    break;
                }
            default:
                {
                    KeyboardHexadecimalPortrait.IsOpen = !KeyboardHexadecimalPortrait.IsOpen;
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
                    KeyboardHexadecimalPortrait.IsOpen = false;
                    KeyboardHexadecimalLandscape.IsOpen = true;
                    break;
                }
            default:
                {
                    KeyboardHexadecimalLandscape.IsOpen = false;
                    KeyboardHexadecimalPortrait.IsOpen = true;
                    break;
                }
        }
    }

    /// <summary>
    /// Hide the bottom sheet depending on the device orientation
    /// </summary>
    private void HideBottomSheet()
    {
        // Get the current device orientation
        string cOrientation = Convert.ToString(GetDeviceOrientation()) ?? "Unknown";

        // Hide the keyboard bottom sheet
        switch (cOrientation)
        {
            case "Landscape":
                {
                    KeyboardHexadecimalLandscape.IsOpen = false;
                    break;
                }
            default:
                {
                    KeyboardHexadecimalPortrait.IsOpen = false;
                    break;
                }
        }
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
                "btnZero" => "0",
                "btnOne" => "1",
                "btnTwo" => "2",
                "btnThree" => "3",
                "btnFour" => "4",
                "btnFive" => "5",
                "btnSix" => "6",
                "btnSeven" => "7",
                "btnEight" => "8",
                "btnNine" => "9",
                "btnA" => "A",
                "btnB" => "B",
                "btnC" => "C",
                "btnD" => "D",
                "btnE" => "E",
                "btnF" => "F",
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
        KeyboardHexadecimalPortrait.IsOpen = false;
        KeyboardHexadecimalLandscape.IsOpen = false;
    }
}
