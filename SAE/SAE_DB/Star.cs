using System;
using System.Collections.Generic;

namespace SAE_DB
{
    public partial class Star : CelestialObject
    {
        public Star()
        {
            Exoplanes = new HashSet<Exoplanet>();
        }

        public virtual StarDetectionMethod? DetectionMethodNavigation { get; set; }
        public virtual StarType? TypeNavigation { get; set; }
        public virtual ICollection<Exoplanet> Exoplanes { get; set; }
    }
}
