﻿
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

namespace SAE_Program
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {

        public MainPage()
        {
            InitializeComponent();
            DataContext = this;

            /* Configuring the database connection */


            Render();
        }
        


        private void CosmicBodyList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            InfoPanel.Items.Clear();

            if (CosmicBodyListEl.SelectedItem == null)
            {
                return;
            }

            var cosmicBody = (CosmicBody)CosmicBodyListEl.SelectedItem;
            CosmicBodyName.Text = cosmicBody?.Name;
            InfoPanel.Items.Add(new InfoPanelItem(propName: "Название", propVal: cosmicBody?.Name));
            InfoPanel.Items.Add(new InfoPanelItem(propName: "Статус", propVal: cosmicBody?.Status == 0 ? "Не подтверждено" : "Подтверждено"));
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
            
            if (cosmicBody?.GetType() == typeof(Exoplanet))
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

            string searchByDM = $"(SELECT Name FROM {AppConfig.SearchFilters.Type}_Detection_Method WHERE {AppConfig.SearchFilters.Type}.DetectionMethod = {AppConfig.SearchFilters.Type}_Detection_Method.Id)";
            string searchByTy = $"(SELECT Name FROM {AppConfig.SearchFilters.Type}_Type WHERE {AppConfig.SearchFilters.Type}.Type = {AppConfig.SearchFilters.Type}_Type.Id)";
            string searchBy = AppConfig.SearchFilters.SearchBy.ToString();
            if (AppConfig.SearchFilters.SearchBy == CosmicBodyPropEnum.DetectionMethod)
            {
                searchBy = searchByDM;
            }
            else if (AppConfig.SearchFilters.SearchBy == CosmicBodyPropEnum.Type)
            {
                searchBy = searchByTy;
            }

            string orderBy = AppConfig.SearchFilters.OrderBy.ToString();
            if (AppConfig.SearchFilters.OrderBy == CosmicBodyPropEnum.DetectionMethod)
            {
                orderBy = searchByDM;
            }
            else if (AppConfig.SearchFilters.OrderBy == CosmicBodyPropEnum.Type)
            {
                orderBy = searchByTy;
            }


            string whereStr = $"WHERE {searchBy} LIKE '{searchBarEl.Text}%'";
            if (searchBarEl.Text == "")
            {
                whereStr = "";
            }
            else if (searchBarEl.Text == "-")
            {
                whereStr = $"WHERE {searchBy} IS NULL";
            }
            string sql = $"SELECT * FROM {AppConfig.SearchFilters.Type} {whereStr} ORDER BY {orderBy}";

            IQueryable<CosmicBody> cosmicBodies;
            if (AppConfig.SearchFilters.Type == CosmicBodyEnum.Exoplanet)
            {
                cosmicBodies = db.Exoplanets.FromSqlRaw(sql)
                    .Include(s => s.DetectionMethodNavigation)
                    .Include(s => s.TypeNavigation);
            }
            else
            {
                cosmicBodies = db.Stars.FromSqlRaw(sql)
                    .Include(s => s.DetectionMethodNavigation)
                    .Include(s => s.TypeNavigation);
            }


            cosmicBodies = cosmicBodies
                .Include(c => c.DiscovererNavigation)
                .Include(c => c.UserNavigation);

            CosmicBodyListEl.ItemsSource = cosmicBodies.ToList();
        }

        private void searchBarEl_TextChanged(object sender, TextChangedEventArgs e)
        {
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
    }
}