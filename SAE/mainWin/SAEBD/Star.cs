using System;
using System.Collections.Generic;

namespace mainWin
{
    public partial class Star : CosmicBody
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
