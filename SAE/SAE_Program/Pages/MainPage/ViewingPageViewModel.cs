using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAE_DB;
namespace SAE_Program.Pages
{
    internal class ViewingPageViewModel : NotifyPropertyChanged, IDataPageViewModel<CelestialObject>
    {
        string name = string.Empty;
        public string Name
        {
            get => name; 
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public ObservableCollection<Property> PropertyList { get; set; } = new();
        public bool IsEmpty { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void UpdateData(CelestialObject? celestialObject)
        {
            if (celestialObject == null)
            {
                return;
            }

            PropertyList.Clear();
            Name = celestialObject.Name;
            PropertyList.Add(new Property("Название", celestialObject.Name));
            PropertyList.Add(new Property("Статус", new StatusConverter().Convert(celestialObject.Status)));
            PropertyList.Add(new Property
            (
                "Первооткрыватель",
                celestialObject.DiscovererNavigation?.Name,
                celestialObject.DiscovererNavigation?.Description
            ));
            PropertyList.Add(new Property("Дата добавления", celestialObject.DateTimeAdded));
            PropertyList.Add(new Property("Дата подтверждения", celestialObject.DateTimeConfirmation));
            PropertyList.Add(new Property("Масса", celestialObject.Mass));
            PropertyList.Add(new Property("Радиус", celestialObject.Radius));


            if (celestialObject.GetType() == typeof(Star))
            {
                var star = (Star)celestialObject;

                PropertyList.Add(new Property
                (
                    "Тип",
                    star?.TypeNavigation?.Name,
                    star?.TypeNavigation?.Description
                ));

                PropertyList.Add(new Property
                (
                    "Метод обнаружения",
                    star?.DetectionMethodNavigation?.Name,
                    star?.DetectionMethodNavigation?.Description
                ));
            }

            if (celestialObject.GetType() == typeof(Exoplanet))
            {
                var exoplanet = (Exoplanet)celestialObject;
                PropertyList.Add(new Property("Радиус орбиты", exoplanet?.OrbitalRadius));
                PropertyList.Add(new Property
                (
                    "Тип",
                    exoplanet?.TypeNavigation?.Name,
                    exoplanet?.TypeNavigation?.Description
                ));

                PropertyList.Add(new Property
                (
                    "Метод обнаружения",
                    exoplanet?.DetectionMethodNavigation?.Name,
                    exoplanet?.DetectionMethodNavigation?.Description
                ));
            }
        }
        public void SetEmptyData()
        {
            PropertyList.Clear();
            Name = string.Empty;
        }
    }
}
