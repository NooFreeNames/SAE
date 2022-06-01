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

namespace SAE_Program
{

    public abstract class NotifyPropertyChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }

    public class SearchFiltres : NotifyPropertyChanged
    {
        public SearchFiltres()
        {
            Type = CelestialObjectEnum.Exoplanet;
            OrderBy = CelestialObjectPropsEnum.Id;
            SearchBy = CelestialObjectPropsEnum.Name;
            RowCount = 50;
        }


        const uint minRowCount = 1u;
        uint rowCount;
        CelestialObjectEnum type;
        CelestialObjectPropsEnum orderBy;
        CelestialObjectPropsEnum searchBy;

        public uint RowCount
        {
            get { return rowCount; }
            set
            {
                rowCount = value <= minRowCount ? minRowCount : value;
                OnPropertyChanged(nameof(RowCount));
            }
        }
        public CelestialObjectEnum Type
        {
            get { return type; }
            set 
            { 
                type = value;
                OnPropertyChanged(nameof(Type));
            }
        }
        public CelestialObjectPropsEnum OrderBy
        {
            get { return orderBy; }
            set
            {
                orderBy = value;
                OnPropertyChanged(nameof(OrderBy));
            }
        }
        public CelestialObjectPropsEnum SearchBy
        {
            get { return searchBy; }
            set
            {
                searchBy = value;
                OnPropertyChanged(nameof(SearchBy));
            }
        }
    }

    public class SearchQuery : NotifyPropertyChanged
    {

        public SearchQuery(string searchString, uint pageNum, SearchFiltres filtres) 
        {
            SearchString = searchString;
            PageNum = pageNum;
            Filtres = filtres;
        }

        
        

        public string searchString = null!;
        public uint pageNum;
        
        public SearchFiltres filtres = null!;
        

        public string SearchString
        {
            get { return searchString; }
            set 
            { 
                searchString = value;
                OnPropertyChanged(nameof(SearchString));
            }
        }

        public uint PageNum
        {
            get { return pageNum; }
            set
            {
                pageNum = value;
                OnPropertyChanged(nameof(PageNum));
            }
        }

        public SearchFiltres Filtres
        {
            get { return filtres; }
            set
            {
                filtres = value;
                OnPropertyChanged(nameof(Filtres));
            }
        }
    }

    static class QueryConverter
    {
        public static SQLQuery ToSQLQuery(SearchQuery searchQuery)
        {

            var tableName = searchQuery.filtres.Type.ToString();

            string detectionMethodQuery = $"(SELECT Name FROM {tableName}_Detection_Method WHERE {tableName}.DetectionMethod = {tableName}_Detection_Method.Id)";
            string typeQuery = $"(SELECT Name FROM {tableName}_Type WHERE {tableName}.Type = {tableName}_Type.Id)";

            string searchBy;
            if (searchQuery.filtres.SearchBy == CelestialObjectPropsEnum.DetectionMethod)
            {
                searchBy = detectionMethodQuery;
            }
            else if (searchQuery.filtres.SearchBy == CelestialObjectPropsEnum.Type)
            {
                searchBy =  typeQuery;
            }
            else
            {
                searchBy = searchQuery.filtres.SearchBy.ToString();
            }

            string? orderBy;
            if (searchQuery.filtres.OrderBy == CelestialObjectPropsEnum.DetectionMethod)
            {
                orderBy = detectionMethodQuery;
            }
            else if (searchQuery.filtres.OrderBy == CelestialObjectPropsEnum.Type)
            {
                orderBy = typeQuery;
            }
            else
            {
                orderBy = searchQuery.filtres.OrderBy.ToString();
            }
            string searchString = searchQuery.searchString
                .Replace(@"\", @"\\")
                .Replace("'", @"\'");
            string? where = null;
            if (searchString == "-")
            {
                where =  $"{searchBy} IS NULL";
            }
            else if (!string.IsNullOrEmpty(searchQuery.searchString))
            {
                where = $"LOCATE('{searchString}', {searchBy}) != 0";
                orderBy = $"LOCATE('{searchString}', {searchBy}), {orderBy}";
            }

            string? limit = null;
            if (searchQuery.PageNum > 0)
            {
                limit = $"{searchQuery.Filtres.RowCount * (searchQuery.PageNum - 1)}, {searchQuery.Filtres.RowCount}";
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
