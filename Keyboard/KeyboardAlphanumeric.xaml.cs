namespace Keyboard
{
    public partial class KeyboardAlphanumeric : ContentView
    {
        // Declare variables for shift key and layout change state
        private bool bShiftKeyEnabled;
        private bool bChangeLayoutEnabled;
        private bool bLongPressDetectedWinUI;

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

        // Cancellation token source for WinUI long-press detection
        private CancellationTokenSource? _winUILongPressCts;

        /// <summary>
        /// Set the BindingContext
        /// </summary>

        public KeyboardAlphanumeric()
    	{
    		InitializeComponent();

            // Handle device orientation changes
            UpdateOrientation(DeviceDisplay.MainDisplayInfo.Orientation);

            DeviceDisplay.MainDisplayInfoChanged += (s, e) =>
            {
                UpdateOrientation(e.DisplayInfo.Orientation);
            };

            // Set the BindingContext to this (the current page)
            BindingContext = this;

            InitializeKeyboard();
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
        /// Handle button pressed event on WinUI platform (called first)
        /// Starts a task that triggers Key_LongPressCompleted if the button remains pressed > 600ms.
        /// </summary>
        private void BtnKey_Pressed(object sender, EventArgs e)
        {
            Debug.WriteLine("BtnKey_Pressed called");

            if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                // Cancel any previous pending long-press detection
                try
                {
                    _winUILongPressCts?.Cancel();
                    _winUILongPressCts?.Dispose();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error cancelling previous CTS: {ex.Message}");
                }

                _winUILongPressCts = new CancellationTokenSource();
                CancellationToken token = _winUILongPressCts.Token;

                // Fire-and-forget long-press detection task
                _ = Task.Run(async () =>
                {
                    try
                    {
                        await Task.Delay(600, token).ConfigureAwait(false);

                        if (token.IsCancellationRequested)
                        {
                            return;
                        }

                        // Mark long-press detected and invoke long-press handler on UI thread
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            try
                            {
                                bLongPressDetectedWinUI = true;
                                Key_LongPressCompleted(sender, e);
                            }
                            catch (Exception innerEx)
                            {
                                Debug.WriteLine($"Error in Key_LongPressCompleted invocation: {innerEx.Message}");
                            }
                        });
                    }
                    catch (TaskCanceledException)
                    {
                        // Expected when token is cancelled
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Long press detection error: {ex.Message}");
                    }
                }, token);

                Debug.WriteLine("WinUI long-press detection started");
            }

            bLongPressDetectedWinUI = false;
        }

        /// <summary>
        /// Handle button released event on WinUI platform (called third)
        /// Cancels pending long-press detection and resets state if needed.
        /// </summary>
        private void BtnKey_Released(object sender, EventArgs e)
        {
            // Only care about WinUI long-press cancellation here
            if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                try
                {
                    _winUILongPressCts?.Cancel();
                    _winUILongPressCts?.Dispose();
                    _winUILongPressCts = null;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error cancelling CTS on release: {ex.Message}");
                }

                // If a long-press was detected we suppress the normal click (BtnKey_Clicked checks the flag).
                // Reset the flag shortly after so subsequent presses work normally.
                if (bLongPressDetectedWinUI)
                {
                    _ = Task.Run(async () =>
                    {
                        await Task.Delay(50).ConfigureAwait(false);
                        MainThread.BeginInvokeOnMainThread(() => bLongPressDetectedWinUI = false);
                    });
                }
            }
        }

        /// <summary>
        /// This method is called when a button is clicked, it sends a message with the key pressed to the page (called second)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnKey_Clicked(object sender, EventArgs e)
        {
            Debug.WriteLine("BtnKey_Clicked called");

            if (DeviceInfo.Platform == DevicePlatform.WinUI && bLongPressDetectedWinUI)
            {
                return;
            }

            if (grdCharactersPopup.IsVisible)
            {
                return;
            }

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

            //// !!!BUG!!! On iOS, after a long press, the button text color remains changed

            // Set the original keys (characters in uppercase)
            //SetOriginalKeys();

            //if (sender is Button button && DeviceInfo.Platform == DevicePlatform.iOS)
            //{
            //    string theme = ClassKeyboardMethods.GetTheme();
            //    switch (theme)
            //    {
            //        case "Dark":
            //            if (Application.Current?.Resources != null && Application.Current.Resources.TryGetValue("KeyBoardKeyTextDark", out var darkColor) && darkColor is Color darkColorValue)
            //            {
            //                button.TextColor = darkColorValue;
            //            }
            //            break;
            //        default:
            //            if (Application.Current?.Resources != null && Application.Current.Resources.TryGetValue("KeyBoardKeyTextLight", out var lightColor) && lightColor is Color lightColorValue)
            //            {
            //                button.TextColor = lightColorValue;
            //            }
            //            break;
            //    }
            //}

            // Set the original keys (characters in uppercase)
            //if (sender is Button button2 && DeviceInfo.Platform == DevicePlatform.iOS)
            //{
            //    if (bShiftKeyEnabled)
            //    {
            //        // Convert characters to lowercase
            //        ConvertKeysToLowerCase();
            //    }
            //    else
            //    {
            //        // Set the original keys (characters in uppercase)
            //        SetOriginalKeys();
            //    }
            //}
        }

        /// <summary>
        /// Handles the completion of a long press gesture on a key and displays the alphanumeric keyboard popup
        /// </summary>
        /// <param name="sender">The source object that raised the long press completed event.</param>
        /// <param name="e">The event data associated with the long press completion, containing information about the gesture</param>
        private async void Key_LongPressCompleted(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                // Clear previous popup characters
                SetPopupCharacters([]);

                // Lookup popup characters from the shared dictionary
                if (!ClassKeyboardLayouts.PopupCharacters.TryGetValue(button.Text, out string[]? popupChars))
                {
                    // No popup for other keys
                    return;
                }

                // Set popup characters and show
                SetPopupCharacters(popupChars);

                // Show the popup
                grdCharactersPopup.IsVisible = true;
            }
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
                // Set the original keys (characters in uppercase)
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
                // Select the !#1 layout (special characters)
                ClassKeyboardMethods.SelectAlphanumericKeyboardLayout("OTHER");
                InitializeKeyboard();
                btnChangeLayoutPortrait.Text = "ABC";
                btnChangeLayoutLandscape.Text = "ABC";
                btnShiftKeyPortrait.IsEnabled = false;
                btnShiftKeyLandscape.IsEnabled = false;
            }
            else
            {
                // Select the ABC layout (characters in uppercase)
                ClassKeyboardMethods.SelectAlphanumericKeyboardLayout(ClassKeyboardMethods.cCurrentKeyboardLayout!);
                InitializeKeyboard();
                btnChangeLayoutPortrait.Text = "!#1";
                btnChangeLayoutLandscape.Text = "!#1";
                btnShiftKeyPortrait.IsEnabled = true;
                btnShiftKeyLandscape.IsEnabled = true;
            }

            bShiftKeyEnabled = false;
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

        /// <summary>
        /// Assign keyboard characters from a string array to Button_X_Text properties
        /// Supports up to 53 keyboard entries (0..52). Clears all when passed an empty array
        /// </summary>
        /// <param name="chars">Array of keyboard characters to set</param>
        private void SetKeyboardCharacters(string[] chars)
        {
            var setters = new Action<string>[]
            {
                v => Button_0_Text = v,
                v => Button_1_Text = v,
                v => Button_2_Text = v,
                v => Button_3_Text = v,
                v => Button_4_Text = v,
                v => Button_5_Text = v,
                v => Button_6_Text = v,
                v => Button_7_Text = v,
                v => Button_8_Text = v,
                v => Button_9_Text = v,
                v => Button_10_Text = v,
                v => Button_11_Text = v,
                v => Button_12_Text = v,
                v => Button_13_Text = v,
                v => Button_14_Text = v,
                v => Button_15_Text = v,
                v => Button_16_Text = v,
                v => Button_17_Text = v,
                v => Button_18_Text = v,
                v => Button_19_Text = v,
                v => Button_20_Text = v,
                v => Button_21_Text = v,
                v => Button_22_Text = v,
                v => Button_23_Text = v,
                v => Button_24_Text = v,
                v => Button_25_Text = v,
                v => Button_26_Text = v,
                v => Button_27_Text = v,
                v => Button_28_Text = v,
                v => Button_29_Text = v,
                v => Button_30_Text = v,
                v => Button_31_Text = v,
                v => Button_32_Text = v,
                v => Button_33_Text = v,
                v => Button_34_Text = v,
                v => Button_35_Text = v,
                v => Button_36_Text = v,
                v => Button_37_Text = v,
                v => Button_38_Text = v,
                v => Button_39_Text = v,
                v => Button_40_Text = v,
                v => Button_41_Text = v,
                v => Button_42_Text = v,
                v => Button_43_Text = v,
                v => Button_44_Text = v,
                v => Button_45_Text = v,
                v => Button_46_Text = v,
                v => Button_47_Text = v,
                v => Button_48_Text = v,
                v => Button_49_Text = v,
                v => Button_50_Text = v,
                v => Button_51_Text = v,
                v => Button_52_Text = v,
            };

            // Clear all first
            for (int i = 0; i < setters.Length; i++)
            {
                setters[i](string.Empty);
            }

            if (chars == null)
            {
                return;
            }

            int max = Math.Min(chars.Length, setters.Length);
            for (int i = 0; i < max; i++)
            {
                setters[i](chars[i]);
            }
        }

        /// <summary>
        /// Assign popup characters from a string array to ButtonChar_X_Text properties
        /// Supports up to 18 popup entries (0..17). Clears all when passed an empty array
        /// </summary>
        /// <param name="chars">Array of popup characters to set</param>
        private void SetPopupCharacters(string[] chars)
        {
            Action<string>[] setters =
            [
                v => ButtonChar_0_Text = v,
                v => ButtonChar_1_Text = v,
                v => ButtonChar_2_Text = v,
                v => ButtonChar_3_Text = v,
                v => ButtonChar_4_Text = v,
                v => ButtonChar_5_Text = v,
                v => ButtonChar_6_Text = v,
                v => ButtonChar_7_Text = v,
                v => ButtonChar_8_Text = v,
                v => ButtonChar_9_Text = v,
                v => ButtonChar_10_Text = v,
                v => ButtonChar_11_Text = v,
                v => ButtonChar_12_Text = v,
                v => ButtonChar_13_Text = v,
                v => ButtonChar_14_Text = v,
                v => ButtonChar_15_Text = v,
                v => ButtonChar_16_Text = v,
                v => ButtonChar_17_Text = v,
            ];

            // Clear all first
            for (int i = 0; i < setters.Length; i++)
            {
                setters[i](string.Empty);
            }

            if (chars == null)
            {
                return;
            }

            int max = Math.Min(chars.Length, setters.Length);
            for (int i = 0; i < max; i++)
            {
                setters[i](chars[i]);
            }
        }
    }
}