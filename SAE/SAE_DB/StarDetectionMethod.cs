using System;
using System.Collections.Generic;

namespace SAE_DB
{
    public partial class StarDetectionMethod
    {
        public StarDetectionMethod()
        {
            Stars = new HashSet<Star>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<Star> Stars { get; set; }
    }
}
