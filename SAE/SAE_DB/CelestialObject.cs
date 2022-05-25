using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE_DB
{
    public class CelestialObject
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public sbyte Status { get; set; }
        public DateOnly DateAdded { get; set; }
        public DateOnly? DateConfirmation { get; set; }
        public float? Mass { get; set; }
        public float? Radius { get; set; }
        public int? User { get; set; }
        public int? DetectionMethod { get; set; }
        public int? Type { get; set; }
        public int? Discoverer { get; set; }

        public virtual Discoverer? DiscovererNavigation { get; set; }
        public virtual User? UserNavigation { get; set; }
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
        DateAdded,
        DateConfirmation,
        Mass,
        Radius,
        OrbitalRadius,
        User,
        DetectionMethod,
        Type,
        Discoverer,
    }
}
