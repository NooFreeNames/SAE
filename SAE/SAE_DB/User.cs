using System;
using System.Collections.Generic;

namespace SAE_DB
{
    public partial class User
    {
        public User()
        {
            Exoplanets = new HashSet<Exoplanet>();
            Stars = new HashSet<Star>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int PasswordHach { get; set; }
        public int TupeUser { get; set; }
        public int? ResearchGroup { get; set; }

        public virtual ResearchGroup? ResearchGroupNavigation { get; set; }
        public virtual UserType TupeUserNavigation { get; set; } = null!;
        public virtual ICollection<Exoplanet> Exoplanets { get; set; }
        public virtual ICollection<Star> Stars { get; set; }
    }
}
