using System;
using System.Collections.Generic;
using System.Text;
using FanLiHang.Model;
using FanLiHang.Dapper.Helper;

namespace FanLiHang.Data
{
    public class UserInfoDataService : IUserInfoDataService
    {
        IDbHelper _dbHelper;
        public UserInfoDataService(IDbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
        public bool Add(UserInfo userInfo)
        {
            return _dbHelper.Insert(userInfo);
        }

        public bool Delete(UserInfo userInfo)
        {
            return _dbHelper.Delete(userInfo);
        }

        public UserInfo Get(int UserID)
        {
            return _dbHelper.GetModel(new UserInfo { ID = UserID });
        }

        public IEnumerable<UserInfo> GetList()
        {
            return _dbHelper.GetModelList<UserInfo>(null);
        }

        public IEnumerable<UserInfo> GetListAtCache()
        {
            return GetList();
        }

        public PagerResultSet<UserInfo> GetPager(PagerParameter pager)
        {
            return _dbHelper.GetModelList<UserInfo>("select * from UserInfo", null, pager);
        }

        public bool Update(UserInfo userInfo)
        {
            return _dbHelper.Update(userInfo);
        }
    }
}
