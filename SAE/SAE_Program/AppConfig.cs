using System;
using System.Configuration;
using System.Collections.Specialized;
using System.Globalization;
using System.Windows;
using SAE_DB;

namespace SAE_Program
{
    public static class SearchFilters
    {
        public static CelestialObjectEnum Type => CelestialObjectEnum.Star;
        public static CelestialObjectPropsEnum OrderBy => CelestialObjectPropsEnum.Id;
        public static CelestialObjectPropsEnum SearchBy => CelestialObjectPropsEnum.Name;
    }
}
