namespace Keyboard;

public partial class NewPage2 : ContentPage
{
	public NewPage2()
	{
		InitializeComponent();

        // To open the bottom sheet
        MyBottomSheet.IsOpen = true;

        // To close the bottom sheet
        //MyBottomSheet.IsOpen = false;
    }

    //private void BtnKey_Clicked(object sender, EventArgs e)
    //{
    
    //}
}