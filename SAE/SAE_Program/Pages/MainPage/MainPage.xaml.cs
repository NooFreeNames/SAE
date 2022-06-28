
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
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
using SAE_DB;
using SAE_Program.Pages;
using System.Linq.Expressions;
using System.ComponentModel;
using System.Windows.Controls.Primitives;
using Microsoft.Xaml.Behaviors;

namespace SAE_Program.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        InfoPage infoPage = new();
        ChangesPage changesPage = new();
        
        public MainPage()
        {
            InitializeComponent();
            DataContext = new MainPageViewModel();
            object? o = null;
            if (o as Exoplanet != null)
            {
                return;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (filtersEl.Visibility == Visibility.Collapsed)
            {
                filtersEl.Visibility = Visibility.Visible;
            }
            else
            {
                filtersEl.Visibility = Visibility.Collapsed;
            }
        }
    }
}
