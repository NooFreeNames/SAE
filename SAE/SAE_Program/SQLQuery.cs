using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Data;
using SAE_DB;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SAE_Program.Pages;

namespace SAE_Program
{

    public abstract class NotifyPropertyChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }

    static class QueryConverter
    {
        public static SQLQuery ToSQLQuery(string searchString, uint pageNum, SearchFiltrs filters)
        {

            var tableName = filters.Type.ToString();

            string detectionMethodQuery = $"(SELECT Name FROM {tableName}_Detection_Method WHERE {tableName}.DetectionMethod = {tableName}_Detection_Method.Id)";
            string typeQuery = $"(SELECT Name FROM {tableName}_Type WHERE {tableName}.Type = {tableName}_Type.Id)";
            string discovererQuery = $"(SELECT Name FROM Discoverer WHERE {tableName}.Discoverer = Discoverer.Id)";

            string searchBy;
            if (filters.SearchBy == CelestialObjectPropsEnum.DetectionMethod)
            {
                searchBy = detectionMethodQuery;
            }
            else if (filters.SearchBy == CelestialObjectPropsEnum.Type)
            {
                searchBy =  typeQuery;
            }
            else if (filters.SearchBy == CelestialObjectPropsEnum.Discoverer)
            {
                searchBy = discovererQuery;
            }
            else
            {
                searchBy = filters.SearchBy.ToString();
            }

            string? orderBy;
            if (filters.OrderBy == CelestialObjectPropsEnum.DetectionMethod)
            {
                orderBy = detectionMethodQuery;
            }
            else if (filters.OrderBy == CelestialObjectPropsEnum.Type)
            {
                orderBy = typeQuery;
            }
            else
            {
                orderBy = filters.OrderBy.ToString();
            }


            string? where = null;
            if (searchString == "-")
            {
                where =  $"{searchBy} IS NULL";
            }
            else if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString
                    .Replace(@"\", @"\\")
                    .Replace("'", @"\'");
                where = $"LOCATE('{searchString}', {searchBy}) != 0";
                orderBy = $"LOCATE('{searchString}', {searchBy}), {orderBy}";
            }

            string? limit = null;
            if (pageNum > 0)
            {
                limit = $"{filters.RowCount * (pageNum - 1)}, {filters.RowCount}";
            }
            
            return new SQLQuery(tableName, where, orderBy, limit);
        }
    }


    internal class SQLQuery
    {
        public SQLQuery(string tableName, string? whereStr, string? orderByStr, string? limitStr)
        {
            this.tableName = tableName;
            this.whereStr = whereStr is null ? string.Empty : "WHERE " + whereStr;
            this.orderByStr = orderByStr is null ? string.Empty : "ORDER BY " + orderByStr;
            this.limitStr = limitStr is null ? string.Empty : "LIMIT " + limitStr;
        }

        readonly string tableName;
        readonly string whereStr;
        readonly string orderByStr;
        readonly string limitStr;


        public string ToSQLString(SQLQueryParams parameters)
        {
            var sqlStr = "SELECT * FROM " + tableName + ' ';
            if ((parameters & SQLQueryParams.WhereStr) != 0)
            {
                sqlStr += whereStr + ' ';
            }
            if ((parameters & SQLQueryParams.OrderByStr) != 0)
            {
                sqlStr += orderByStr + ' ';
            }
            if ((parameters & SQLQueryParams.LimitStr) != 0)
            {
                sqlStr += limitStr + ' ';
            }
 
            return sqlStr;
        }
    }
    

    public enum SQLQueryParams : byte
    {
        None = 0,
        WhereStr = 1 << 0,
        OrderByStr = 1 << 1,
        LimitStr = 1 << 2,
        All = WhereStr | OrderByStr | LimitStr,
    }
}
