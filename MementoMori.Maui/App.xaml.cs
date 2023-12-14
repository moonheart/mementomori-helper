using MementoMori.Common;

namespace MementoMori.Maui
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            Window window = base.CreateWindow(activationState);

            window.Created += async (s, e) =>
            {
                await Services.Get<InitializeWorker>().StartAsync(CancellationToken.None);
            };

            return window;
        }
    }
}
