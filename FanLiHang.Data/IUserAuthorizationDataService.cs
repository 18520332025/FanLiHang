using FanLiHang.Dapper.Helper;
using FanLiHang.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FanLiHang.Data
{
    public interface IUserAuthorizationDataService
    {
        UserAuthorization GetAuthorization(int userID);
        PagerResultSet<UserAuthorization> GetList(int userID, PagerParameter pager);
        IEnumerable<UserAuthorization> GetListAtCache();
        List<FunctionPower> GetPowers(int ID, SystemCode systemCode);
        UserAuthorization Get(int authorizationID);
        bool Insert(UserAuthorization userAuthorization);
        bool Update(UserAuthorization userAuthorization);

    }
}
