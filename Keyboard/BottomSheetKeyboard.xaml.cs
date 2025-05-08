using The49.Maui.BottomSheet;
using System.Diagnostics;
using CommunityToolkit.Mvvm.Messaging.Messages;
using CommunityToolkit.Mvvm.Messaging;

namespace Keyboard
{
    public partial class BottomSheetKeyboard : BottomSheet
    {
        //public event EventHandler<string>? KeyPressed;

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
        [Obsolete]
        private void BtnKey_Clicked(object sender, EventArgs e)
        {
            if (sender is Button button && !string.IsNullOrEmpty(button.AutomationId))
            {
                string cKeyPressed = string.Empty;

                switch (button.AutomationId)
                {
                    case "btnZero":
                        cKeyPressed = ClassEntryMethods.cNumNativeDigits[..1];
                        break;
                    case "btnOne":
                        cKeyPressed = ClassEntryMethods.cNumNativeDigits.Substring(1, 1);
                        break;
                    case "btnTwo":
                        cKeyPressed = ClassEntryMethods.cNumNativeDigits.Substring(2, 1);
                        break;
                    case "btnThree":
                        cKeyPressed = ClassEntryMethods.cNumNativeDigits.Substring(3, 1);
                        break;
                    case "btnFour":
                        cKeyPressed = ClassEntryMethods.cNumNativeDigits.Substring(4, 1);
                        break;
                    case "btnFive":
                        cKeyPressed = ClassEntryMethods.cNumNativeDigits.Substring(5, 1);
                        break;
                    case "btnSix":
                        cKeyPressed = ClassEntryMethods.cNumNativeDigits.Substring(6, 1);
                        break;
                    case "btnSeven":
                        cKeyPressed = ClassEntryMethods.cNumNativeDigits.Substring(7, 1);
                        break;
                    case "btnEight":
                        cKeyPressed = ClassEntryMethods.cNumNativeDigits.Substring(8, 1);
                        break;
                    case "btnNine":
                        cKeyPressed = ClassEntryMethods.cNumNativeDigits.Substring(9, 1);
                        break;
                    default:
                        cKeyPressed = button.AutomationId;
                        break;
                }

                try
                {
                    MessagingCenter.Send(this, "KeyPressed", cKeyPressed);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error sending message: {ex.Message}");
                }
            }
        }
    }
}
