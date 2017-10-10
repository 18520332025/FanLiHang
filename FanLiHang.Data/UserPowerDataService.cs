using FanLiHang.Dapper.Helper;
using FanLiHang.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace FanLiHang.Data
{
    public class UserPowerDataService : IUserPowerDataService
    {
        IDbHelper _dbHelper;
        IRoleDataService roleDataService;
        public UserPowerDataService(IDbHelper dbHelper, IRoleDataService roleDataService)
        {
            _dbHelper = dbHelper;
            this.roleDataService = roleDataService;
        }
        public bool Delete(UserPower userPower)
        {
            return _dbHelper.Delete<UserPower>(userPower);
        }

        public IEnumerable<UserPower> GetList(int UserAuthorizationID)
        {
            var list = _dbHelper.GetModelList<UserPower>(" and UserAuthorizationID=" + UserAuthorizationID);
            var roleList = roleDataService.GetListByCatch();
            foreach (var item in list)
            {
                item.Role = roleList.Where(x => x.ID == item.RoleID).First();
            }
            return list;
        }

        public UserPower Get(int ID)
        {
            return _dbHelper.GetModel<UserPower>(new UserPower { ID = ID });
        }

        public bool Insert(UserPower userPower)
        {
            return _dbHelper.Insert<UserPower>(userPower);
        }

        public bool Update(UserPower userPower)
        {
            return _dbHelper.Update<UserPower>(userPower);
        }
    }
}
