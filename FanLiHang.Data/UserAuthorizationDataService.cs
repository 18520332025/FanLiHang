using System;
using System.Collections.Generic;
using System.Text;
using FanLiHang.Model;
using FanLiHang.Dapper.Helper;
using System.Linq;
using System.Linq.Expressions;
namespace FanLiHang.Data
{
    public class UserAuthorizationDataService : IUserAuthorizationDataService
    {
        IDbHelper _dbHelper;
        IUserInfoDataService userInfoDataService;
        public UserAuthorizationDataService(IDbHelper dbHelper, IUserInfoDataService userInfoDataService)
        {
            this.userInfoDataService = userInfoDataService;
            _dbHelper = dbHelper;
        }

        public UserAuthorization GetAuthorization(int userID)
        {
            var list = _dbHelper.GetModelList<UserAuthorization>(" and UserID=" + userID);
            return list.FirstOrDefault();
        }

        public UserAuthorization Get(int authorizationID)
        {
            return _dbHelper.GetModel<UserAuthorization>(new UserAuthorization { ID = authorizationID });
        }



        public List<FunctionPower> GetPowers(int userAuthorizationID, SystemCode systemCode)
        {
            string sql = @"
                            Select FunctionPower.* from FunctionPower 
                                Join RoleFunctionPower on RoleFunctionPower.FunctionPowerID=FunctionPower.ID
                                join [Role] on [Role].ID = RoleFunctionPower.RoleID
                                join UserPower on UserPower.RoleID = [Role].ID
                                join UserAuthorization on UserAuthorization.ID = UserPower.UserAuthorizationID
                                where UserAuthorization.ID=@UserAuthorizationID and FunctionPower.AppInfoID = @SystemCode
                                union all
                                select FunctionPower.* from FunctionPower
                                join DepartmentFunctionPower on DepartmentFunctionPower.FunctionPowerID = FunctionPower.ID 
                                join Department on Department.ID = DepartmentFunctionPower.DepartmentID
                                join [Role] on [Role].DepartmentID = Department.ID 
                                join UserPower on UserPower.RoleID = [Role].ID
                                join UserAuthorization on UserAuthorization.ID = UserPower.UserAuthorizationID 
                                where UserAuthorization.ID=@UserAuthorizationID and FunctionPower.AppInfoID = @SystemCode
                        ";
            return _dbHelper.GetModelList<FunctionPower>(sql, new { UserAuthorizationID = userAuthorizationID, SystemCode = (int)systemCode },
                new PagerParameter
                {
                    PageIndex = 1,
                    PageSize = 99999999,
                    OrderBy = "ID"
                }).Rows;
        }

        public PagerResultSet<UserAuthorization> GetList(int userID,PagerParameter pager)
        {
            string sql = @" SELECT 
	                                UserAuthorization.*,
	                                UserInfo.ID as UID,
	                                UserInfo.[Name] as UName,
	                                UserInfo.Phone as UPhone,
	                                UserInfo.Cornet as UCornet,
	                                UserInfo.EMail  as UEmail,
	                                UserInfo.QQ as UQQ,
	                                UserInfo.Address as UAddress,
	                                UserInfo.Domicile as UDomicile,
	                                UserInfo.PhotoUrl as UPhotoUrl,
	                                AppInfo.ID as AID,
	                                AppInfo.Name as AName,
	                                AppInfo.Code as ACode,
	                                Department.ID as DID,
	                                Department.CreateBy as DCreateBy,
	                                Department.CreateDate as DCreateDate,
	                                Department.Deleted as DDeleted,
	                                Department.Manager as DManager,
	                                Department.Name as DName
	
                                FROM UserAuthorization
                                join UserInfo on UserInfo.ID = UserAuthorization.UserID
                                join AppInfo on AppInfo.ID = UserAuthorization.AppInfoID
                                join Department on Department.ID = UserAuthorization.DepartmentID ";
            if (userID != 0)
            {
                sql += " where UserInfo.ID=" + userID;
            }
            return _dbHelper.GetModelList<UserAuthorization>(sql, null,pager, dr => {
                UserAuthorization userAuthorization = new UserAuthorization();
                userAuthorization.ID = int.Parse(dr["ID"].ToString());
                userAuthorization.AppInfo = new AppInfo();
                userAuthorization.AppInfoID = int.Parse(dr["AppInfoID"].ToString());
                userAuthorization.AppInfo.Code = dr["ACode"].ToString();
                userAuthorization.AppInfo.Name = dr["AName"].ToString();
                userAuthorization.AppInfo.ID = userAuthorization.AppInfoID;
                userAuthorization.Department = new Department();
                userAuthorization.DepartmentID = (int)dr["DepartmentID"];
                userAuthorization.Department.ID = userAuthorization.DepartmentID;
                userAuthorization.Department.Name = dr["DName"].ToString();
                userAuthorization.Department.Manager = (int)dr["DManager"];
                userAuthorization.Department.Deleted = (bool)dr["DDeleted"];
                userAuthorization.Department.CreateBy = (int)dr["DCreateBy"];
                userAuthorization.Department.CreateDate = (DateTime)dr["DCreateDate"];
                userAuthorization.User = new UserInfo();
                userAuthorization.UserID = (int)dr["UserID"];
                userAuthorization.User.ID = userAuthorization.UserID;
                userAuthorization.User.Name = (string)dr["UName"];
                userAuthorization.User.Phone = (string)dr["UPhone"];
                userAuthorization.User.Cornet = (string)dr["UCornet"];
                userAuthorization.User.Email = (string)dr["UEmail"];
                userAuthorization.User.QQ = (string)dr["UQQ"];
                userAuthorization.User.Address = (string)dr["UAddress"];
                userAuthorization.User.Domicile = (string)dr["UDomicile"];
                userAuthorization.User.PhotoUrl = (string)dr["UPhotoUrl"];
                userAuthorization.LoginID = (string)dr["LoginID"];
                userAuthorization.Password = (string)dr["Password"];
                return userAuthorization;
            });
        }

        public IEnumerable<UserAuthorization> GetListAtCache()
        {
            return _dbHelper.GetModelList<UserAuthorization>(null);
        }

        public bool Insert(UserAuthorization userAuthorization)
        {
            return _dbHelper.Insert(userAuthorization);
        }

        public bool Update(UserAuthorization userAuthorization)
        {
            return _dbHelper.Update(userAuthorization);
        }
    }
}
