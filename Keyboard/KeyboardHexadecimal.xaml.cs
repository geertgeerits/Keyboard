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
    }
}