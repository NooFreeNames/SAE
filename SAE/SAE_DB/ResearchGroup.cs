using System;
using System.Collections.Generic;

namespace SAE_DB
{
    public partial class ResearchGroup : NamedEntityWithUintId
    {
        public ResearchGroup()
        {
            Users = new HashSet<User>();
        }

        public virtual ICollection<User> Users { get; set; }
    }
}
