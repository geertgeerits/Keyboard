namespace Keyboard;

public partial class NewPage1 : ContentPage
{
	public NewPage1()
	{
		InitializeComponent();

        // To open the bottom sheet
        //MyBottomSheet.IsOpen = true;
        KeyboardDecimal.IsOpen = true;

        // To close the bottom sheet
        //MyBottomSheet.IsOpen = false;
    }

    //private void BtnKey_Clicked(object sender, EventArgs e)
    //{

    //}

}