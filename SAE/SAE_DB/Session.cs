using System;
using System.Collections.Generic;

namespace SAE_DB
{
    public partial class Session : IEntityWithUintId
    {
        public uint Id { get; set; }
        public uint User { get; set; }
        public string DeviceIdHash { get; set; } = null!;

        public virtual User UserNavigation { get; set; } = null!;
    }
}
