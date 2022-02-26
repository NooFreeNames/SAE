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
        NameValueCollection Config => ConfigurationManager.AppSettings;

        public MainWindow()
        {
            InitializeComponent();
            
            /* Setting the window size */
            double screenW = SystemParameters.PrimaryScreenWidth;
            double screenH = SystemParameters.PrimaryScreenHeight;
            if (!float.TryParse(Config.Get("WinSizeCoeff"), out float winSizeCoeff)) { winSizeCoeff = 0.5f; }
            MainWin.Width = screenW * winSizeCoeff;
            MainWin.Height = screenH * winSizeCoeff;

            /* Setting the window in the center of the screen */
            MainWin.Left = (screenW - MainWin.Width) / 2;
            MainWin.Top = (screenH - MainWin.Height) / 2;

            MainFrame.Content = new MainPage();
        }

        private void GoToMainPage(object sender, RoutedEventArgs e)
        {
            if(MainFrame.Content.GetType() != typeof(MainPage))
            {
                MainFrame.Content = new MainPage();
            }
        }

        private void GoToAddingCosmicBodyPage(object sender, RoutedEventArgs e)
        {
            if (MainFrame.Content.GetType() != typeof(AddingCosmicBodyPage))
            {
                MainFrame.Content = new AddingCosmicBodyPage();
            }
        }

        private void GoToSettingsPage(object sender, RoutedEventArgs e)
        {
            if (MainFrame.Content.GetType() != typeof(SettingsPage))
            {
                MainFrame.Content = new SettingsPage();
            }
        }

        private void GoToProfilePage(object sender, RoutedEventArgs e)
        {
            if (MainFrame.Content.GetType() != typeof(ProfilePage))
            {
                MainFrame.Content = new ProfilePage();
            }
        }
    }
}
