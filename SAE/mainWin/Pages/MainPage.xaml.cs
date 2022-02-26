using Microsoft.EntityFrameworkCore;
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

namespace mainWin
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        NameValueCollection Config => ConfigurationManager.AppSettings;
        DbContextOptions<SAEDBContext> Options { get; set; }

        public MainPage()
        {
            InitializeComponent();
            DataContext = this;

            /* Configuring the database connection */
            string connectionString = Config.Get("ConnectionString") ?? "";
            string serverVersion = Config.Get("ServerVersion") ?? "";
            var optionsBuilder = new DbContextOptionsBuilder<SAEDBContext>()
                .UseMySql(connectionString, ServerVersion.Parse(serverVersion))
                .EnableDetailedErrors(true);
            Options = optionsBuilder.Options;

            /* Putting the stars in the ListView */
            using var db = new SAEDBContext(Options);
            var stars = db.Exoplanets.ToList();
            foreach (var s in stars)
            {
                s.DetectionMethodNavigation = db.ExoplanetDetectionMethods.FirstOrDefault(x => x.Id == s.DetectionMethod);
                s.DiscovererNavigation = db.Discoverers.FirstOrDefault(x => x.Id == s.Discoverer);
                s.TypeNavigation = db.ExoplanetTypes.FirstOrDefault(x => x.Id == s.Type);
                s.UserNavigation = db.Users.FirstOrDefault(x => x.Id == s.User);

                CosmicBodyList.Items.Add(s);
            }
        }

        private void CosmicBodyList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            InfoPanel.Items.Clear();

            var cosmicBody = (CosmicBody)CosmicBodyList.SelectedItem;
            CosmicBodyName.Text = cosmicBody.Name;
            InfoPanel.Items.Add(new InfoPanelItem(propName: "Название", propVal: cosmicBody.Name));
            InfoPanel.Items.Add(new InfoPanelItem(propName: "Статус", propVal: cosmicBody.Status == 0 ? "Не подтверждено" : "Подтверждено"));
            InfoPanel.Items.Add(new InfoPanelItem
            (
                propName: "Первооткрыватель",
                propVal: cosmicBody?.DiscovererNavigation?.Name,
                descPropVal: cosmicBody?.DiscovererNavigation?.Description
            ));
            InfoPanel.Items.Add(new InfoPanelItem(propName: "Дата добавления", propVal: cosmicBody?.DateAdded));
            InfoPanel.Items.Add(new InfoPanelItem(propName: "Дата подтверждения", propVal: cosmicBody?.DateConfirmation));
            InfoPanel.Items.Add(new InfoPanelItem(propName: "Масса", propVal: cosmicBody?.Mass));
            InfoPanel.Items.Add(new InfoPanelItem(propName: "Радиус", propVal: cosmicBody?.Radius));



            if (cosmicBody?.GetType() == typeof(Star))
            {
                var star = (Star)CosmicBodyList.SelectedItem;

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
            else if (cosmicBody?.GetType() == typeof(Exoplanet))
            {
                var exoplanet = (Exoplanet)CosmicBodyList.SelectedItem;
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
    }
}
