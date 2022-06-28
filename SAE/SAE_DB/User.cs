using System;
using System.Collections.Generic;

namespace SAE_DB
{
    public partial class User : IEntityWithUintId
    {
        public User()
        {
            ExoplanetUserWhoAddedNavigations = new HashSet<Exoplanet>();
            ExoplanetUserWhoConfirmedNavigations = new HashSet<Exoplanet>();
            Sessions = new HashSet<Session>();
            StarUserWhoAddedNavigations = new HashSet<Star>();
            StarUserWhoConfirmedNavigations = new HashSet<Star>();
            ResearchGroups = new HashSet<ResearchGroup>();
        }

        public uint Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHach { get; set; } = null!;
        public TypeUserEnum TupeUser { get; set; }
        public DateTime RegistrationDataTime { get; set; }

        public virtual ICollection<Exoplanet> ExoplanetUserWhoAddedNavigations { get; set; }
        public virtual ICollection<Exoplanet> ExoplanetUserWhoConfirmedNavigations { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
        public virtual ICollection<Star> StarUserWhoAddedNavigations { get; set; }
        public virtual ICollection<Star> StarUserWhoConfirmedNavigations { get; set; }

        public virtual ICollection<ResearchGroup> ResearchGroups { get; set; }
    }

    public enum TypeUserEnum
    {
        None,
        Scientist,
        Admin,
    }
}
