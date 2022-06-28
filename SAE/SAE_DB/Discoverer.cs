using System;
using System.Collections.Generic;

namespace SAE_DB
{
    public partial class Discoverer : NamedEntityWithUintId
    {
        public Discoverer()
        {
            Exoplanets = new HashSet<Exoplanet>();
            Stars = new HashSet<Star>();
        }

        public virtual ICollection<Exoplanet> Exoplanets { get; set; }
        public virtual ICollection<Star> Stars { get; set; }
    }
}
