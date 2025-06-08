namespace Keyboard
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Window dimensions and location for desktop apps
        /// </summary>
        /// <param name="activationState"></param>
        /// <returns></returns>
        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell())
            //return new Window(new NavigationPage(new MainPage()))
            {
                X = 300,
                Y = 40,
                Height = 600,
                Width = 800,
                MinimumHeight = 600,
                MinimumWidth = 800,
                MaximumHeight = 1100,
                MaximumWidth = 1000
            };
        }
    }
}