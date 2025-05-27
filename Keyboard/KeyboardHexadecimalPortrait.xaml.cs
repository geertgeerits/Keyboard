using CommunityToolkit.Mvvm.Messaging;
using System.Diagnostics;

namespace Keyboard
{
    public partial class KeyboardHexadecimalPortrait : ContentView
    {
        public KeyboardHexadecimalPortrait()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This method is called when a button is clicked, it sends a message with the key pressed to the page
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

            // Send the message with the key pressed to the page
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
        /// Raise an event to notify the parent to hide the overlay
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyboardHide_Clicked(object sender, EventArgs e)
        {
            if (sender is ImageButton imageButton)
            {
                _ = WeakReferenceMessenger.Default.Send(new StringMessage(imageButton.AutomationId));
            }
        }
    }
}