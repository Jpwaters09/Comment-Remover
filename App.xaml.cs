using Microsoft.UI.Xaml;
using Windows.Storage;

namespace Comment_Remover
{
    public partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();

            object themeSetting = ApplicationData.Current.LocalSettings.Values["themeSetting"];

            if (themeSetting != null)
            {
                if ((int)themeSetting == 1)
                {
                    App.Current.RequestedTheme = (ApplicationTheme)0;
                }

                if ((int)themeSetting == 2)
                {
                    App.Current.RequestedTheme = (ApplicationTheme)1;
                }
            }

            else
            {
                ApplicationData.Current.LocalSettings.Values["themeSetting"] = 0;
            }
        }

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            m_window = new MainWindow();
            m_window.Activate();
        }

        private Window m_window;
    }
}
