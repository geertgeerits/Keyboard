namespace Keyboard
{
    public partial class KeyboardAlphanumericLandscape : ContentView
    {
        public KeyboardAlphanumericLandscape()
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
                    "btn-1" => "1",
                    "btn-2" => "2",
                    "btn-3" => "3",
                    "btn-4" => "4",
                    "btn-5" => "5",
                    "btn-6" => "6",
                    "btn-7" => "7",
                    "btn-8" => "8",
                    "btn-9" => "9",
                    "btn-0" => "0",
                    "btn-A" => "A",
                    "btn-B" => "B",
                    "btn-C" => "C",
                    "btn-D" => "D",
                    "btn-E" => "E",
                    "btn-F" => "F",
                    "btn-G" => "G",
                    "btn-H" => "H",
                    "btn-I" => "I",
                    "btn-J" => "J",
                    "btn-K" => "K",
                    "btn-L" => "L",
                    "btn-M" => "M",
                    "btn-N" => "N",
                    "btn-O" => "O",
                    "btn-P" => "P",
                    "btn-Q" => "Q",
                    "btn-R" => "R",
                    "btn-S" => "S",
                    "btn-T" => "T",
                    "btn-U" => "U",
                    "btn-V" => "V",
                    "btn-W" => "W",
                    "btn-X" => "X",
                    "btn-Y" => "Y",
                    "btn-Z" => "Z",
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