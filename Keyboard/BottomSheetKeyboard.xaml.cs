using The49.Maui.BottomSheet;
using System.Diagnostics;
using CommunityToolkit.Mvvm.Messaging.Messages;
using CommunityToolkit.Mvvm.Messaging;

namespace Keyboard
{
    public partial class BottomSheetKeyboard : BottomSheet
    {
        public BottomSheetKeyboard()
        {
            InitializeComponent();

            //// Initialize the number format settings based on the current culture
            ClassEntryMethods.InitializeNumberFormat();

            btnMinus.Text = ClassEntryMethods.cNumNegativeSign;
            btnDecimalPoint.Text = ClassEntryMethods.cNumDecimalSeparator;
            btnZero.Text = ClassEntryMethods.cNumNativeDigits[..1];
            btnOne.Text = ClassEntryMethods.cNumNativeDigits.Substring(1, 1);
            btnTwo.Text = ClassEntryMethods.cNumNativeDigits.Substring(2, 1);
            btnThree.Text = ClassEntryMethods.cNumNativeDigits.Substring(3, 1);
            btnFour.Text = ClassEntryMethods.cNumNativeDigits.Substring(4, 1);
            btnFive.Text = ClassEntryMethods.cNumNativeDigits.Substring(5, 1);
            btnSix.Text = ClassEntryMethods.cNumNativeDigits.Substring(6, 1);
            btnSeven.Text = ClassEntryMethods.cNumNativeDigits.Substring(7, 1);
            btnEight.Text = ClassEntryMethods.cNumNativeDigits.Substring(8, 1);
            btnNine.Text = ClassEntryMethods.cNumNativeDigits.Substring(9, 1);
        }

        /// <summary>
        /// This method is called when a button is clicked, it sends a message with the key pressed to the MainPage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnKey_Clicked(object sender, EventArgs e)
        {
            if (sender is Button button && !string.IsNullOrEmpty(button.AutomationId))
            {
                string cKeyPressed = button.AutomationId switch
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
    }

    /// <summary>
    /// This class is used to send a message with a string value when a key is pressed on the keyboard
    /// </summary>
    /// <param name="value"></param>
    public class StringMessage(string value) : ValueChangedMessage<string>(value)
    {
    }
}
