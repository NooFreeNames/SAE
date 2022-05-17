using System;
using System.Collections.Generic;

namespace SAE_DB
{
    public partial class ResearchGroup
    {
        public ResearchGroup()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
