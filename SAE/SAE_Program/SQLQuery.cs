using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SAE_DB;
namespace SAE_Program
{
    internal class SQLQuery
    {
        public SQLQuery(string text, uint rowCount)
        {
            this.text = text;
            this.rowCount = rowCount;
        }

        public string WhereSQLString => $"SELECT * FROM {TypeStr} {WhereStr}";
        public string SQLString => $"SELECT * FROM {TypeStr} {WhereStr} {OrderByStr} {LinitStr}";
        uint rowCount;
        
        public static int ItemNum => 50;
        public static CelestialObjectEnum Type => CelestialObjectEnum.Exoplanet;
        public static CelestialObjectPropsEnum orderBy = CelestialObjectPropsEnum.Id;
        static CelestialObjectPropsEnum SearchBy = CelestialObjectPropsEnum.Name;

        string text = "";
        string searchByDM = $"(SELECT Name FROM {Type}_Detection_Method WHERE {Type}.DetectionMethod = {Type}_Detection_Method.Id)";
        string searchByTy = $"(SELECT Name FROM {Type}_Type WHERE {Type}.Type = {Type}_Type.Id)";

        string TypeStr => Type.ToString();

        string LinitStr
        {
            get
            {
                if (rowCount <= 0)
                {
                    return "";
                }
                return $"LIMIT {ItemNum * (rowCount - 1)}, {ItemNum}";
            }
        }

        string OrderByStr
        {
            get
            {
                if (orderBy == CelestialObjectPropsEnum.DetectionMethod)
                {
                    return "ORDER BY " + searchByDM;
                }
                else if (orderBy == CelestialObjectPropsEnum.Type)
                {
                    return "ORDER BY " + searchByTy;
                }
                else
                {
                    return "ORDER BY " + orderBy.ToString();
                }
            }
        }


        string SearchByStr
        {
            get
            {
                if (SearchBy == CelestialObjectPropsEnum.DetectionMethod)
                {
                    return searchByDM;
                }
                else if (SearchBy == CelestialObjectPropsEnum.Type)
                {
                    return searchByTy;
                }
                else
                {
                    return SearchBy.ToString();
                }
            }
        }
        string WhereStr
        {
            get
            {
                if (text == "")
                {
                    return "";
                }
                else if (text == "-")
                {
                    return $"WHERE {SearchByStr} IS NULL";
                } else
                {
                    return $"WHERE {SearchByStr} LIKE '{text}%'";
                }
            }
        }

        
    }
}
