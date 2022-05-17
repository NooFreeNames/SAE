using System;
using System.Collections.Generic;

namespace SAE_DB
{
    public partial class Discoverer
    {
        public Discoverer()
        {
            Exoplanets = new HashSet<Exoplanet>();
            Stars = new HashSet<Star>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<Exoplanet> Exoplanets { get; set; }
        public virtual ICollection<Star> Stars { get; set; }
    }
}
