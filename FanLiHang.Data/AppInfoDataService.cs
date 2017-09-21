using System;
using System.Collections.Generic;
using System.Text;
using FanLiHang.Model;
using FanLiHang.Dapper.Helper;

namespace FanLiHang.Data
{
    public class AppInfoDataService : IAppInfoDataService
    {
        IDbHelper _dbHelper;
        public AppInfoDataService(IDbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
        public IEnumerable<AppInfo> GetList()
        {
            return _dbHelper.GetModelList<AppInfo>(null);
        }
    }
}
