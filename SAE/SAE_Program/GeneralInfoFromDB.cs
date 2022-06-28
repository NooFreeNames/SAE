using Microsoft.EntityFrameworkCore;
using SAE_DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE_Program
{
    public static class GeneralInfoFromDB
    {
        static GeneralInfoFromDB()
        {
            CelestialObjectTypeList = new[] { typeof(Star), typeof(Exoplanet) };
            StatusList = new StatusEnum[] { StatusEnum.Confirmed, StatusEnum.NotConfirmed };
            Update();
        }

        public static void Update()
        {
            using var db = new SAEDBContext();
            db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            StarDetectionMethodList = db.StarDetectionMethods.ToArray();
            StarTypeList = db.StarTypes.ToArray();
            ExoplanetDetectionMethodList = db.ExoplanetDetectionMethods.ToArray();
            ExoplanetTypeList = db.ExoplanetTypes.ToArray();
            DiscovererList = db.Discoverers.ToArray();
        }

        public static Type[] CelestialObjectTypeList { get; set; }
        public static StatusEnum[] StatusList { get; set; }
        public static IEnumerable<ExoplanetDetectionMethod> ExoplanetDetectionMethodList { get; set; } = null!;
        public static IEnumerable<StarDetectionMethod> StarDetectionMethodList { get; set; } = null!;
        public static IEnumerable<ExoplanetType> ExoplanetTypeList { get; set; } = null!;
        public static IEnumerable<StarType> StarTypeList { get; set; } = null!;
        public static IEnumerable<Discoverer> DiscovererList { get; set; } = null!;
        //public static IEnumerable<NamedEntityWithByteId>? GetDetectionMethodListByType(Type type)
        //{
        //    if (type == typeof(Exoplanet) || type == typeof(ExoplanetDetectionMethod))
        //    {
        //        return ExoplanetDetectionMethodList;
        //    }
        //    else if (type == typeof(Star) || (type == typeof(StarDetectionMethod)))
        //    {
        //        return StarDetectionMethodList;
        //    }
        //    return null;
        //}
    }
}
