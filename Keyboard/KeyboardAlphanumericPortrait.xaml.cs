namespace Keyboard
{
    public partial class KeyboardAlphanumericPortrait : ContentView
    {
        // Declare variables for shift key and layout change state
        private bool bShiftKeyEnabled;
        private bool bChangeLayoutEnabled;

        // Declare variables for binding properties
        private string _buttonChar_0_Text = string.Empty;
        private string _buttonChar_1_Text = string.Empty;
        private string _buttonChar_2_Text = string.Empty;
        private string _buttonChar_3_Text = string.Empty;
        private string _buttonChar_4_Text = string.Empty;
        private string _buttonChar_5_Text = string.Empty;
        private string _buttonChar_6_Text = string.Empty;
        private string _buttonChar_7_Text = string.Empty;
        private string _buttonChar_8_Text = string.Empty;
        private string _buttonChar_9_Text = string.Empty;
        private string _buttonChar_10_Text = string.Empty;
        private string _buttonChar_11_Text = string.Empty;
        private string _buttonChar_12_Text = string.Empty;
        private string _buttonChar_13_Text = string.Empty;
        private string _buttonChar_14_Text = string.Empty;
        private string _buttonChar_15_Text = string.Empty;
        private string _buttonChar_16_Text = string.Empty;
        private string _buttonChar_17_Text = string.Empty;

        private string _button_0_Text = string.Empty;
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
        private string _button_21_Text = string.Empty;
        private string _button_22_Text = string.Empty;
        private string _button_23_Text = string.Empty;
        private string _button_24_Text = string.Empty;
        private string _button_25_Text = string.Empty;
        private string _button_26_Text = string.Empty;
        private string _button_27_Text = string.Empty;
        private string _button_28_Text = string.Empty;
        private string _button_29_Text = string.Empty;
        private string _button_30_Text = string.Empty;
        private string _button_31_Text = string.Empty;
        private string _button_32_Text = string.Empty;
        private string _button_33_Text = string.Empty;
        private string _button_34_Text = string.Empty;
        private string _button_35_Text = string.Empty;
        private string _button_36_Text = string.Empty;
        private string _button_37_Text = string.Empty;
        private string _button_38_Text = string.Empty;
        private string _button_39_Text = string.Empty;
        private string _button_40_Text = string.Empty;
        private string _button_41_Text = string.Empty;
        private string _button_42_Text = string.Empty;
        private string _button_43_Text = string.Empty;
        private string _button_44_Text = string.Empty;
        private string _button_45_Text = string.Empty;
        private string _button_46_Text = string.Empty;
        private string _button_47_Text = string.Empty;
        private string _button_48_Text = string.Empty;
        private string _button_49_Text = string.Empty;
        private string _button_50_Text = string.Empty;
        private string _button_51_Text = string.Empty;
        private string _button_52_Text = string.Empty;

        // Properties for the button texts of the keyboard
        public string ButtonChar_0_Text
        {
            get => _buttonChar_0_Text;
            set
            {
                _buttonChar_0_Text = value;
                OnPropertyChanged();
            }
        }

        public string ButtonChar_1_Text
        {
            get => _buttonChar_1_Text;
            set
            {
                _buttonChar_1_Text = value;
                OnPropertyChanged();
            }
        }

        public string ButtonChar_2_Text
        {
            get => _buttonChar_2_Text;
            set
            {
                _buttonChar_2_Text = value;
                OnPropertyChanged();
            }
        }

        public string ButtonChar_3_Text
        {
            get => _buttonChar_3_Text;
            set
            {
                _buttonChar_3_Text = value;
                OnPropertyChanged();
            }
        }

        public string ButtonChar_4_Text
        {
            get => _buttonChar_4_Text;
            set
            {
                _buttonChar_4_Text = value;
                OnPropertyChanged();
            }
        }

        public string ButtonChar_5_Text
        {
            get => _buttonChar_5_Text;
            set
            {
                _buttonChar_5_Text = value;
                OnPropertyChanged();
            }
        }

        public string ButtonChar_6_Text
        {
            get => _buttonChar_6_Text;
            set
            {
                _buttonChar_6_Text = value;
                OnPropertyChanged();
            }
        }

        public string ButtonChar_7_Text
        {
            get => _buttonChar_7_Text;
            set
            {
                _buttonChar_7_Text = value;
                OnPropertyChanged();
            }
        }

        public string ButtonChar_8_Text
        {
            get => _buttonChar_8_Text;
            set
            {
                _buttonChar_8_Text = value;
                OnPropertyChanged();
            }
        }

        public string ButtonChar_9_Text
        {
            get => _buttonChar_9_Text;
            set
            {
                _buttonChar_9_Text = value;
                OnPropertyChanged();
            }
        }

        public string ButtonChar_10_Text
        {
            get => _buttonChar_10_Text;
            set
            {
                _buttonChar_10_Text = value;
                OnPropertyChanged();
            }
        }

        public string ButtonChar_11_Text
        {
            get => _buttonChar_11_Text;
            set
            {
                _buttonChar_11_Text = value;
                OnPropertyChanged();
            }
        }

        public string ButtonChar_12_Text
        {
            get => _buttonChar_12_Text;
            set
            {
                _buttonChar_12_Text = value;
                OnPropertyChanged();
            }
        }

        public string ButtonChar_13_Text
        {
            get => _buttonChar_13_Text;
            set
            {
                _buttonChar_13_Text = value;
                OnPropertyChanged();
            }
        }

        public string ButtonChar_14_Text
        {
            get => _buttonChar_14_Text;
            set
            {
                _buttonChar_14_Text = value;
                OnPropertyChanged();
            }
        }

        public string ButtonChar_15_Text
        {
            get => _buttonChar_15_Text;
            set
            {
                _buttonChar_15_Text = value;
                OnPropertyChanged();
            }
        }

        public string ButtonChar_16_Text
        {
            get => _buttonChar_16_Text;
            set
            {
                _buttonChar_16_Text = value;
                OnPropertyChanged();
            }
        }

        public string ButtonChar_17_Text
        {
            get => _buttonChar_17_Text;
            set
            {
                _buttonChar_17_Text = value;
                OnPropertyChanged();
            }
        }

        public string Button_0_Text
        {
            get => _button_0_Text;
            set
            {
                _button_0_Text = value;
                OnPropertyChanged();
            }
        }

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

        public string Button_21_Text
        {
            get => _button_21_Text;
            set
            {
                _button_21_Text = value;
                OnPropertyChanged();
            }
        }

        public string Button_22_Text
        {
            get => _button_22_Text;
            set
            {
                _button_22_Text = value;
                OnPropertyChanged();
            }
        }

        public string Button_23_Text
        {
            get => _button_23_Text;
            set
            {
                _button_23_Text = value;
                OnPropertyChanged();
            }
        }

        public string Button_24_Text
        {
            get => _button_24_Text;
            set
            {
                _button_24_Text = value;
                OnPropertyChanged();
            }
        }

        public string Button_25_Text
        {
            get => _button_25_Text;
            set
            {
                _button_25_Text = value;
                OnPropertyChanged();
            }
        }

        public string Button_26_Text
        {
            get => _button_26_Text;
            set
            {
                _button_26_Text = value;
                OnPropertyChanged();
            }
        }

        public string Button_27_Text
        {
            get => _button_27_Text;
            set
            {
                _button_27_Text = value;
                OnPropertyChanged();
            }
        }

        public string Button_28_Text
        {
            get => _button_28_Text;
            set
            {
                _button_28_Text = value;
                OnPropertyChanged();
            }
        }

        public string Button_29_Text
        {
            get => _button_29_Text;
            set
            {
                _button_29_Text = value;
                OnPropertyChanged();
            }
        }

        public string Button_30_Text
        {
            get => _button_30_Text;
            set
            {
                _button_30_Text = value;
                OnPropertyChanged();
            }
        }

        public string Button_31_Text
        {
            get => _button_31_Text;
            set
            {
                _button_31_Text = value;
                OnPropertyChanged();
            }
        }

        public string Button_32_Text
        {
            get => _button_32_Text;
            set
            {
                _button_32_Text = value;
                OnPropertyChanged();
            }
        }

        public string Button_33_Text
        {
            get => _button_33_Text;
            set
            {
                _button_33_Text = value;
                OnPropertyChanged();
            }
        }

        public string Button_34_Text
        {
            get => _button_34_Text;
            set
            {
                _button_34_Text = value;
                OnPropertyChanged();
            }
        }

        public string Button_35_Text
        {
            get => _button_35_Text;
            set
            {
                _button_35_Text = value;
                OnPropertyChanged();
            }
        }

        public string Button_36_Text
        {
            get => _button_36_Text;
            set
            {
                _button_36_Text = value;
                OnPropertyChanged();
            }
        }

        public string Button_37_Text
        {
            get => _button_37_Text;
            set
            {
                _button_37_Text = value;
                OnPropertyChanged();
            }
        }

        public string Button_38_Text
        {
            get => _button_38_Text;
            set
            {
                _button_38_Text = value;
                OnPropertyChanged();
            }
        }

        public string Button_39_Text
        {
            get => _button_39_Text;
            set
            {
                _button_39_Text = value;
                OnPropertyChanged();
            }
        }

        public string Button_40_Text
        {
            get => _button_40_Text;
            set
            {
                _button_40_Text = value;
                OnPropertyChanged();
            }
        }

        public string Button_41_Text
        {
            get => _button_41_Text;
            set
            {
                _button_41_Text = value;
                OnPropertyChanged();
            }
        }

        public string Button_42_Text
        {
            get => _button_42_Text;
            set
            {
                _button_42_Text = value;
                OnPropertyChanged();
            }
        }

        public string Button_43_Text
        {
            get => _button_43_Text;
            set
            {
                _button_43_Text = value;
                OnPropertyChanged();
            }
        }

        public string Button_44_Text
        {
            get => _button_44_Text;
            set
            {
                _button_44_Text = value;
                OnPropertyChanged();
            }
        }

        public string Button_45_Text
        {
            get => _button_45_Text;
            set
            {
                _button_45_Text = value;
                OnPropertyChanged();
            }
        }

        public string Button_46_Text
        {
            get => _button_46_Text;
            set
            {
                _button_46_Text = value;
                OnPropertyChanged();
            }
        }

        public string Button_47_Text
        {
            get => _button_47_Text;
            set
            {
                _button_47_Text = value;
                OnPropertyChanged();
            }
        }

        public string Button_48_Text
        {
            get => _button_48_Text;
            set
            {
                _button_48_Text = value;
                OnPropertyChanged();
            }
        }

        public string Button_49_Text
        {
            get => _button_49_Text;
            set
            {
                _button_49_Text = value;
                OnPropertyChanged();
            }
        }

        public string Button_50_Text
        {
            get => _button_50_Text;
            set
            {
                _button_50_Text = value;
                OnPropertyChanged();
            }
        }

        public string Button_51_Text
        {
            get => _button_51_Text;
            set
            {
                _button_51_Text = value;
                OnPropertyChanged();
            }
        }

        public string Button_52_Text
        {
            get => _button_52_Text;
            set
            {
                _button_52_Text = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Set the BindingContext
        /// </summary>
        public KeyboardAlphanumericPortrait()
        {
            InitializeComponent();

            // Set the BindingContext to this (the current page)
            BindingContext = this;

            InitializeKeyboard();
        }

        /// <summary>
        /// Initializes the keyboard by assigning character values to each button
        /// Using line by line is a bit faster than using a loop
        /// </summary>
        private void InitializeKeyboard()
        {
            // Start the stopwatch
            //long startTime = Stopwatch.GetTimestamp();

            Button_0_Text = ClassKeyboardMethods.cAlphaNumCharacters[0];
            Button_1_Text = ClassKeyboardMethods.cAlphaNumCharacters[1];
            Button_2_Text = ClassKeyboardMethods.cAlphaNumCharacters[2];
            Button_3_Text = ClassKeyboardMethods.cAlphaNumCharacters[3];
            Button_4_Text = ClassKeyboardMethods.cAlphaNumCharacters[4];
            Button_5_Text = ClassKeyboardMethods.cAlphaNumCharacters[5];
            Button_6_Text = ClassKeyboardMethods.cAlphaNumCharacters[6];
            Button_7_Text = ClassKeyboardMethods.cAlphaNumCharacters[7];
            Button_8_Text = ClassKeyboardMethods.cAlphaNumCharacters[8];
            Button_9_Text = ClassKeyboardMethods.cAlphaNumCharacters[9];

            // Set the original keys 10-39, row 2-3-4 of the keyboard (characters in uppercase)
            SetOriginalKeys();

            Button_40_Text = ClassKeyboardMethods.cAlphaNumCharacters[40];
            Button_41_Text = ClassKeyboardMethods.cAlphaNumCharacters[41];
            Button_42_Text = ClassKeyboardMethods.cAlphaNumCharacters[42];
            Button_43_Text = ClassKeyboardMethods.cAlphaNumCharacters[43];
            Button_44_Text = ClassKeyboardMethods.cAlphaNumCharacters[44];
            Button_45_Text = ClassKeyboardMethods.cAlphaNumCharacters[45];
            Button_46_Text = ClassKeyboardMethods.cAlphaNumCharacters[46];
            Button_47_Text = ClassKeyboardMethods.cAlphaNumCharacters[47];
            Button_48_Text = ClassKeyboardMethods.cAlphaNumCharacters[48];
            Button_49_Text = ClassKeyboardMethods.cAlphaNumCharacters[49];
            Button_50_Text = ClassKeyboardMethods.cAlphaNumCharacters[50];
            Button_51_Text = ClassKeyboardMethods.cAlphaNumCharacters[51];
            Button_52_Text = ClassKeyboardMethods.cAlphaNumCharacters[52];

            //// Stop the stopwatch
            //TimeSpan delta = Stopwatch.GetElapsedTime(startTime);
            //_ = Application.Current!.Windows[0].Page!.DisplayAlertAsync("InitializeKeyboard", $"Time elapsed (hh:mm:ss.xxxxxxx): {delta}", "OK");
        }

        /// <summary>
        /// Handles the completion of a long press gesture on a key and displays the alphanumeric keyboard popup.
        /// </summary>
        /// <remarks>Opening the popup closes the keyboard overlay, so this can not be used</remarks>
        /// <param name="sender">The source object that raised the long press completed event.</param>
        /// <param name="e">The event data associated with the long press completion, containing information about the gesture.</param>
        private async void TouchBehavior_LongPressCompleted(object sender, CommunityToolkit.Maui.Core.LongPressCompletedEventArgs e)
        {
            Debug.WriteLine("TouchBehavior_LongPressCompleted - Long press completed on key");

            if (sender is Button button)
            {
                // Clear previous popup characters
                ButtonChar_0_Text = string.Empty;
                ButtonChar_1_Text = string.Empty;
                ButtonChar_2_Text = string.Empty;
                ButtonChar_3_Text = string.Empty;
                ButtonChar_4_Text = string.Empty;
                ButtonChar_5_Text = string.Empty;
                ButtonChar_6_Text = string.Empty;
                ButtonChar_7_Text = string.Empty;
                ButtonChar_8_Text = string.Empty;
                ButtonChar_9_Text = string.Empty;
                ButtonChar_10_Text = string.Empty;
                ButtonChar_11_Text = string.Empty;
                ButtonChar_12_Text = string.Empty;
                ButtonChar_13_Text = string.Empty;
                ButtonChar_14_Text = string.Empty;
                ButtonChar_15_Text = string.Empty;
                ButtonChar_16_Text = string.Empty;
                ButtonChar_17_Text = string.Empty;

                // Set popup characters based on the key pressed
                switch (button.Text)
                {
                    case "A":
                        ButtonChar_0_Text = "Á";
                        ButtonChar_1_Text = "À";
                        ButtonChar_2_Text = "Â";
                        ButtonChar_3_Text = "Ä";
                        ButtonChar_4_Text = "Ã";
                        ButtonChar_5_Text = "Å";
                        break;
                    case "B":
                        ButtonChar_0_Text = "B́";
                        ButtonChar_1_Text = "B̀";
                        break;
                    case "C":
                        ButtonChar_0_Text = "Ć";
                        ButtonChar_1_Text = "C̀";
                        ButtonChar_2_Text = "Ç";
                        break;
                    case "D":
                        ButtonChar_0_Text = "D́";
                        ButtonChar_1_Text = "D̀";
                        break;
                    case "E":
                        ButtonChar_0_Text = "É";
                        ButtonChar_1_Text = "È";
                        ButtonChar_2_Text = "Ê";
                        ButtonChar_3_Text = "Ë";
                        ButtonChar_4_Text = "Ē";
                        break;
                    case "F":
                        ButtonChar_0_Text = "F́";
                        ButtonChar_1_Text = "F̀";
                        break;
                    case "G":
                        ButtonChar_0_Text = "Ǵ";
                        ButtonChar_1_Text = "G̀";
                        break;
                    case "H":
                        ButtonChar_0_Text = "H́";
                        ButtonChar_1_Text = "H̀";
                        break;
                    case "I":
                        ButtonChar_0_Text = "Í";
                        ButtonChar_1_Text = "Ì";
                        ButtonChar_2_Text = "Î";
                        ButtonChar_3_Text = "Ï";
                        ButtonChar_4_Text = "Ī";
                        break;
                    case "J":
                        ButtonChar_0_Text = "J́";
                        ButtonChar_1_Text = "J̀";
                        break;
                    case "K":
                        ButtonChar_0_Text = "Ḱ";
                        ButtonChar_1_Text = "K̀";
                        break;
                    case "L":
                        ButtonChar_0_Text = "Ĺ";
                        ButtonChar_1_Text = "L̀";
                        break;
                    case "M":
                        ButtonChar_0_Text = "Ḿ";
                        ButtonChar_1_Text = "M̀";
                        break;
                    case "N":
                        ButtonChar_0_Text = "Ñ";
                        ButtonChar_1_Text = "Ń";
                        ButtonChar_2_Text = "Ǹ";
                        ButtonChar_3_Text = "Ń";
                        break;
                    case "O":
                        ButtonChar_0_Text = "Ó";
                        ButtonChar_1_Text = "Ò";
                        ButtonChar_2_Text = "Ô";
                        ButtonChar_3_Text = "Ö";
                        ButtonChar_4_Text = "Õ";
                        ButtonChar_5_Text = "Ø";
                        break;
                    case "P":
                        ButtonChar_0_Text = "Ṕ";
                        ButtonChar_1_Text = "P̀";
                        break;
                    case "Q":
                        ButtonChar_0_Text = "Q́";
                        ButtonChar_1_Text = "Q̀";
                        break;
                    case "R":
                        ButtonChar_0_Text = "Ŕ";
                        ButtonChar_1_Text = "R̀";
                        break;
                    case "S":
                        ButtonChar_0_Text = "Ś";
                        ButtonChar_1_Text = "Š";
                        break;
                    case "T":
                        ButtonChar_0_Text = "T́";
                        ButtonChar_1_Text = "T̀";
                        break;
                    case "U":
                        ButtonChar_0_Text = "Ú";
                        ButtonChar_1_Text = "Ù";
                        ButtonChar_2_Text = "Û";
                        ButtonChar_3_Text = "Ü";
                        ButtonChar_4_Text = "Ū";
                        break;
                    case "V":
                        ButtonChar_0_Text = "V́";
                        ButtonChar_1_Text = "V̀";
                        break;
                    case "W":
                        ButtonChar_0_Text = "Ẃ";
                        ButtonChar_1_Text = "Ẁ";
                        break;
                    case "X":
                        ButtonChar_0_Text = "X́";
                        ButtonChar_1_Text = "X̀";
                        break;
                    case "Y":
                        ButtonChar_0_Text = "Ý";
                        ButtonChar_1_Text = "Ỳ";
                        ButtonChar_2_Text = "Ŷ";
                        break;
                    case "Z":
                        ButtonChar_0_Text = "Ź";
                        ButtonChar_1_Text = "Ž";
                        ButtonChar_2_Text = "Ż";
                        break;
                    
                    case "a":
                        ButtonChar_0_Text = "á";
                        ButtonChar_1_Text = "à";
                        ButtonChar_2_Text = "â";
                        ButtonChar_3_Text = "ä";
                        ButtonChar_4_Text = "ã";
                        ButtonChar_5_Text = "å";
                        break;
                    case "b":
                        ButtonChar_0_Text = "b́";
                        ButtonChar_1_Text = "b̀";
                        ButtonChar_2_Text = "β";
                        break;
                    case "c":
                        ButtonChar_0_Text = "ć";
                        ButtonChar_1_Text = "c̀";
                        ButtonChar_2_Text = "ç";
                        break;
                    case "d":
                        ButtonChar_0_Text = "d́";
                        ButtonChar_1_Text = "d̀";
                        ButtonChar_2_Text = "δ";
                        break;
                    case "e":
                        ButtonChar_0_Text = "é";
                        ButtonChar_1_Text = "è";
                        ButtonChar_2_Text = "ê";
                        ButtonChar_3_Text = "ë";
                        ButtonChar_4_Text = "ē";
                        break;
                    case "f":
                        ButtonChar_0_Text = "f́";
                        ButtonChar_1_Text = "f̀";
                        ButtonChar_2_Text = "ƒ";
                        break;
                    case "g":
                        ButtonChar_0_Text = "ǵ";
                        ButtonChar_1_Text = "g̀";
                        ButtonChar_2_Text = "ğ";
                        break;
                    case "h":
                        ButtonChar_0_Text = "h́";
                        ButtonChar_1_Text = "h̀";
                        ButtonChar_2_Text = "ħ";
                        break;
                    case "i":
                        ButtonChar_0_Text = "í";
                        ButtonChar_1_Text = "ì";
                        ButtonChar_2_Text = "î";
                        ButtonChar_3_Text = "ï";
                        ButtonChar_4_Text = "ī";
                        break;
                    case "j":
                        ButtonChar_0_Text = "j́";
                        ButtonChar_1_Text = "j̀";
                        ButtonChar_2_Text = "ĵ";
                        break;
                    case "k":
                        ButtonChar_0_Text = "ḱ";
                        ButtonChar_1_Text = "k̀";
                        ButtonChar_2_Text = "ķ";
                        break;
                    case "l":
                        ButtonChar_0_Text = "ĺ";
                        ButtonChar_1_Text = "l̀";
                        ButtonChar_2_Text = "ł";
                        break;
                    case "m":
                        ButtonChar_0_Text = "ḿ";
                        ButtonChar_1_Text = "m̀";
                        ButtonChar_2_Text = "μ";
                        break;
                    case "n":
                        ButtonChar_0_Text = "ñ";
                        ButtonChar_1_Text = "ń";
                        ButtonChar_2_Text = "ǹ";
                        ButtonChar_3_Text = "ń";
                        break;
                    case "o":
                        ButtonChar_0_Text = "ó";
                        ButtonChar_1_Text = "ò";
                        ButtonChar_2_Text = "ô";
                        ButtonChar_3_Text = "ö";
                        ButtonChar_4_Text = "õ";
                        ButtonChar_5_Text = "ø";
                        break;
                    case "p":
                        ButtonChar_0_Text = "ṕ";
                        ButtonChar_1_Text = "p̀";
                        ButtonChar_2_Text = "π";
                        break;
                    case "q":
                        ButtonChar_0_Text = "q́";
                        ButtonChar_1_Text = "q̀";
                        ButtonChar_2_Text = "ɋ";
                        break;
                    case "r":
                        ButtonChar_0_Text = "ŕ";
                        ButtonChar_1_Text = "r̀";
                        ButtonChar_2_Text = "ř";
                        break;
                    case "s":
                        ButtonChar_0_Text = "ś";
                        ButtonChar_1_Text = "š";
                        ButtonChar_2_Text = "ß";
                        break;
                    case "t":
                        ButtonChar_0_Text = "t́";
                        ButtonChar_1_Text = "t̀";
                        ButtonChar_2_Text = "ţ";
                        break;
                    case "u":
                        ButtonChar_0_Text = "ú";
                        ButtonChar_1_Text = "ù";
                        ButtonChar_2_Text = "û";
                        ButtonChar_3_Text = "ü";
                        ButtonChar_4_Text = "ū";
                        break;
                    case "v":
                        ButtonChar_0_Text = "v́";
                        ButtonChar_1_Text = "v̀";
                        ButtonChar_2_Text = "ν";
                        break;
                    case "w":
                        ButtonChar_0_Text = "ẃ";
                        ButtonChar_1_Text = "ẁ";
                        ButtonChar_2_Text = "ŵ";
                        break;
                    case "x":
                        ButtonChar_0_Text = "x́";
                        ButtonChar_1_Text = "x̀";
                        ButtonChar_2_Text = "χ";
                        break;
                    case "y":
                        ButtonChar_0_Text = "ý";
                        ButtonChar_1_Text = "ỳ";
                        ButtonChar_2_Text = "ŷ";
                        break;
                    case "z":
                        ButtonChar_0_Text = "ź";
                        ButtonChar_1_Text = "ž";
                        ButtonChar_2_Text = "ż";
                        break;
                    default:
                        // No popup for other keys
                        return;
                }
            }

            // Show the popup
            grdCharactersPopup.IsVisible = true;
        }

        /// <summary>
        /// Handles the click event for a character button and sends the button's text as a message to the page.
        /// </summary>
        /// <remarks>This method uses <see cref="WeakReferenceMessenger"/> to broadcast the button's text.
        /// Only events triggered by a <see cref="Button"/> are processed; other sender types are ignored.</remarks>
        /// <param name="sender">The source of the event, expected to be a <see cref="Button"/> representing the character button that was
        /// clicked.</param>
        /// <param name="e">An <see cref="EventArgs"/> instance containing the event data.</param>
        private void BtnCharacter_Clicked(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                // Send the message with the key pressed to the page
                try
                {
                    WeakReferenceMessenger.Default.Send(new StringMessage(button.Text));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error sending message: {ex.Message}");
                }

                grdCharactersPopup.IsVisible = false;
            }
        }

        /// <summary>
        /// This method is called when a button is clicked, it sends a message with the key pressed to the page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnKey_Clicked(object sender, EventArgs e)
        {
            string cKeyPressed = string.Empty;

            if (sender is Button button)
            {
                cKeyPressed = button.Text;
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
        /// BtnShift_Clicked event handler to toggle the shift key state
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnShiftKey_Clicked(object sender, EventArgs e)
        {
            bShiftKeyEnabled = !bShiftKeyEnabled;

            if (bShiftKeyEnabled)
            {
                // Convert characters to lowercase
                ConvertKeysToLowerCase();
            }
            else
            {
                // set the original keys (characters in uppercase)
                SetOriginalKeys();
            }
        }

        /// <summary>
        /// BtnChangeLayout_Clicked event handler to toggle the keyboard layout state
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnChangeLayout_Clicked(object sender, EventArgs e)
        {
            bChangeLayoutEnabled = !bChangeLayoutEnabled;

            if (bChangeLayoutEnabled)
            {
                // Change to !#1 layout (characters in uppercase)
                ClassKeyboardMethods.SelectAlphanumericKeyboardLayout("OTHER");
                InitializeKeyboard();
                btnChangeLayout.Text = "ABC";
                btnShiftKey.IsEnabled = false;
            }
            else
            {
                // Change to ABC layout (special characters)
                ClassKeyboardMethods.SelectAlphanumericKeyboardLayout(ClassKeyboardMethods.cCurrentKeyboardLayout);
                InitializeKeyboard();
                btnChangeLayout.Text = "!#1";
                btnShiftKey.IsEnabled = true;
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

        /// <summary>
        /// Handles the click event to hide the keyboard character popup.
        /// </summary>
        /// <param name="sender">The source of the event, typically the control that was clicked.</param>
        /// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
        private void OnKeyboardCharHide_Clicked(object sender, EventArgs e)
        {
            grdCharactersPopup.IsVisible = false;
        }

        /// <summary>
        /// Set the original keys, row 2-3-4 of the keyboard (characters in uppercase)
        /// </summary>
        private void SetOriginalKeys()
        {
            Button_10_Text = ClassKeyboardMethods.cAlphaNumCharacters[10];
            Button_11_Text = ClassKeyboardMethods.cAlphaNumCharacters[11];
            Button_12_Text = ClassKeyboardMethods.cAlphaNumCharacters[12];
            Button_13_Text = ClassKeyboardMethods.cAlphaNumCharacters[13];
            Button_14_Text = ClassKeyboardMethods.cAlphaNumCharacters[14];
            Button_15_Text = ClassKeyboardMethods.cAlphaNumCharacters[15];
            Button_16_Text = ClassKeyboardMethods.cAlphaNumCharacters[16];
            Button_17_Text = ClassKeyboardMethods.cAlphaNumCharacters[17];
            Button_18_Text = ClassKeyboardMethods.cAlphaNumCharacters[18];
            Button_19_Text = ClassKeyboardMethods.cAlphaNumCharacters[19];
            Button_20_Text = ClassKeyboardMethods.cAlphaNumCharacters[20];
            Button_21_Text = ClassKeyboardMethods.cAlphaNumCharacters[21];
            Button_22_Text = ClassKeyboardMethods.cAlphaNumCharacters[22];
            Button_23_Text = ClassKeyboardMethods.cAlphaNumCharacters[23];
            Button_24_Text = ClassKeyboardMethods.cAlphaNumCharacters[24];
            Button_25_Text = ClassKeyboardMethods.cAlphaNumCharacters[25];
            Button_26_Text = ClassKeyboardMethods.cAlphaNumCharacters[26];
            Button_27_Text = ClassKeyboardMethods.cAlphaNumCharacters[27];
            Button_28_Text = ClassKeyboardMethods.cAlphaNumCharacters[28];
            Button_29_Text = ClassKeyboardMethods.cAlphaNumCharacters[29];
            Button_30_Text = ClassKeyboardMethods.cAlphaNumCharacters[30];
            Button_31_Text = ClassKeyboardMethods.cAlphaNumCharacters[31];
            Button_32_Text = ClassKeyboardMethods.cAlphaNumCharacters[32];
            Button_33_Text = ClassKeyboardMethods.cAlphaNumCharacters[33];
            Button_34_Text = ClassKeyboardMethods.cAlphaNumCharacters[34];
            Button_35_Text = ClassKeyboardMethods.cAlphaNumCharacters[35];
            Button_36_Text = ClassKeyboardMethods.cAlphaNumCharacters[36];
            Button_37_Text = ClassKeyboardMethods.cAlphaNumCharacters[37];
            Button_38_Text = ClassKeyboardMethods.cAlphaNumCharacters[38];
            Button_39_Text = ClassKeyboardMethods.cAlphaNumCharacters[39];
        }

        /// <summary>
        /// Convert characters keys to lowercase, row 2-3-4 of the keyboard
        /// </summary>
        private void ConvertKeysToLowerCase()
        {
            Button_10_Text = ClassKeyboardMethods.cAlphaNumCharacters[10].ToLower();
            Button_11_Text = ClassKeyboardMethods.cAlphaNumCharacters[11].ToLower();
            Button_12_Text = ClassKeyboardMethods.cAlphaNumCharacters[12].ToLower();
            Button_13_Text = ClassKeyboardMethods.cAlphaNumCharacters[13].ToLower();
            Button_14_Text = ClassKeyboardMethods.cAlphaNumCharacters[14].ToLower();
            Button_15_Text = ClassKeyboardMethods.cAlphaNumCharacters[15].ToLower();
            Button_16_Text = ClassKeyboardMethods.cAlphaNumCharacters[16].ToLower();
            Button_17_Text = ClassKeyboardMethods.cAlphaNumCharacters[17].ToLower();
            Button_18_Text = ClassKeyboardMethods.cAlphaNumCharacters[18].ToLower();
            Button_19_Text = ClassKeyboardMethods.cAlphaNumCharacters[19].ToLower();
            Button_20_Text = ClassKeyboardMethods.cAlphaNumCharacters[20].ToLower();
            Button_21_Text = ClassKeyboardMethods.cAlphaNumCharacters[21].ToLower();
            Button_22_Text = ClassKeyboardMethods.cAlphaNumCharacters[22].ToLower();
            Button_23_Text = ClassKeyboardMethods.cAlphaNumCharacters[23].ToLower();
            Button_24_Text = ClassKeyboardMethods.cAlphaNumCharacters[24].ToLower();
            Button_25_Text = ClassKeyboardMethods.cAlphaNumCharacters[25].ToLower();
            Button_26_Text = ClassKeyboardMethods.cAlphaNumCharacters[26].ToLower();
            Button_27_Text = ClassKeyboardMethods.cAlphaNumCharacters[27].ToLower();
            Button_28_Text = ClassKeyboardMethods.cAlphaNumCharacters[28].ToLower();
            Button_29_Text = ClassKeyboardMethods.cAlphaNumCharacters[29].ToLower();
            Button_30_Text = ClassKeyboardMethods.cAlphaNumCharacters[30].ToLower();
            Button_31_Text = ClassKeyboardMethods.cAlphaNumCharacters[31].ToLower();
            Button_32_Text = ClassKeyboardMethods.cAlphaNumCharacters[32].ToLower();
            Button_33_Text = ClassKeyboardMethods.cAlphaNumCharacters[33].ToLower();
            Button_34_Text = ClassKeyboardMethods.cAlphaNumCharacters[34].ToLower();
            Button_35_Text = ClassKeyboardMethods.cAlphaNumCharacters[35].ToLower();
            Button_36_Text = ClassKeyboardMethods.cAlphaNumCharacters[36].ToLower();
            Button_37_Text = ClassKeyboardMethods.cAlphaNumCharacters[37].ToLower();
            Button_38_Text = ClassKeyboardMethods.cAlphaNumCharacters[38].ToLower();
            Button_39_Text = ClassKeyboardMethods.cAlphaNumCharacters[39].ToLower();
        }
    }
}
