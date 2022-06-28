using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using SAE_DB;

namespace SAE_Program.Pages
{
    public class ChangePageViewModel : NotifyPropertyChanged, IDataPageViewModel<CelestialObject>, IDataErrorInfo
    {
        public ChangePageViewModel()
        {
            CommitCommand = new Command(delegate { Commit(); } , delegate { return !hasNameError; });
            LoadedCommand = new Command(delegate
            {
                Context = new();
            });
            UnLoadedCommand = new Command(delegate 
            { 
                Context!.Dispose(); 
                Context = null;
                Clear();
            });
            celestialObject = new Exoplanet();
            CurrentSession.OnUserTypeChanged += delegate 
            { 
                UserIsAdmin = CurrentSession.UserType == TypeUserEnum.Admin; 
            };
        }

        ~ChangePageViewModel()
        {
            Context?.Dispose();
        }

        public Command CommitCommand { get; set; }
        public Command LoadedCommand { get; set; }
        public Command UnLoadedCommand { get; set; }

        bool userIsAdmin;
        public bool UserIsAdmin {
            get => userIsAdmin;
            set 
            { 
                userIsAdmin = value;
                OnPropertyChanged(nameof(UserIsAdmin));
            } 
        }
        bool IsStatusChanged => Status != startStatus;
        bool IsNameChanged => Name != startName;
        bool hasNameError;
        string startName;
        StatusEnum startStatus;
        CelestialObject celestialObject = null!;
        SAEDBContext? _context;

