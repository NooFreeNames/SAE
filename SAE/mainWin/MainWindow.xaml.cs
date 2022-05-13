using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Collections.Specialized;
using mainWin.Pages;

namespace mainWin
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
            DataContext = this;

            /* Setting the window size */
            MainWin.Width = AppConfig.ScreenWidth * AppConfig.WinSizeCoeff;
            MainWin.Height = AppConfig.ScreenHeight * AppConfig.WinSizeCoeff;

            /* Setting the window in the center of the screen */
            MainWin.Left = (AppConfig.ScreenWidth - MainWin.Width) / 2;
            MainWin.Top = (AppConfig.ScreenHeight - MainWin.Height) / 2;

            MainFrame.Content = mainPage;
        }

        private void GoToMainPage(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = mainPage;
        }

        private void GoToAddingCosmicBodyPage(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = addingCosmicBodyPage;
        }

        private void GoToSettingsPage(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = settingsPage;
        }

        private void GoToProfilePage(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = profilePage;
        }
    }
}
