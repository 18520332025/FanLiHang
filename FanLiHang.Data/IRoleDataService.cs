using FanLiHang.Dapper.Helper;
using FanLiHang.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FanLiHang.Data
{
    public interface IRoleDataService
    {
        PagerResultSet<Role> GetList(int departmentID, PagerParameter pageParameter);
        bool Add(Role role);
        bool Update(Role role);
        Role Get(int id);
        IEnumerable<Role> GetList();
        IEnumerable<Role> GetListByCatch();
        List<RoleFunctionPower> GetPowers(int? roleID, int? appInfoID);
        void UpdateFunctionPowers(int RoleID, int AppInfoID, int[] functionPowerIDList);
    }
}
