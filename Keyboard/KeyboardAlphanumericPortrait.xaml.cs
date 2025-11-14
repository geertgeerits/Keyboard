namespace Keyboard
{
    public partial class KeyboardAlphanumericPortrait : ContentView
    {
        // Declare variables for binding properties
        private string _button_1_Text = string.Empty;
        private string _button_2_Text = string.Empty;
        private string _button_3_Text = string.Empty;
        private string _button_4_Text = string.Empty;
        private string _button_5_Text = string.Empty;
        private string _button_6_Text = string.Empty;
        private string _button_7_Text = string.Empty;
        private string _button_8_Text = string.Empty;
        private string _button_9_Text = string.Empty;
        private string _button_10_Text = string.Empty;
        private string _button_11_Text = string.Empty;
        private string _button_12_Text = string.Empty;
        private string _button_13_Text = string.Empty;
        private string _button_14_Text = string.Empty;
        private string _button_15_Text = string.Empty;
        private string _button_16_Text = string.Empty;
        private string _button_17_Text = string.Empty;
        private string _button_18_Text = string.Empty;
        private string _button_19_Text = string.Empty;
        private string _button_20_Text = string.Empty;

        private string _buttonDecimalPointText = string.Empty;
        private string _buttonMinusText = string.Empty;

        // Properties for the button texts of the keyboard
        public string Button_1_Text
        {
            get => _button_1_Text;
            set
            {
                _button_1_Text = value;
                OnPropertyChanged();
            }
        }

        public string Button_2_Text
        {
            get => _button_2_Text;
            set
            {
                _button_2_Text = value;
                OnPropertyChanged();
            }
        }

        public string Button_3_Text
        {
            get => _button_3_Text;
            set
            {
                _button_3_Text = value;
                OnPropertyChanged();
            }
        }

        public string Button_4_Text
        {
            get => _button_4_Text;
            set
            {
                _button_4_Text = value;
                OnPropertyChanged();
            }
        }

        public string Button_5_Text
        {
            get => _button_5_Text;
            set
            {
                _button_5_Text = value;
                OnPropertyChanged();
            }
        }

        public string Button_6_Text
        {
            get => _button_6_Text;
            set
            {
                _button_6_Text = value;
                OnPropertyChanged();
            }
        }

        public string Button_7_Text
        {
            get => _button_7_Text;
            set
            {
                _button_7_Text = value;
                OnPropertyChanged();
            }
        }

        public string Button_8_Text
        {
            get => _button_8_Text;
            set
            {
                _button_8_Text = value;
                OnPropertyChanged();
            }
        }

        public string Button_9_Text
        {
            get => _button_9_Text;
            set
            {
                _button_9_Text = value;
                OnPropertyChanged();
            }
        }
        public string Button_10_Text
        {
            get => _button_10_Text;
            set
            {
                _button_10_Text = value;
                OnPropertyChanged();
            }
        }

        public string Button_11_Text
        {
            get => _button_11_Text;
            set
            {
                _button_11_Text = value;
                OnPropertyChanged();
            }
        }

        public string Button_12_Text
        {
            get => _button_12_Text;
            set
            {
                _button_12_Text = value;
                OnPropertyChanged();
            }
        }

        public string Button_13_Text
        {
            get => _button_13_Text;
            set
            {
                _button_13_Text = value;
                OnPropertyChanged();
            }
        }

        public string Button_14_Text
        {
            get => _button_14_Text;
            set
            {
                _button_14_Text = value;
                OnPropertyChanged();
            }
        }

        public string Button_15_Text
        {
            get => _button_15_Text;
            set
            {
                _button_15_Text = value;
                OnPropertyChanged();
            }
        }

        public string Button_16_Text
        {
            get => _button_16_Text;
            set
            {
                _button_16_Text = value;
                OnPropertyChanged();
            }
        }

        public string Button_17_Text
        {
            get => _button_17_Text;
            set
            {
                _button_17_Text = value;
                OnPropertyChanged();
            }
        }

        public string Button_18_Text
        {
            get => _button_18_Text;
            set
            {
                _button_18_Text = value;
                OnPropertyChanged();
            }
        }

        public string Button_19_Text
        {
            get => _button_19_Text;
            set
            {
                _button_19_Text = value;
                OnPropertyChanged();
            }
        }

        public string Button_20_Text
        {
            get => _button_20_Text;
            set
            {
                _button_20_Text = value;
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

        public KeyboardAlphanumericPortrait()
        {
            InitializeComponent();

            // Set the BindingContext to this (the current page)
            this.BindingContext = this;

            Button_1_Text = ClassEntryMethods.cAlphaNumCharacters.Substring(0, 1);
            Button_2_Text = ClassEntryMethods.cAlphaNumCharacters.Substring(1, 1);
            Button_3_Text = ClassEntryMethods.cAlphaNumCharacters.Substring(2, 1);
            Button_4_Text = ClassEntryMethods.cAlphaNumCharacters.Substring(3, 1);
            Button_5_Text = ClassEntryMethods.cAlphaNumCharacters.Substring(4, 1);
            Button_6_Text = ClassEntryMethods.cAlphaNumCharacters.Substring(5, 1);
            Button_7_Text = ClassEntryMethods.cAlphaNumCharacters.Substring(6, 1);
            Button_8_Text = ClassEntryMethods.cAlphaNumCharacters.Substring(7, 1);
            Button_9_Text = ClassEntryMethods.cAlphaNumCharacters.Substring(8, 1);
            Button_10_Text = ClassEntryMethods.cAlphaNumCharacters.Substring(9, 1);
            Button_11_Text = ClassEntryMethods.cAlphaNumCharacters.Substring(10, 1);
            Button_12_Text = ClassEntryMethods.cAlphaNumCharacters.Substring(11, 1);
            Button_13_Text = ClassEntryMethods.cAlphaNumCharacters.Substring(12, 1);
            Button_14_Text = ClassEntryMethods.cAlphaNumCharacters.Substring(13, 1);
            Button_15_Text = ClassEntryMethods.cAlphaNumCharacters.Substring(14, 1);
            Button_16_Text = ClassEntryMethods.cAlphaNumCharacters.Substring(15, 1);
            Button_17_Text = ClassEntryMethods.cAlphaNumCharacters.Substring(16, 1);
            Button_18_Text = ClassEntryMethods.cAlphaNumCharacters.Substring(17, 1);
            Button_19_Text = ClassEntryMethods.cAlphaNumCharacters.Substring(18, 1);
            Button_20_Text = ClassEntryMethods.cAlphaNumCharacters.Substring(19, 1);

            ButtonDecimalPointText = ClassEntryMethods.cNumDecimalSeparator;
            ButtonMinusText = ClassEntryMethods.cNumNegativeSign;
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
                    "Key_1" => ClassEntryMethods.cAlphaNumCharacters.Substring(0, 1),
                    "Key_2" => ClassEntryMethods.cAlphaNumCharacters.Substring(1, 1),
                    "Key_3" => ClassEntryMethods.cAlphaNumCharacters.Substring(2, 1),
                    "Key_4" => ClassEntryMethods.cAlphaNumCharacters.Substring(3, 1),
                    "Key_5" => ClassEntryMethods.cAlphaNumCharacters.Substring(4, 1),
                    "Key_6" => ClassEntryMethods.cAlphaNumCharacters.Substring(5, 1),
                    "Key_7" => ClassEntryMethods.cAlphaNumCharacters.Substring(6, 1),
                    "Key_8" => ClassEntryMethods.cAlphaNumCharacters.Substring(7, 1),
                    "Key_9" => ClassEntryMethods.cAlphaNumCharacters.Substring(8, 1),
                    "Key_10" => ClassEntryMethods.cAlphaNumCharacters.Substring(9, 1),
                    "Key_11" => ClassEntryMethods.cAlphaNumCharacters.Substring(10, 1),
                    "Key_12" => ClassEntryMethods.cAlphaNumCharacters.Substring(11, 1),
                    "Key_13" => ClassEntryMethods.cAlphaNumCharacters.Substring(12, 1),
                    "Key_14" => ClassEntryMethods.cAlphaNumCharacters.Substring(13, 1),
                    "Key_15" => ClassEntryMethods.cAlphaNumCharacters.Substring(14, 1),
                    "Key_16" => ClassEntryMethods.cAlphaNumCharacters.Substring(15, 1),
                    "Key_17" => ClassEntryMethods.cAlphaNumCharacters.Substring(16, 1),
                    "Key_18" => ClassEntryMethods.cAlphaNumCharacters.Substring(17, 1),
                    "Key_19" => ClassEntryMethods.cAlphaNumCharacters.Substring(18, 1),
                    "Key_20" => ClassEntryMethods.cAlphaNumCharacters.Substring(19, 1),

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
