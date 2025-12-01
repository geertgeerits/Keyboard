namespace Keyboard
{
    public partial class KeyboardHexadecimal : ContentView
    {
        // Expose a bindable ICommand so pages can handle key presses via binding
        public static readonly BindableProperty KeyPressedCommandProperty =
            BindableProperty.Create(
                nameof(KeyPressedCommand),
                typeof(ICommand),
                typeof(KeyboardHexadecimal),
                default(ICommand));

        public ICommand? KeyPressedCommand
        {
            get => (ICommand?)GetValue(KeyPressedCommandProperty);
            set => SetValue(KeyPressedCommandProperty, value);
        }

        public KeyboardHexadecimal()
    	{
    		InitializeComponent();

            // Handle orientation changes
            UpdateOrientation(DeviceDisplay.MainDisplayInfo.Orientation);

            DeviceDisplay.MainDisplayInfoChanged += (s, e) =>
            {
                UpdateOrientation(e.DisplayInfo.Orientation);
            };
        }

        /// <summary>
        /// Update the visual state based on the device orientation
        /// </summary>
        /// <param name="orientation"></param>
        private void UpdateOrientation(DisplayOrientation orientation)
        {
            if (orientation == DisplayOrientation.Landscape)
            {
                VisualStateManager.GoToState(this, "Landscape");
            }
            else
            {
                VisualStateManager.GoToState(this, "Portrait");
            }
        }

        // Backwards-compatible: keep Click handler if you still want to support the messenger
        private void BtnKey_Clicked(object sender, EventArgs e)
        {
            // Optional: map and raise via command to unify behavior
            if (sender is Button btn && KeyPressedCommand?.CanExecute(btn.Text) == true)
            {
                KeyPressedCommand.Execute(btn.Text);
            }
            else if (sender is ImageButton ib && KeyPressedCommand?.CanExecute(ib.AutomationId) == true)
            {
                KeyPressedCommand.Execute(ib.AutomationId);
            }
            else
            {
                // Fallback to messenger for existing code paths
                string id = (sender as VisualElement)?.AutomationId ?? string.Empty;
                if (!string.IsNullOrEmpty(id))
                {
                    WeakReferenceMessenger.Default.Send(new StringMessage(id));
                }
            }
        }

        /// <summary>
        /// This method is called when a button is clicked, it sends a message with the key pressed to the page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void BtnKey_ClickedOLD(object sender, EventArgs e)
        //{
        //    string cKeyPressed = string.Empty;

        //    if (sender is Button button && !string.IsNullOrEmpty(button.AutomationId))
        //    {
        //        cKeyPressed = button.AutomationId switch
        //        {
        //            "btnZero" => "0",
        //            "btnOne" => "1",
        //            "btnTwo" => "2",
        //            "btnThree" => "3",
        //            "btnFour" => "4",
        //            "btnFive" => "5",
        //            "btnSix" => "6",
        //            "btnSeven" => "7",
        //            "btnEight" => "8",
        //            "btnNine" => "9",
        //            "btnA" => "A",
        //            "btnB" => "B",
        //            "btnC" => "C",
        //            "btnD" => "D",
        //            "btnE" => "E",
        //            "btnF" => "F",
        //            _ => button.AutomationId,
        //        };
        //    }

        //    if (sender is ImageButton imageButton && !string.IsNullOrEmpty(imageButton.AutomationId))
        //    {
        //        cKeyPressed = imageButton.AutomationId;
        //    }

        //    // Send the message with the key pressed to the page
        //    try
        //    {
        //        WeakReferenceMessenger.Default.Send(new StringMessage(cKeyPressed));
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine($"Error sending message: {ex.Message}");
        //    }
        //}

        /// <summary>
        /// Raise an event to notify the parent to hide the overlay
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void OnKeyboardHide_Clicked(object sender, EventArgs e)
        //{
        //    if (sender is ImageButton imageButton)
        //    {
        //        _ = WeakReferenceMessenger.Default.Send(new StringMessage(imageButton.AutomationId));
        //    }
        //}
    }
}