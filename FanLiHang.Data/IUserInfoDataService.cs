using System;
using System.Collections.Generic;
using System.Text;
using FanLiHang.Model;
using FanLiHang.Dapper.Helper;

namespace FanLiHang.Data
{
    public interface IUserInfoDataService
    {
        bool Add(UserInfo userInfo);
        bool Update(UserInfo userInfo);
        IEnumerable<UserInfo> GetList();
        PagerResultSet<UserInfo> GetPager(PagerParameter pager);
        IEnumerable<UserInfo> GetListAtCache();
        UserInfo Get(int UserID);
        bool Delete(UserInfo userInfo);
    }
}
