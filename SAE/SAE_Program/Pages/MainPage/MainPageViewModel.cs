using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using SAE_DB;

namespace SAE_Program.Pages
{
    public class MainPageViewModel : NotifyPropertyChanged
    {
        public MainPageViewModel()
        {
            NextPageCommand = new Command(delegate { CurrentPageNum++; }, 
                delegate { return CurrentPageNum < MaxPageNum; });
            LastPageCommand = new Command(delegate { CurrentPageNum = MaxPageNum; }, 
                delegate { return CurrentPageNum < MaxPageNum;});

            PrevPageCommand = new Command(delegate { CurrentPageNum--; }, 
                delegate { return CurrentPageNum > minPageNum; });
            FirstPageCommand = new Command(delegate { CurrentPageNum = minPageNum; }, 
                delegate { return CurrentPageNum > minPageNum; });

            ViewingModeCommand = new Command(delegate { IsViewingMode = true; }, 
                delegate { return SelectedCelestialObject != null && !IsViewingMode; });
            EditingModeCommand = new Command(delegate { IsEditingMode = true; }, 
                delegate { return SelectedCelestialObject != null && !IsEditingMode && !isOnlyViewingMode; });
            AddingModeCommand = new Command(delegate { IsAddingMode = true; }, 
                delegate { return !IsAddingMode && !isOnlyViewingMode; });
            RemoveCommand = new Command(delegate
            {
                using var db = new SAEDBContext();
                if (SelectedCelestialObject is Star star)
                {
                    db.Remove(star);
                } 
                else if (SelectedCelestialObject is Exoplanet exoplanet)
                {
                    db.Remove(exoplanet);
                }
                db.SaveChanges();
                UpdateMaxPageNum();
                UpdateCelestialObjectList();
                
            }, delegate { return !isOnlyViewingMode; });



            SearchFiltrs = new SearchFiltrs();
            SearchFiltrs.OnChangingFilters += delegate { UpdateCelestialObjectList(); UpdateMaxPageNum(); };
            CurrentSession.OnUserTypeChanged += delegate
            {
                isOnlyViewingMode = CurrentSession.UserType == TypeUserEnum.None;
                if (isOnlyViewingMode) { IsViewingMode = true; }
            };

            ((ChangePageViewModel)changesPage.DataContext).OnCommit += CommitUpdate;
            UpdateValuesList();
            IsViewingMode = true;
        }

        void CommitUpdate(CelestialObject celestialObject)
        {
            using var db = new SAEDBContext();
            UpdateMaxPageNum();
            IQueryable<CelestialObject> queryable;
            if (celestialObject is Exoplanet)
            {
                queryable = db.Exoplanets;
                SearchFiltrs.Type = CelestialObjectEnum.Exoplanet;
            } 
            else
            {
                queryable = db.Stars;
                SearchFiltrs.Type = CelestialObjectEnum.Star;
            }
            SearchFiltrs.OrderBy = CelestialObjectPropsEnum.Id;
            SearchString = string.Empty;
            CurrentPageNum = ((uint)queryable.Where(e => e.Id < celestialObject.Id).Count()) / SearchFiltrs.RowCount + 1u;
            SelectedCelestialObject = celestialObject;
        }

        bool isOnlyViewingMode;
        readonly InfoPage infoPage = new();
        readonly ChangesPage changesPage = new();
        Page currentPage;
        public Page CurrentPage
        {
            get => currentPage; 
            set
            {
                currentPage = value;
                
                OnPropertyChanged(nameof(CurrentPage));
            }
        }

        public Command NextPageCommand { get; set; }
        public Command PrevPageCommand { get; set; }
        public Command FirstPageCommand { get; set; }
        public Command LastPageCommand { get; set; }
        
        public Command ViewingModeCommand { get; set; }
        public Command EditingModeCommand { get; set; }
        public Command AddingModeCommand { get; set; }
        public Command RemoveCommand { get; set; }

        const uint minPageNum = 1u;
        uint currentPageNum;
        uint maxPageNum;
        string searchString = null!;
        CelestialObject[] celestialObjectList = null!;
        CelestialObject? selectedCelestialObject;
        SearchFiltrs searchFiltrs = null!;
        bool isAddingMode = false;
        bool isEditingMode = false;
        bool isViewingMode = false;
        bool isStringQuery = true;
        bool isDateRangeQuery = false;
        bool isListQuery = false;

