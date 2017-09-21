
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using FanLiHang.Dapper.ModelData.Attribute;
using Dapper;
using System.Data;
using System.Linq.Expressions;
using Microsoft.Extensions.Caching.Memory;

namespace FanLiHang.Dapper.Helper
{
    public class DbHelper : IDbHelper
    {
        private IMemoryCache _cache;
        private IDBConnectionStringConfig _connectionStringConfig;
        private IDbConnection _dbConnection;
        public DbHelper(IMemoryCache cache, IDBConnectionStringConfig connectionStringConfig, IDbConnection dbConnection)
        {
            this._cache = cache;
            this._connectionStringConfig = connectionStringConfig;
            this._dbConnection = dbConnection;
            this._dbConnection.ConnectionString = _connectionStringConfig.DefaultConnectionString;

        }
        DapperBaseSQL baseSql;

        private void extend(string Connection)
        {
            if (!string.IsNullOrEmpty(Connection))
            {
                _dbConnection.ConnectionString = Connection;
            }
        }

        private void setBaseSql<T>()
        {
            if (this.baseSql == null)
            {
                DapperDbHelperMapper dapperDbHelperMapper = new DapperDbHelperMapper(_cache);
                if (_cache != null)
                {
                    this.baseSql = dapperDbHelperMapper.GetDapperBaseSQLAtCache<T>();
                }
                else
                {
                    this.baseSql = dapperDbHelperMapper.GetDapperBaseSQL(typeof(T));
                }
            }
        }

        public bool Insert<T>(T t, string Connection = null)
        {
            setBaseSql<T>();
            extend(Connection);
            string insert = baseSql.Insert;
            try
            {
                Type type = typeof(T);
                int newID = _dbConnection.ExecuteScalar<int>(insert, t);
                type.GetProperty(baseSql.TableConifg.Key.Name).SetValue(t, newID, null);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Insert<T>(T t, IDbTransaction dbTran)
        {
            setBaseSql<T>();
            try
            {
                string insert = baseSql.Insert;
                Type type = typeof(T);
                int newID = dbTran.Connection.ExecuteScalar<int>(insert, t, dbTran);
                type.GetProperty(baseSql.TableConifg.Key.Name).SetValue(t, newID, null);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update<T>(T t, string Connection = null)
        {
            setBaseSql<T>();
            extend(Connection);
            try
            {
                _dbConnection.ExecuteScalar(baseSql.Update, t);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update<T>(T t, IDbTransaction dbTran)
        {
            setBaseSql<T>();
            try
            {
                dbTran.Connection.ExecuteScalar(baseSql.Update, t, dbTran);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Delete<T>(T t, string Connection = null)
        {
            extend(Connection);
            setBaseSql<T>();
            try
            {
                _dbConnection.ExecuteScalar(baseSql.Delete, t);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Delete<T>(T t, IDbTransaction dbTran)
        {
            setBaseSql<T>();
            try
            {
                dbTran.Connection.ExecuteScalar(baseSql.Delete, t, dbTran);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public T GetModel<T>(T t, string Connection = null)
        {
            setBaseSql<T>();
            extend(Connection);
            IEnumerable<T> enumerable = _dbConnection.Query<T>(baseSql.Get, t);
            List<T> list = new List<T>(enumerable);
            if (list.Count != 0)
                return list[0];
            else
                return default(T);
        }

        public IEnumerable<T> GetModelList<T>(string where, string Connection = null)
        {
            setBaseSql<T>();
            extend(Connection);
            return _dbConnection.Query<T>(baseSql.GetList + where);
        }

        public PagerResultSet<T> GetModelList<T>(string sql, object pars, PagerParameter pager, string Connection = null)
        {
            setBaseSql<T>();
            extend(Connection);
            DapperSqlHelper dsh = new DapperSqlHelper();
            string sqlPager = dsh.GetPager(sql, pager);
            string sqlCount = dsh.GetCount(sql, pager);

            int count = _dbConnection.ExecuteScalar<int>(sqlCount, pars);
            List<T> rs = new List<T>(_dbConnection.Query<T>(sqlPager, pars));
            PagerResultSet<T> prs = new PagerResultSet<T>(rs, count, pager);
            return prs;
        }

        public PagerResultSet<T3> GetModelList<T1, T2, T3>(string sql, object pars, string splitOn, PagerParameter pager, Func<T1, T2, T3> map, string Connection = null)
        {
            extend(Connection);
            DapperSqlHelper dsh = new DapperSqlHelper();
            string sqlPager = dsh.GetPager(sql, pager);
            string sqlCount = dsh.GetCount(sql, pager);

            int count = _dbConnection.ExecuteScalar<int>(sqlCount, pars);
            List<T3> rs = new List<T3>(_dbConnection.Query<T1, T2, T3>(sqlPager, map, pars, null, false, splitOn));
            PagerResultSet<T3> prs = new PagerResultSet<T3>(rs, count, pager);
            return prs;
        }

        public PagerResultSet<T> GetModelList<T>(string sql, object pars, PagerParameter pager, Func<System.Data.IDataReader, T> Transformation, string Connection = null)
        {
            extend(Connection);
            DapperSqlHelper dsh = new DapperSqlHelper();
            string sqlPager = dsh.GetPager(sql, pager);
            string sqlCount = dsh.GetCount(sql, pager);
            int count = _dbConnection.ExecuteScalar<int>(sqlCount, pars);
            var reader = _dbConnection.ExecuteReader(sql, pars);
            List<T> list = new List<T>();
            while (reader.Read())
            {
                list.Add(Transformation(reader));
            }
            PagerResultSet<T> prs = new PagerResultSet<T>(list, count, pager);
            return prs;
        }

        public PagerResultSet<T1> GetModelList<T1, T2>(string sql, object obj, PagerParameter page, string Connection = null)
        {
            extend(Connection);
            ManyToOneConfigs configs = new ManyToOneConfigs(_cache);
            var manyToOne = configs.GetConfig<T1, T2>();
            Dictionary<object, T1> dicts = new Dictionary<object, T1>();
            var list = GetModelList<T1, T2, T1>(sql, obj, manyToOne.ManyName, page, (x, y) =>
            {
                T1 to;
                object key = manyToOne.GetKeyFunc(x);
                if (!dicts.TryGetValue(key, out to))
                {
                    dicts.Add(key, to = x);
                }
                List<T2> data = manyToOne.GetListFunc(dicts[key]);
                if (y != null)
                {
                    data.Add(y);
                }
                return to;
            }, Connection);
            return new PagerResultSet<T1>(new List<T1>(dicts.Values), list.Pager);
        }

        public IDataReader GetReader(string sql, object obj)
        {
            return _dbConnection.ExecuteReader(sql, obj);
        }
    }
}
