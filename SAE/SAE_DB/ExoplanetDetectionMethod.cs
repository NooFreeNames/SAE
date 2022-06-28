using System;
using System.Collections.Generic;

namespace SAE_DB
{
    public partial class ExoplanetDetectionMethod : NamedEntityWithByteId
    {
        public ExoplanetDetectionMethod()
        {
            Exoplanets = new HashSet<Exoplanet>();
        }

        public virtual ICollection<Exoplanet> Exoplanets { get; set; }
    }
}
