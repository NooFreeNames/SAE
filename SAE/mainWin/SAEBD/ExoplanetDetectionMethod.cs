using System;
using System.Collections.Generic;

namespace mainWin
{
    public partial class ExoplanetDetectionMethod
    {
        public ExoplanetDetectionMethod()
        {
            Exoplanets = new HashSet<Exoplanet>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<Exoplanet> Exoplanets { get; set; }
    }
}