        public bool IsAddingMode
        {
            get => isAddingMode; 
            set
            {
                isAddingMode = value;
                if (value)
                {
                    CurrentPage = changesPage;
                    ((IDataPageViewModel<CelestialObject>)CurrentPage.DataContext).SetEmptyData();
                    IsViewingMode = false;
                    IsEditingMode = false;
                }
                OnPropertyChanged(nameof(IsAddingMode));
            }
        }
        public bool IsEditingMode
        {
            get => isEditingMode; 
            set
            {
                isEditingMode = value;
                if (value)
                {
                    CurrentPage = changesPage;
                    ((IDataPageViewModel<CelestialObject>)CurrentPage.DataContext).UpdateData(SelectedCelestialObject);
                    IsAddingMode = false;
                    IsViewingMode = false;
                }
                OnPropertyChanged(nameof(IsEditingMode));
            }
        }
        public bool IsViewingMode
        {
            get => isViewingMode; 
            set
            {
                isViewingMode = value;
                if (value)
                {
                    CurrentPage = infoPage;
                    ((IDataPageViewModel<CelestialObject>)CurrentPage.DataContext).UpdateData(SelectedCelestialObject);
                    IsAddingMode = false;
                    IsEditingMode = false;
                }
                OnPropertyChanged(nameof(IsViewingMode));
            }
        }
        bool IsStringQuery
        {
            get { return isStringQuery; }
            set
            {
                isStringQuery = value;
                OnPropertyChanged(nameof(IsStringQuery));
            }
        }
        bool IsDateRangeQuery
        {
            get { return isDateRangeQuery; }
            set
            {
                isDateRangeQuery = value;
                OnPropertyChanged(nameof(IsDateRangeQuery));
            }
        }
        bool IsListQuery
        {
            get { return isListQuery; }
            set
            {
                isListQuery = value;
                OnPropertyChanged(nameof(IsListQuery));
            }
        }
        public uint CurrentPageNum
        {
            get { return currentPageNum; }
            set
            {
                if (value <= minPageNum)
                {
                    currentPageNum = minPageNum;
                }
                else if (value >= MaxPageNum)
                {
                    currentPageNum = MaxPageNum;
                }
                else
                {
                    currentPageNum = value;
                }

                OnPropertyChanged(nameof(CurrentPageNum));
                UpdateCelestialObjectList();
            }
        }
        public uint MaxPageNum
        {
            get { return maxPageNum; }
            set
            {
                maxPageNum = value <= minPageNum ? minPageNum : value;
                if (CurrentPageNum > maxPageNum)
                {
                    CurrentPageNum = maxPageNum;
                }
                OnPropertyChanged(nameof(MaxPageNum));
            }
        }
        public string SearchString
        {
            get { return searchString; }
            set
            {
                searchString = value;
                CurrentPageNum = minPageNum;
                OnPropertyChanged(nameof(SearchString));
                UpdateMaxPageNum();
            }
        }
        public CelestialObject[] CelestialObjectList
        {
            get { return celestialObjectList; }
            set
            {
                celestialObjectList = value;
                OnPropertyChanged(nameof(CelestialObjectList));
            }
        }
        public CelestialObject? SelectedCelestialObject
        {
            get { return selectedCelestialObject; }
            set
            {
                selectedCelestialObject = value;
                if (!IsAddingMode)
                {
                    ((IDataPageViewModel<CelestialObject>)CurrentPage.DataContext).UpdateData(SelectedCelestialObject);
                }
                
                OnPropertyChanged(nameof(SelectedCelestialObject));
                
            }
        }
        public SearchFiltrs SearchFiltrs
        {
            get { return searchFiltrs; }
            set
            {
                searchFiltrs = value;
                OnPropertyChanged(nameof(SearchFiltrs));
            }
        }
        
        public string[] ValuesList { get; set; }


        private void UpdateCelestialObjectList()
        {
            using var db = new SAEDBContext();
            db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            
            var sqlQuery = QueryConverter
                .ToSQLQuery(SearchString, CurrentPageNum, SearchFiltrs)
                .ToSQLString(SQLQueryParams.All);
            IQueryable<CelestialObject> celestialObjecties;
            if (SearchFiltrs.Type == CelestialObjectEnum.Exoplanet)
            {
                celestialObjecties = db.Exoplanets.FromSqlRaw(sqlQuery)
                    .Include(s => s.DetectionMethodNavigation)
                    .Include(s => s.TypeNavigation);
            }
            else
            {
                celestialObjecties = db.Stars.FromSqlRaw(sqlQuery)
                    .Include(s => s.DetectionMethodNavigation)
                    .Include(s => s.TypeNavigation);
            }


            celestialObjecties = celestialObjecties
                .Include(c => c.DiscovererNavigation)
                .Include(c => c.UserWhoAddedNavigation)
                .Include(c => c.UserWhoConfirmedNavigation);

            CelestialObjectList = celestialObjecties.ToArray();
        }
        private void UpdateMaxPageNum()
        {
            using var db = new SAEDBContext();
            
            var sqlQuery = QueryConverter
                .ToSQLQuery(SearchString, CurrentPageNum, SearchFiltrs)
                .ToSQLString(SQLQueryParams.WhereStr);
            int count;
            if (SearchFiltrs.Type == CelestialObjectEnum.Exoplanet)
            {
                count = db.Exoplanets.FromSqlRaw(sqlQuery).Count();
            }
            else
            {
                count = db.Stars.FromSqlRaw(sqlQuery).Count();
            }

            MaxPageNum = (uint)Math.Ceiling((double)count / SearchFiltrs.RowCount);
        }
        
        private void UpdateValuesList()
        {
            using var db = new SAEDBContext();
            switch (SearchFiltrs.SearchBy)
            {
                case CelestialObjectPropsEnum.DetectionMethod:
                    if (SearchFiltrs.Type == CelestialObjectEnum.Star)
                    {
                        ValuesList = GeneralInfoFromDB.StarDetectionMethodList.Select(s => s.Name).ToArray();
                    }
                    else if (SearchFiltrs.Type == CelestialObjectEnum.Exoplanet)
                    {
                        ValuesList = GeneralInfoFromDB.ExoplanetDetectionMethodList.Select(s => s.Name).ToArray();
                    }
                    break;
                case CelestialObjectPropsEnum.Status:
                    ValuesList = new string[] { StatusConverter.confirmed, StatusConverter.notConfirmed };
                    break;
            }
        }
    }
}
