using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace FanLiHang.Dapper.Helper
{
    public interface IManyToOneConfigs
    {
        void AddConfig<T1, T2>(ManyToOne<T1, T2>.GetKey<T1> getKey, ManyToOne<T1, T2>.GetList<T1, T2> getList, Expression<ManyToOne<T1, T2>.GetName<T2>> GetManyName);
        ManyToOne<T1, T2> GetConfig<T1, T2>();
    }
}