        public bool IsExoplenetType
        { 
            get => CelestialObjectType == typeof(Exoplanet);
        }
        public bool IsEmpty
        {
            get => CelestialObject.Id == 0;
        }
        public string Name
        {
            get => CelestialObject.Name;
            set
            {
                var MaxLength = Context?.Entry(CelestialObject).Property(e => e.Name).Metadata.GetMaxLength();
                if (MaxLength.HasValue && value.Length > MaxLength)
                {
                    value = value[..MaxLength.Value];
                }
                celestialObject.Name = value;

                OnEntryPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(Name));
            }
        }
        public StatusEnum Status
        {
            get => CelestialObject.Status; 
            set
            {
                CelestialObject.Status = value;
                OnEntryPropertyChanged(nameof(Status));
                OnPropertyChanged(nameof(Status));
            }
        }
        public float? Mass { 
            get
            {
                if(celestialObject.Mass.HasValue)
                {
                    return celestialObject.Mass.Value;
                }
                return default;
            }
            set 
            {
                
                CelestialObject.Mass = value;
                OnEntryPropertyChanged(nameof(Mass)); 
                OnPropertyChanged(nameof(Mass));
            } 
        }
        public float? Radius { 
            get => CelestialObject.Radius; 
            set 
            {
                CelestialObject.Radius = value;
                OnEntryPropertyChanged(nameof(Radius));
                OnPropertyChanged(nameof(Radius));
            } 
        }
        public float? OrbitalRadius
        {
            get => (CelestialObject as Exoplanet)?.OrbitalRadius;
            set
            {
                if (CelestialObject is Exoplanet exoplanet)
                {
                    exoplanet.OrbitalRadius = value;
                    OnEntryPropertyChanged(nameof(OrbitalRadius));
                    OnPropertyChanged(nameof(OrbitalRadius));
                }
            }
        }
        public string? Description { 
            get => CelestialObject.Description; 
            set 
            {
                CelestialObject.Description = value;
                OnEntryPropertyChanged(nameof(Description));
                OnPropertyChanged(nameof(Description));
            } 
        }
        public NamedEntityWithByteId? DetectionMethod { 
            get => CelestialObject.GetDetectionMethod();
            set 
            {
                CelestialObject.SetDetectionMethod(value);

                OnEntryPropertyChanged(nameof(DetectionMethod));
                OnPropertyChanged(nameof(DetectionMethod));
            } 
        }
        public NamedEntityWithByteId? Type { 
            get => CelestialObject.Get_Type(); 
            set 
            {
                CelestialObject.SetType(value);
                OnEntryPropertyChanged(nameof(Type));
                OnPropertyChanged(nameof(Type));
            } 
        }
        public Discoverer? Discoverer { 
            get => CelestialObject.DiscovererNavigation; 
            set 
            {
                CelestialObject.Discoverer = value?.Id;
                CelestialObject.DiscovererNavigation = value;
                OnEntryPropertyChanged(nameof(Discoverer));
            } 
        }
        public Type CelestialObjectType
        {
            get => CelestialObject.GetType();
            set
            {
                //if (celestialObjectType != value)
                //{
                //    DetectionMethod = null;
                //    Type = null;
                //}
                
                if (value == typeof(Exoplanet) && IsEmpty)
                {
                    CelestialObject = new Exoplanet();
                } 
                else if (IsEmpty)
                {
                    CelestialObject = new Star();
                }
            }
        }
        public CelestialObject CelestialObject
        {
            get => celestialObject;
            set
            {
                celestialObject = value;
                Context?.Dispose();
                Context = new();
                startName = celestialObject.Name;
                startStatus = celestialObject.Status;

                OnPropertyChanged(nameof(Status));
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(Mass));
                OnPropertyChanged(nameof(Radius));
                OnPropertyChanged(nameof(OrbitalRadius));
                OnPropertyChanged(nameof(Description));
                OnPropertyChanged(nameof(DetectionMethod));
                OnPropertyChanged(nameof(Type));
                OnPropertyChanged(nameof(Discoverer));

                OnPropertyChanged(nameof(CelestialObjectType));
                OnPropertyChanged(nameof(CurrentTypeList));
                OnPropertyChanged(nameof(CurrentDetectionMethodList));
                OnPropertyChanged(nameof(IsExoplenetType));
                OnPropertyChanged(nameof(IsEmpty));
                var c = Context.Entry(CelestialObject).State;
            }
        }

        public static Type[] CelestialObjectTypeList { get => GeneralInfoFromDB.CelestialObjectTypeList; }
        public static StatusEnum[] StatusList { get => GeneralInfoFromDB.StatusList; }
        public IEnumerable<NamedEntityWithByteId> CurrentDetectionMethodList
        {
            get
            {
                if (CelestialObjectType == typeof(Star))
                {
                    return GeneralInfoFromDB.StarDetectionMethodList;
                } 
                else
                {
                    return GeneralInfoFromDB.ExoplanetDetectionMethodList;
                }
            }
        }
        public IEnumerable<NamedEntityWithByteId> CurrentTypeList
        {
            get
            {
                if (CelestialObjectType == typeof(Star))
                {
                    return GeneralInfoFromDB.StarTypeList;
                }
                else
                {
                    return GeneralInfoFromDB.ExoplanetTypeList;
                }
            }
        }

        public SAEDBContext? Context
        {
            get => _context; 
            set
            {
                _context = value;
                if (_context is null)
                {
                    return;
                }

                if (IsEmpty)
                {
                    _context.Entry(CelestialObject).State = EntityState.Added;
                }
                else
                {
                    _context.Entry(CelestialObject).State = EntityState.Unchanged;
                }
            }
        }

        public string Error => throw new NotImplementedException();

        public string this[string columnName]
        {
            get
            {
                string error = "";
                
                if (Context is not null)
                {
                    IQueryable<CelestialObject> queryable;
                    if (CelestialObject is Exoplanet)
                    {
                        queryable = Context.Exoplanets;
                    }
                    else
                    {
                        queryable = Context.Stars;
                    }

                    switch (columnName)
                    {
                        case nameof(Name):
                            var NameMaxLength = Context.Entry(CelestialObject).Property(e => e.Name).Metadata.GetMaxLength();
                            if (string.IsNullOrEmpty(Name))
                            {
                                error = "Название не может быть пустым";
                            }
                            else if (IsNameChanged && queryable.Select(e => e.Name).Contains(Name))
                            {
                                error = "Имена не могут повторяться";
                            }
                            break;
                    }
                }
                hasNameError = !string.IsNullOrEmpty(error);
                return error;
            }
        }

        public void UpdateData(CelestialObject? dataPresentor)
        {
            if (dataPresentor == null)
            {
                return;
            }
            
            CelestialObject = (CelestialObject)dataPresentor.Clone();
            
        }
        public void SetEmptyData()
        {
            CelestialObject = new Exoplanet();
        }

        public event Action<CelestialObject>? OnCommit;
        public void Commit()
        {
            if (Context is null)
            {
                return;
            }

            if (IsStatusChanged && Status == StatusEnum.Confirmed)
            {
                CelestialObject.DateTimeConfirmation = DateTime.Now;
                OnEntryPropertyChanged(nameof(CelestialObject.DateTimeConfirmation));
            }
            else if (IsStatusChanged && Status == StatusEnum.NotConfirmed)
            {
                CelestialObject.DateTimeConfirmation = null;
                OnEntryPropertyChanged(nameof(CelestialObject.DateTimeConfirmation));
            }

            if (IsEmpty)
            {
                CelestialObject.DateTimeAdded = DateTime.Now;
                OnEntryPropertyChanged(nameof(CelestialObject.DateTimeAdded));
            }
            
            var IsEmptyBuf = IsEmpty;
            Context.SaveChanges();
            OnCommit?.Invoke(CelestialObject);
            if (IsEmptyBuf)
            {
                var celestialObject = (CelestialObject)CelestialObject.Clone();
                celestialObject.Id = 0;
                CelestialObject = celestialObject;
            }
        }
        
        public void Clear()
        {
            Name = string.Empty;
            Mass = default;
            Radius = null;
            OrbitalRadius = null;
            Description = null;
            DetectionMethod = null;
            Type = null;
            Discoverer = null;
        }

        protected void OnEntryPropertyChanged([CallerMemberName] string prop = "")
        {
            if (Context is not null && !string.IsNullOrEmpty(prop) && !IsEmpty)
            {
                Context.Entry(CelestialObject).Property(prop).IsModified = true;
            }
        }
    }
}
