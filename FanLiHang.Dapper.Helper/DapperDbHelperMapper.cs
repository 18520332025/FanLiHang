
using FanLiHang.Dapper.ModelData.Attribute;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FanLiHang.Dapper.Helper
{
    public class DapperDbHelperMapper
    {
        public IMemoryCache _cache { get; set; }
        public DapperDbHelperMapper(IMemoryCache cache)
        {
            _cache = cache;
        }

        public void RegisterAssemblyTypes(Assembly assembly)
        {
            Type[] types = assembly.GetTypes();
            foreach (var type in types)
            {
                RegisterType(type);
            }
        }

        public void RegisterType(Type type)
        {
            _cache.Set<bool>(GetRegionLable(type), true);
            if (type.IsClass)
            {
                if (TableAttrbuteHelper.GetTableName(type) == null)
                {
                    return;
                }
                string key = GetKeyID(type);
                DapperBaseSQL baseSql = _cache.Get<DapperBaseSQL>(key);
                DapperBaseSQL dapperBaseSQL = GetDapperBaseSQL(type);
                if (baseSql != null)
                {
                    baseSql = dapperBaseSQL;
                }
                else
                {
                    _cache.Set<DapperBaseSQL>(key, dapperBaseSQL);
                }
            }

        }

        public DapperBaseSQL GetDapperBaseSQLAtCache<T>()
        {
            Type type = typeof(T);
            if (!_cache.Get<bool>(GetRegionLable(type)))
            {
                RegisterType(type);
            }
            return _cache.Get<DapperBaseSQL>(GetKeyID(type));
        }

        public DapperBaseSQL GetDapperBaseSQL(Type type)
        {
            DapperSqlHelper dapperSql = new DapperSqlHelper();
            DataTableConfig config = new DataTableConfig(type);
            DapperBaseSQL dapperBaseSQL = new DapperBaseSQL
            {
                Get = dapperSql.GetDarpperModelSql(config),
                GetList = dapperSql.GetDarpperModelListSql(config) + " where 1=1",
                Insert = dapperSql.GetDarpperInsertSql(config),
                Update = dapperSql.GetDarpperUpdateSql(config),
                Delete = dapperSql.GetDarpperDeleteSql(config),
                TableConifg = config
            };
            return dapperBaseSQL;
        }

        public string GetKeyID(Type t)
        {
            return "dapperSqlAs" + t.GUID;
        }

        public string GetRegionLable(Type t)
        {
            return "regionDapper" + t.GUID;
        }
    }
}
