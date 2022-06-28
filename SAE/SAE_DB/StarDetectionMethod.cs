using System;
using System.Collections.Generic;

namespace SAE_DB
{
    public partial class StarDetectionMethod : NamedEntityWithByteId
    {
        public StarDetectionMethod()
        {
            Stars = new HashSet<Star>();
        }

        public virtual ICollection<Star> Stars { get; set; }
    }
}
