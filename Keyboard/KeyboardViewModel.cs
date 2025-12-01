//using System.ComponentModel;
//using System.Runtime.CompilerServices;
//using System.Windows.Input;
//using CommunityToolkit.Mvvm.Input;

//namespace Keyboard
//{
//    public partial class KeyboardViewModel : INotifyPropertyChanged
//    {
//        private string _inputText = string.Empty;

//        public string InputText
//        {
//            get => _inputText;
//            set
//            {
//                if (_inputText != value)
//                {
//                    _inputText = value;
//                    OnPropertyChanged();
//                }
//            }
//        }

//        public ICommand KeyPressCommand { get; }

//        public KeyboardViewModel()
//        {
//            KeyPressCommand = new RelayCommand<string>(OnKeyPress);
//        }

//        private void OnKeyPress(string key)
//        {
//            // Ensure property updates happen on the UI thread so the Entry can refresh.
//            Microsoft.Maui.ApplicationModel.MainThread.BeginInvokeOnMainThread(() =>
//            {
//                if (key == "Backspace")
//                {
//                    if (InputText.Length > 0)
//                        InputText = InputText[..^1];
//                }
//                else
//                {
//                    InputText += key;
//                }
//            });

//            Debug.WriteLine($"OnKeyPress: Key pressed: {key} - {InputText}");
//        }

//        public event PropertyChangedEventHandler? PropertyChanged;
//        private void OnPropertyChanged([CallerMemberName] string? name = null) =>
//            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

//    }
//}
