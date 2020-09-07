using BraintreeSample.APIHelper.Entities;
using Pluralize.NET;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace BraintreeSample.APIHelper.Builders
{
    public class DapperQueryBuilder<T>
    where T : BaseEntity
    {
        public string table { get; set; }
        public DapperQueryBuilder()
        {
            CultureInfo culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            culture.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd HH:mm:ss";
            culture.DateTimeFormat.LongTimePattern = "";
            Thread.CurrentThread.CurrentCulture = culture;
            table = new Pluralizer().Pluralize(typeof(T).Name.ToLower(new CultureInfo("en-US", false)).Replace("entity", "").Replace("entities", ""));
        }
        public string CreateQuery(T obj)
        {
            var columnNames = new List<string>();
            var values = new List<string>();

            foreach (var item in obj.GetType().GetProperties())
            {
                var columnName = item.Name.ToLower(new CultureInfo("en-US", false));

                if (columnName.Equals("id")) continue;

                columnNames.Add(columnName);
                var value = item.GetValue(obj, null);
                if (columnName.Equals("guid")) value = Guid.NewGuid();
                if (value == null)
                {
                    values.Add($"null");
                }
                else
                {
                    if (columnName.Equals("createdat") || columnName.Equals("updatedat"))
                    {
                        values.Add($"'{DateTime.Now}'");
                    }
                    else
                    {
                        if (value is string || value is Guid || value is DateTime)
                        {
                            values.Add($"'{value.ToString().Replace("'", "''")}'");
                        }
                        else if (value is Enum)
                        {
                            values.Add($"{value.GetHashCode()}");
                        }
                        else
                        {
                            values.Add($"{value}");
                        }
                    }
                }
            }

            return $"insert into {table}({string.Join(", ", columnNames)}) values({string.Join(", ", values)}); select LAST_INSERT_ID();";
        }
        public string UpdateQuery(T obj)
        {
            var updateItems = new List<string>();

            foreach (var item in obj.GetType().GetProperties())
            {
                var columnName = item.Name.ToLower(new CultureInfo("en-US", false));

                if (columnName.Equals("id") || columnName.Equals("guid") || columnName.Equals("createdat") || columnName.Equals("createduserid")) continue;

                var value = item.GetValue(obj, null);
                if (value == null)
                {
                    updateItems.Add($"{columnName}=null");
                }
                else
                {
                    if (columnName.Equals("updatedat"))
                    {
                        updateItems.Add($"{columnName}='{DateTime.Now}'");
                    }
                    else
                    {
                        if (value is string || value is Guid || value is DateTime)
                        {
                            updateItems.Add($"{columnName}='{value.ToString().Replace("'", "''")}'");
                        }
                        else if (value is Enum)
                        {
                            updateItems.Add($"{columnName}={value.GetHashCode()}");
                        }
                        else
                        {
                            updateItems.Add($"{columnName}={value}");
                        }
                    }
                }
            }

            return $"update {table} set {string.Join(", ", updateItems)} where id={obj.ID} || guid='{obj.Guid}'";
        }
        public string DeleteQuery(int id)
        {
            return $"update {table} set isdeleted=true, isactive=false where id={id}";
        }
        public string DeleteQuery(Guid guid)
        {
            return $"update {table} set isdeleted=true, isactive=false where guid='{guid}'";
        }
        public string ReadQuery(int id)
        {
            return $"select * from {table} where id={id} and isactive=true and isdeleted=false";
        }
        public string ReadQuery(Guid guid)
        {
            return $"select * from {table} where guid='{guid}' and isactive=true and isdeleted=false";
        }
        public string ListQuery(int page, int pageSize)
        {
            return $"select * from {table} where isactive=true and isdeleted=false order by id limit {pageSize} offset {pageSize * page}";
        }
    }
}
