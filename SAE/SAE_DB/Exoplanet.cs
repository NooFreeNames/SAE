using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SAE_DB
{
    public partial class Exoplanet : CelestialObject
    {
        public Exoplanet()
        {
            Stars = new HashSet<Star>();
        }

        

        public float? OrbitalRadius { get; set; }
        public virtual ExoplanetDetectionMethod? DetectionMethodNavigation { get; set; }
        public virtual ExoplanetType? TypeNavigation { get; set; }
        public virtual ICollection<Star> Stars { get; set; }

        public override NamedEntityWithByteId? GetDetectionMethod()
        {
            return DetectionMethodNavigation;
        }

        public override NamedEntityWithByteId? Get_Type()
        {
            return TypeNavigation;
        }

        public override void SetDetectionMethod(NamedEntityWithByteId? detectionMethod)
        {
            if (detectionMethod is not ExoplanetDetectionMethod and not null)
            {
                return;
            }

            base.SetDetectionMethod(detectionMethod);
            DetectionMethodNavigation = detectionMethod as ExoplanetDetectionMethod;
        }
        public override void SetType(NamedEntityWithByteId? type)
        {
            if (type is not ExoplanetType and not null)
            {
                return;
            }

            base.SetDetectionMethod(type);
            TypeNavigation = type as ExoplanetType;
        }
    }
}
