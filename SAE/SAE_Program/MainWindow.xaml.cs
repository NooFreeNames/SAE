using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

using SAE_Program.Pages;
using SAE_Program.Properties;


namespace SAE_Program
{
    public partial class MainWindow : Window
    {
        /* Pages */
        public MainPage mainPage = new();
        public AddingCosmicBodyPage addingCosmicBodyPage = new();
        public SettingsPage settingsPage = new();
        public ProfilePage profilePage = new();

        public MainWindow()
        {
            InitializeComponent();
            App.Current.MainWindow = this;
            var position = MainWinSettings.Default.Position;
            if (position.IsEmpty || !MainWinSettings.Default.RememberPosition)
            {
                var screenWidth = SystemParameters.PrimaryScreenWidth;
                var screenHeight = SystemParameters.PrimaryScreenHeight;

                /* Setting the window size */
                Width = screenWidth * MainWinSettings.Default.SizeCoeff;
                Height = screenHeight * MainWinSettings.Default.SizeCoeff;

                /* Setting the window in the center of the screen */
                Left = (screenWidth - MainWin.Width) / 2;
                Top = (screenHeight - MainWin.Height) / 2;
            } 
            else
            {
                Width = position.Width;
                Height = position.Height;
                Left = position.Left;
                Top = position.Top;
            }

            MainFrame.Content = mainPage;
        }

        private void GoToPage(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = ((Control)sender).Name switch
            {
                "AddingCosmicBodyPageBtn" => addingCosmicBodyPage,
                "SettingsPageBtn" => settingsPage,
                "ProfilePageBtn" => profilePage,
                _ => mainPage,
            };
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            if (MainWinSettings.Default.RememberPosition)
            {
                MainWinSettings.Default.Position = RestoreBounds;
            }
            
            MainWinSettings.Default.Save();
        }
    }
}
