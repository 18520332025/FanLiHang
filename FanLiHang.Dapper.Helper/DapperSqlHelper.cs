using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using FanLiHang.Dapper.ModelData.Attribute;
namespace FanLiHang.Dapper.Helper
{
    public class DapperSqlHelper
    {
        public DapperSqlHelper()
        {
        }


        public string GetDarpperDeleteSql(DataTableConfig config)
        {
            string delete = " delete " + config.TableName + " where +" + config.Key.Name + "=@" + (config.Key.Alias ?? config.Key.Name);
            return delete;
        }

        public string GetDarpperInsertSql(DataTableConfig config)
        {
            string insert = "insert into [" + config.TableName + "] (";

            foreach (var column in config.Column)
            {
                insert += "[" + (column.Alias ?? column.Name) + "],";
            }
            insert = insert.Trim(',') + ")Values(";
            foreach (var column in config.Column)
            {
                insert += "@" + column.Name + ",";
            }
            insert = insert.Trim(',') + ");select @@IDENTITY;";
            return insert;
        }

        public string GetDarpperUpdateSql(DataTableConfig config)
        {
            string update = "update [" + config.TableName + "] set ";
            foreach (var column in config.Column)
            {
                update += "[" + (column.Alias ?? column.Name) + "]=@" + column.Name + ",";
            }
            update = update.Trim(',');
            update += " where  " + (config.Key.Alias ?? config.Key.Name) + "=@" + (config.Key.Alias ?? config.Key.Name);
            return update;
        }

        public string GetDarpperModelSql(DataTableConfig config)
        {
            string selectSqlFormat = " select {0} from [" + config.TableName + "] where [" + config.Key.Name + "]=@" + config.Key.Name;
            string columnsSql = config.Key.Name + ",";
            foreach (var column in config.Column)
            {
                if (!string.IsNullOrEmpty(column.Alias))
                {
                    columnsSql += column.Alias + " as " + column.Name;
                }
                else
                {
                    columnsSql += column.Name;
                }
                columnsSql += ",";
            }
            string selectSql = string.Format(selectSqlFormat, columnsSql.Trim(','));
            return selectSql;
        }

        public string GetDarpperModelListSql(DataTableConfig config)
        {
            string selectSqlFormat = " select {0} from [" + config.TableName + "]";
            string columnsSql = "[" + config.Key.Name + "],";
            foreach (var column in config.Column)
            {
                if (!string.IsNullOrEmpty(column.Alias))
                {
                    columnsSql += "[" + column.Alias + "] as " + column.Name;
                }
                else
                {
                    columnsSql += "[" + column.Name + "]";
                }
                columnsSql += ",";
            }
            string selectSql = string.Format(selectSqlFormat, columnsSql.Trim(','));
            return selectSql;
        }

        public string GetPager(string sql, PagerParameter parameter)
        {
            string pagerSql = "SELECT * from(select DENSE_RANK() OVER (ORDER by " + parameter.OrderBy + (parameter.Desc ? " desc" : "") + " ) as num,z.* from (" + sql + ")z )t where t.num>";
            pagerSql += (parameter.PageIndex - 1) * parameter.PageSize + " and t.num<=" + (parameter.PageIndex) * parameter.PageSize;
            return pagerSql;
        }

        public string GetCount(string sql, PagerParameter parameter)
        {
            string countSql = " select count(*) from (select " + parameter.OrderBy + " from (" + sql + ")as t1 group by " + parameter.OrderBy + ") as t2";
            return countSql;
        }

    }

}
