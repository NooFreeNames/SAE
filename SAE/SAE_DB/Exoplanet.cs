using System;
using System.Collections.Generic;

namespace SAE_DB
{
    public partial class Exoplanet : CelestialObject
    {
        public Exoplanet()
        {
            Stars = new HashSet<Star>();
        }

        public float? OrbitalRadius { get; set; }

        public virtual ExoplanetDetectionMethod? DetectionMethodNavigation { get; set; }
        public virtual ExoplanetType? TypeNavigation { get; set; }

        public virtual ICollection<Star> Stars { get; set; }
    }
}
