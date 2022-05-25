
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
using  System.Linq.Expressions;
using System.ComponentModel;
namespace SAE_Program
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public static readonly DependencyProperty CelestialObjectListElPageProperty;

        uint defoutVAl = 1u;
        public uint CelestialObjectListElPage
        {
            get { return (uint)GetValue(CelestialObjectListElPageProperty); }
            set
            {
                SetValue(CelestialObjectListElPageProperty, value);
                LeftBtn.IsEnabled = true;
                RightBtn.IsEnabled = true;
                if (value <= defoutVAl)
                {
                    SetValue(CelestialObjectListElPageProperty, defoutVAl);
                    LeftBtn.IsEnabled = false;
                }
                if (value >= CelestialObjectListElItemsNum)
                {
                    SetValue(CelestialObjectListElPageProperty, CelestialObjectListElItemsNum);
                    RightBtn.IsEnabled = false;
                }

                Render();
            }
        }

        public static readonly DependencyProperty CelestialObjectListElItemsNumProperty;
        public uint CelestialObjectListElItemsNum
        {
            get { return (uint)GetValue(CelestialObjectListElItemsNumProperty); }
            set 
            { 
                if (value <= defoutVAl)
                {
                    SetValue(CelestialObjectListElItemsNumProperty, defoutVAl);
                } else
                {
                    SetValue(CelestialObjectListElItemsNumProperty, value);
                    
                }
            }
        }

        static MainPage()
        {
            CelestialObjectListElItemsNumProperty = DependencyProperty.Register(
            "CelestialObjectListElItemsNum",
            typeof(uint),
            typeof(MainPage));

            CelestialObjectListElPageProperty = DependencyProperty.Register(
            "CelestialObjectListElPage",
            typeof(uint),
            typeof(MainPage));
        }
        
        public MainPage()
        {
            InitializeComponent();
            DataContext = this;
            SetValue(CelestialObjectListElPageProperty, 1u);
            Render();
        }
        


        private void CosmicBodyList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            InfoPanel.Items.Clear();

            var cosmicBody = CelestialObjectListEl.SelectedItem as CelestialObject;
            if (cosmicBody == null)
            {
                return;
            }
            CosmicBodyName.Text = cosmicBody.Name;
            InfoPanel.Items.Add(new InfoPanelItem(propName: "Название", propVal: cosmicBody.Name));
            InfoPanel.Items.Add(new InfoPanelItem(propName: "Статус", propVal:  new StatusConverter().Convert(cosmicBody.Status)));
            InfoPanel.Items.Add(new InfoPanelItem
            (
                propName: "Первооткрыватель",
                propVal: cosmicBody.DiscovererNavigation?.Name,
                descPropVal: cosmicBody.DiscovererNavigation?.Description
            ));
            InfoPanel.Items.Add(new InfoPanelItem(propName: "Дата добавления", propVal: cosmicBody.DateAdded));
            InfoPanel.Items.Add(new InfoPanelItem(propName: "Дата подтверждения", propVal: cosmicBody.DateConfirmation));
            InfoPanel.Items.Add(new InfoPanelItem(propName: "Масса", propVal: cosmicBody.Mass));
            InfoPanel.Items.Add(new InfoPanelItem(propName: "Радиус", propVal: cosmicBody.Radius));


            if (cosmicBody.GetType() == typeof(Star))
            {
                var star = (Star)cosmicBody;

                InfoPanel.Items.Add(new InfoPanelItem
                (
                    propName: "Тип",
                    propVal: star?.TypeNavigation?.Name,
                    descPropVal: star?.TypeNavigation?.Description
                ));

                InfoPanel.Items.Add(new InfoPanelItem
                (
                    propName: "Метод обнаружения",
                    propVal: star?.DetectionMethodNavigation?.Name,
                    descPropVal: star?.DetectionMethodNavigation?.Description
                ));
            }
            
            if (cosmicBody.GetType() == typeof(Exoplanet))
            {
                var exoplanet = (Exoplanet)cosmicBody;
                InfoPanel.Items.Add(new InfoPanelItem(propName: "Радиус орбиты", propVal: exoplanet?.OrbitalRadius));
                InfoPanel.Items.Add(new InfoPanelItem
                (
                    propName: "Тип",
                    propVal: exoplanet?.TypeNavigation?.Name,
                    descPropVal: exoplanet?.TypeNavigation?.Description
                ));

                InfoPanel.Items.Add(new InfoPanelItem
                (
                    propName: "Метод обнаружения",
                    propVal: exoplanet?.DetectionMethodNavigation?.Name,
                    descPropVal: exoplanet?.DetectionMethodNavigation?.Description
                ));
            }
        }


        private void Render()
        {
            using var db = new SAEDBContext();

            var sqlQuery = new SQLQuery(searchBarEl.Text, CelestialObjectListElPage);
            IQueryable<CelestialObject> celestialObjecties;
            int count;
            if (SQLQuery.Type == CelestialObjectEnum.Exoplanet)
            {
                count = db.Exoplanets.FromSqlRaw(sqlQuery.WhereSQLString).Count();
                celestialObjecties = db.Exoplanets.FromSqlRaw(sqlQuery.SQLString)
                    .Include(s => s.DetectionMethodNavigation)
                    .Include(s => s.TypeNavigation);
            }
            else
            {
                count = db.Exoplanets.FromSqlRaw(sqlQuery.WhereSQLString).Count();
                celestialObjecties = db.Stars.FromSqlRaw(sqlQuery.SQLString)
                    .Include(s => s.DetectionMethodNavigation)
                    .Include(s => s.TypeNavigation);
            }


            celestialObjecties = celestialObjecties
                .Include(c => c.DiscovererNavigation)
                .Include(c => c.UserNavigation);

            CelestialObjectListElItemsNum = (uint)Math.Ceiling((double)count / SQLQuery.ItemNum);
            CelestialObjectListEl.ItemsSource = celestialObjecties.ToArray();
        }


        private void searchBarEl_TextChanged(object sender, TextChangedEventArgs e)
        {
            CelestialObjectListElPage = defoutVAl;
            Render();
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CelestialObjectListElPage++;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            CelestialObjectListElPage--;
        }

        private void Button_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
