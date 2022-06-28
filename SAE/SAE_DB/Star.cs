using System;
using System.Collections.Generic;

namespace SAE_DB
{
    public partial class Star : CelestialObject
    {
        public Star()
        {
            Exoplanes = new HashSet<Exoplanet>();
        }

        public virtual StarDetectionMethod? DetectionMethodNavigation { get; set; }
        public virtual StarType? TypeNavigation { get; set; }
        public virtual ICollection<Exoplanet> Exoplanes { get; set; }

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
            if (detectionMethod is not StarDetectionMethod and not null)
            {
                return;
            }

            base.SetDetectionMethod(detectionMethod);
            DetectionMethodNavigation = detectionMethod as StarDetectionMethod;
        }
        public override void SetType(NamedEntityWithByteId? type)
        {
            if (type is not StarType and not null)
            {
                return;
            }

            base.SetDetectionMethod(type);
            TypeNavigation = type as StarType;
        }
    }
}
