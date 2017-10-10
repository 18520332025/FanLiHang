using FanLiHang.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FanLiHang.Data
{
    public interface IUserPowerDataService
    {
        bool Insert(UserPower userPower);
        bool Update(UserPower userPower);
        bool Delete(UserPower userPower);
        IEnumerable<UserPower> GetList(int UserAuthorizationID);
        UserPower Get(int ID);
    }
}
