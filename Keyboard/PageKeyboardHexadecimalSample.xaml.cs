using CommunityToolkit.Mvvm.Messaging;
using System.Diagnostics;

namespace Keyboard;

public partial class PageKeyboardHexadecimalSample : ContentPage
{
    // Declare variables
    private string cEntryAutomationId = string.Empty;

    public PageKeyboardHexadecimalSample()
	{
		InitializeComponent();

        // Subscribe to orientation changes
        DeviceDisplay.MainDisplayInfoChanged += OnMainDisplayInfoChanged;

        // Show or hide the keyboard toggle button visibility
        imgbtnToggleKeyboard.IsVisible = ClassEntryMethods.bKeyboardToggleButton;
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
        ShowBottomSheet();
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
        CustomKeyboardHexadecimalPortrait.IsOpen = false;
        CustomKeyboardHexadecimalLandscape.IsOpen = false;
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

            //entry.CursorPosition = entry.Text.Length;  // The full selection of the text is gone when the cursor position is set to the end of the text

            cEntryAutomationId = entry.AutomationId;
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
        }
    }

    /// <summary>
    /// Check if the value is hexadecimal
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void NumberEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        if (!ClassEntryMethods.IsHexadecimalNumber((Entry)sender, e.NewTextValue))
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
    private void KeyboardHexadecimal_Closed(object sender, EventArgs e)
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

        // Hide the keyboard
        switch (cOrientation)
        {
            case "Landscape":
                {
                    CustomKeyboardHexadecimalLandscape.IsOpen = !CustomKeyboardHexadecimalLandscape.IsOpen;
                    break;
                }
            default:
                {
                    CustomKeyboardHexadecimalPortrait.IsOpen = !CustomKeyboardHexadecimalPortrait.IsOpen;
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
                    CustomKeyboardHexadecimalPortrait.IsOpen = false;
                    CustomKeyboardHexadecimalLandscape.IsOpen = true;
                    break;
                }
            default:
                {
                    CustomKeyboardHexadecimalLandscape.IsOpen = false;
                    CustomKeyboardHexadecimalPortrait.IsOpen = true;
                    break;
                }
        }
    }
}
