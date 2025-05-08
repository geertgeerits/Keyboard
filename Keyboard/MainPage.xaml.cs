/*
2025-05-08

https://www.youtube.com/watch?v=bdKWnddRDY0&t=856s
*/

using The49.Maui.BottomSheet;
using CommunityToolkit.Mvvm.Messaging.Messages;
using CommunityToolkit.Mvvm.Messaging;
using System.Diagnostics;
using Microsoft.Maui.Controls;

namespace Keyboard
{
    public partial class MainPage : ContentPage
    {
        private string cNameEntryField = string.Empty;
        private bool bEntryCompleted = false;

        public MainPage()
        {
            InitializeComponent();

            WeakReferenceMessenger.Default.Register<StringMessage>(this, (recipient, message) =>
            {
                DisplayReceivedMessage(message.Value);
                Debug.WriteLine($"Received message: {message.Value}");
            });

            //// Initialize the number format settings based on the current culture
            ClassEntryMethods.InitializeNumberFormat();

            //// Set the theme and the number color
            //Globals.SetTheme();
            ClassEntryMethods.SetNumberColor();

            MessagingCenter.Subscribe<BottomSheetKeyboard, string>(this, "KeyPressed", (sender, key) =>
            {
                Debug.WriteLine($"Key Pressed: {key}");
                //BtnKeyboardClicked(sender, key);
                //BtnKeyboardClicked(key);
            });

            // Open the bottom sheet when the page appears
            var page = new BottomSheetKeyboard();
            page.ShowAsync();
        }

        private void DisplayReceivedMessage(string message)
        {
            // Handle the received message (e.g., display it in a Label)
            //ReceivedMessageLabel.Text = message;
            BtnKeyboardClicked(message);
            Debug.WriteLine($"Received message: {message}");
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

                cNameEntryField = entry.AutomationId;
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
                cNameEntryField = entry.AutomationId;

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
            //var views = rootLayout.Children;
            //foreach (View view in views)
            //{
            //    if (view != null && view.IsFocused)
            //    {
            //        System.Diagnostics.Debug.WriteLine("Focused view: " + view);
            //    }
            //}

            Entry? focusedEntry = cNameEntryField switch
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
                    case "btnDelete":
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
        /// Handles the click event for the button to show the bottom sheet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnShowBottomSheetClicked(object sender, EventArgs e)
        {
            BottomSheetKeyboard sheet = new()
            {
                HasHandle = true,
                HandleColor = Colors.Red
            };

            await sheet.ShowAsync(Window);

            //if (page.Showing)
            //{
            //    // If the bottom sheet is already showing, dismiss it
            //    await page.DismissAsync();
            //}
            //else
            //{
            //    await page.ShowAsync(Window);
            //}

            //if (page != null)
            //{
            //    await page.DismissAsync();
            //}
            //else
            //{
            //    await page.ShowAsync(Window);
            //}
        }

        /// <summary>
        /// Handles the click event for the button to hide the bottom sheet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnHideBottomSheetClicked(object sender, EventArgs e)
        {
            BottomSheetKeyboard sheet = new()
            {
            };

            await sheet.DismissAsync();
        }
    }
}
