using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SAE_DB;

namespace SAE_Program.Pages
{
    interface ISearchFiltrs
    {
        public event Action? OnChangingFilters;
        public void SetDefaultFiltrs();
    }

    public class SearchFiltrs : NotifyPropertyChanged, ISearchFiltrs
    {
        public SearchFiltrs()
        {
            SetDefaultFiltrs();
            SetDefaultFiltrsCommand = new Command(delegate { SetDefaultFiltrs(); });
        }

        public event Action? OnChangingFilters;
        public Command SetDefaultFiltrsCommand { get; set; }

        const uint minRowCount = 1u;
        const uint rowCountDefault = 50u;
        const CelestialObjectEnum typeDefault = CelestialObjectEnum.Exoplanet;
        const CelestialObjectPropsEnum orderByDefault = CelestialObjectPropsEnum.Id;
        const CelestialObjectPropsEnum searchByDefault = CelestialObjectPropsEnum.Name;

        uint rowCount;
        CelestialObjectEnum type;
        CelestialObjectPropsEnum orderBy;
        CelestialObjectPropsEnum searchBy;
        
        public uint RowCount
        {
            get { return rowCount; }
            set
            {
                rowCount = value <= minRowCount ? minRowCount : value;
                OnPropertyChanged(nameof(RowCount));
            }
        }
        public CelestialObjectEnum Type
        {
            get { return type; }
            set
            {
                type = value;
                OnPropertyChanged(nameof(Type));
            }
        }
        public CelestialObjectPropsEnum OrderBy
        {
            get { return orderBy; }
            set
            {
                orderBy = value;
                OnPropertyChanged(nameof(OrderBy));
            }
        }
        public CelestialObjectPropsEnum SearchBy
        {
            get { return searchBy; }
            set
            {
                searchBy = value;
                OnPropertyChanged(nameof(SearchBy));
            }
        }
        public CelestialObjectEnum[] TypeList { get; set; } = new[]
        {
            CelestialObjectEnum.Star,
            CelestialObjectEnum.Exoplanet,
        };
        public CelestialObjectPropsEnum[] PropList { get; set; } = new[]
        {
            CelestialObjectPropsEnum.Id,
            CelestialObjectPropsEnum.Name,
            CelestialObjectPropsEnum.DetectionMethod,
            CelestialObjectPropsEnum.DateTimeAdded,
            CelestialObjectPropsEnum.DateTimeConfirmation,
            CelestialObjectPropsEnum.Discoverer,
        };

        public void SetDefaultFiltrs()
        {
            RowCount = rowCountDefault;
            Type = typeDefault;
            OrderBy = orderByDefault;
            SearchBy = searchByDefault;
        }
        protected override void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            base.OnPropertyChanged(prop);
            OnChangingFilters?.Invoke();
        }
    }
}
