using CommunityToolkit.Mvvm.Messaging;
using System.Diagnostics;

namespace Keyboard
{
    public partial class KeyboardDecimalLandscape : ContentView
    {
        // Declare variables
        private string _buttonZeroText = string.Empty;
        private string _buttonOneText = string.Empty;
        private string _buttonTwoText = string.Empty;
        private string _buttonThreeText = string.Empty;
        private string _buttonFourText = string.Empty;
        private string _buttonFiveText = string.Empty;
        private string _buttonSixText = string.Empty;
        private string _buttonSevenText = string.Empty;
        private string _buttonEightText = string.Empty;
        private string _buttonNineText = string.Empty;
        private string _buttonDecimalPointText = string.Empty;
        private string _buttonMinusText = string.Empty;

        // Properties for the button texts of the keyboard
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

        public KeyboardDecimalLandscape()
    	{
    		InitializeComponent();

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
    }
}