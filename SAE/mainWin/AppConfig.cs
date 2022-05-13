using System;
using System.Configuration;
using System.Collections.Specialized;
using System.Globalization;
using System.Windows;

namespace mainWin
{
    internal static class AppConfig
    {
        static AppConfig()
        {
            config = ConfigurationManager.AppSettings;
        }
        public static readonly NameValueCollection config;
        
        public static string? ConnectionString => config.Get("ConnectionString");
        public static string? ServerVersion => config.Get("ServerVersion");

        public static double ScreenWidth => SystemParameters.PrimaryScreenWidth;
        public static double ScreenHeight => SystemParameters.PrimaryScreenHeight;
        public static double WinSizeCoeff { 
            get 
            {
                const double defaultCoeff = 0.5f;
                if (double.TryParse(config.Get("WinSizeCoeff"), out var number) && number > 0)
                {
                    return number;
                }
                return defaultCoeff;
            } 
        }

        public static class SearchFilters
        {
            public static CosmicBodyEnum Type => CosmicBodyEnum.Exoplanet;
            public static CosmicBodyPropEnum OrderBy => CosmicBodyPropEnum.Type;
            public static CosmicBodyPropEnum SearchBy => CosmicBodyPropEnum.DateAdded;
        }
    }

    

    enum CosmicBodyEnum
    {
        Galaxy,
        Star,
        Exoplanet,
        Asteroid,
    }

    enum CosmicBodyPropEnum
    {
        Id,
        Name,
        Status,
        DateAdded,
        DateConfirmation,
        Mass,
        Radius,
        DetectionMethod,
        Type,
    }
}
