using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace FanLiHang.Dapper.Helper
{
    public class ManyToOneConfigs
    {
        private  IMemoryCache _cache;
        public ManyToOneConfigs(IMemoryCache cache)
        {
            this._cache = cache;
        }

        public  void AddConfig<T1, T2>(ManyToOne<T1, T2>.GetKey<T1> getKey, ManyToOne<T1, T2>.GetList<T1, T2> getList, Expression<ManyToOne<T1, T2>.GetName<T2>> GetManyName)
        {
            string Key = "mtc-" + typeof(T1).Name + "-" + typeof(T2).Name;
            _cache.Set<ManyToOne<T1, T2>>(Key, new ManyToOne<T1, T2>(getKey, getList, GetManyName));
        }

        public  ManyToOne<T1, T2> GetConfig<T1, T2>()
        {
            string Key = "mtc-" + typeof(T1).Name + "-" + typeof(T2).Name;
            return _cache.Get<ManyToOne<T1, T2>>(Key);
        }

    }
}
