using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
namespace FanLiHang.Dapper.ModelData.Attribute
{

    public class ColumnConfig
    {
        public string Name;
        public string Alias;
    }

    public class DataTableConfig
    {
        public string TableName
        {
            get;
            set;
        }

        public ColumnConfig[] Column
        {
            get;
            set;
        }
        public ColumnConfig Key
        {
            get;
            set;
        }

        public DataTableConfig(Type t)
        {
            this.TableName = TableAttrbuteHelper.GetTableName(t);
            Type ttype = t;
            var properties = ttype.GetProperties();
            List<ColumnConfig> columns = new List<ColumnConfig>();
            foreach (var pro in properties)
            {
                var attributes = pro.GetCustomAttributes<Column>();
                if (pro.PropertyType.IsPrimitive
                    || pro.PropertyType == typeof(DateTime)
                    || pro.PropertyType == typeof(Guid)
                    || pro.PropertyType == typeof(String))

                {
                    if (attributes.Count() != 0)
                    {
                        Column column = (Column)attributes.First();
                        string columnName = pro.Name;
                        ColumnConfig columnConfig = new ColumnConfig();
                        if (!string.IsNullOrEmpty(column.Alias))
                        {
                            columnConfig.Alias = column.Alias;
                        }
                        if (!column.Key && !column.NoDataColumn)
                        {
                            columnConfig.Name = columnName;
                            columns.Add(columnConfig);
                        }
                        else if (column.Key)
                        {
                            Key = new ColumnConfig { Name = columnName, Alias = column.Alias };
                        }
                    }
                    else
                    {
                        string columnName = pro.Name;
                        columns.Add(new ColumnConfig { Name = columnName });
                    }
                }
            }
            this.Column = columns.ToArray();
        }
    }
}
