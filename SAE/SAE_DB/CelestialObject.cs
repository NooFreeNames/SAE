using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE_DB
{
    public abstract class CelestialObject : NamedEntityWithUlongId, ICloneable
    {
        public StatusEnum Status { get; set; } 
        public DateTime DateTimeAdded { get; set; }
        public DateTime? DateTimeConfirmation { get; set; }
        public float? Mass { get; set; }
        public float? Radius { get; set; }
        public uint? UserWhoAdded { get; set; }
        public uint? UserWhoConfirmed { get; set; }
        public byte? DetectionMethod { get; set; }
        public byte? Type { get; set; }
        public uint? Discoverer { get; set; }

        public virtual Discoverer? DiscovererNavigation { get; set; }
        public virtual User? UserWhoAddedNavigation { get; set; }
        public virtual User? UserWhoConfirmedNavigation { get; set; }

        public abstract NamedEntityWithByteId? GetDetectionMethod();
        public abstract NamedEntityWithByteId? Get_Type();
        public virtual void SetDetectionMethod(NamedEntityWithByteId? detectionMethod)
        {
            if (DetectionMethod is null)
            {
                DetectionMethod = detectionMethod?.Id;
            } 
        }
        public virtual void SetType(NamedEntityWithByteId? type)
        {
            Type = type?.Id;
        }
        public virtual object Clone()
        {
            return MemberwiseClone();
        }
    }

    public enum StatusEnum
    {
        NotConfirmed,
        Confirmed,
    }

    public enum CelestialObjectEnum
    {
        Galaxy,
        Star,
        Exoplanet,
        Asteroid,
    }

    public enum CelestialObjectPropsEnum
    {
        Id,
        Name,
        Status,
        DateTimeAdded,
        DateTimeConfirmation,
        Mass,
        Radius,
        OrbitalRadius,
        User,
        DetectionMethod,
        Type,
        Discoverer,
    }
}
