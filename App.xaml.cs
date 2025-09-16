namespace MediaElement.Crash.Demo
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            // Removing Shell navigation exposes the exception
            //return new Window(new MainPage());

            // Wrapping MainPage with NavigationPage allows us to catch exception
            return new Window(new NavigationPage(new MainPage()));
        }
    }
}