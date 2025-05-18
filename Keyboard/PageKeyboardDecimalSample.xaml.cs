namespace Keyboard;

public partial class PageKeyboardDecimalSample : ContentPage
{
	public PageKeyboardDecimalSample()
	{
		InitializeComponent();

        // To open the bottom sheet
        CustomKeyboardDecimal.IsOpen = true;

        // To close the bottom sheet
        //CustomKeyboardDecimal.IsOpen = false;

    }
}