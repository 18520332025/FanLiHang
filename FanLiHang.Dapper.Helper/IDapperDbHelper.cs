using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace FanLiHang.Dapper.Helper
{
    public interface IDbHelper
    {
        bool Insert<T>(T t, string Connection = null);
        bool Insert<T>(T t, IDbTransaction dbTran);
        bool Update<T>(T t, string Connection = null);
        bool Update<T>(T t, IDbTransaction dbTran);
        bool Delete<T>(T t, string Connection = null);
        bool Delete<T>(T t, IDbTransaction dbTran);
        T GetModel<T>(T t, string Connection = null);
        IEnumerable<T> GetModelList<T>(string where, string Connection = null);
        PagerResultSet<T> GetModelList<T>(string sql, object pars, PagerParameter page, string Connection = null);
        PagerResultSet<T3> GetModelList<T1, T2, T3>(string sql, object pars, string splitOn, PagerParameter page, Func<T1, T2, T3> map, string Connection = null);
        PagerResultSet<T1> GetModelList<T1, T2>(string sql, object obj, PagerParameter page, string Connection = null);
        PagerResultSet<T> GetModelList<T>(string sql, object obj, PagerParameter pager, Func<System.Data.IDataReader, T> Transformation, string Connecton = null);
        IDataReader GetReader(string sql, object obj);
        IDbTransaction BeginTran();

        bool InsertAsTran<T>(T t);
        bool DeleteAsTran<T>(T t);
        bool UpdateAsTran<T>(T t);
        void CommitTran();
        void RollbackTran();
    }
}
