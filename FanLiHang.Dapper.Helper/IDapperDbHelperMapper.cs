using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace FanLiHang.Dapper.Helper
{
    public interface IDapperDbHelperMapper
    {
        void RegisterAssemblyTypes(Assembly assembly);
        DapperBaseSQL GetDapperBaseSQL<T>();

    }

}
