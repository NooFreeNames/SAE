using System;
using System.Collections.Generic;

namespace SAE_DB
{
    public partial class ExoplanetType : NamedEntityWithByteId
    {
        public ExoplanetType()
        {
            Exoplanets = new HashSet<Exoplanet>();
        }

        public virtual ICollection<Exoplanet> Exoplanets { get; set; }
    }
}
