using System;
using System.Collections.Generic;

namespace SAE_DB
{
    public partial class StarType : NamedEntityWithByteId
    {
        public StarType()
        {
            Stars = new HashSet<Star>();
        }

        public virtual ICollection<Star> Stars { get; set; }
    }
}
