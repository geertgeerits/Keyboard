using System.ComponentModel;
using System.Runtime.CompilerServices;

public class ViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    // Example property
    private string _buttonOneText = "1";
    public string ButtonOneText
    {
        get => _buttonOneText;
        set
        {
            if (_buttonOneText != value)
            {
                _buttonOneText = value;
                OnPropertyChanged();
            }
        }
    }
}
