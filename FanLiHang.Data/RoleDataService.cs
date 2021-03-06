﻿using FanLiHang.Dapper.Helper;
using FanLiHang.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Dapper;
using System.Linq;
using System.Linq.Expressions;
namespace FanLiHang.Data
{
    public class RoleDataService : IRoleDataService
    {
        IDbHelper dbHelper;
        public RoleDataService(IDbHelper dbHelper)
        {
            this.dbHelper = dbHelper;
        }

        public bool Add(Role role)
        {
            return dbHelper.Insert<Role>(role);
        }

        public Role Get(int id)
        {
            return dbHelper.GetModel<Role>(new Role { ID = id });
        }

        public PagerResultSet<Role> GetList(int departmentID, PagerParameter pageParameter)
        {
            string sql = " select * from Role where departmentID=@departmentID ";
            return dbHelper.GetModelList<Role>(sql, new { departmentID = departmentID }, pageParameter);
        }

        public IEnumerable<Role> GetList()
        {
            return dbHelper.GetModelList<Role>(null, null);
        }

        public List<RoleFunctionPower> GetPowers(int? roleID, int? appInfoID)
        {

            string sql = @"select FunctionPower.*,RoleFunctionPower.*,Role.*,RoleFunctionPower.ID as RFPID from RoleFunctionPower 
                                join FunctionPower on FunctionPower.ID = RoleFunctionPower.FunctionPowerID
                                join [Role] on [Role] .ID = RoleFunctionPower.RoleID
                                where 1=1";
            var search = new { RoleID = roleID, AppInfoID = appInfoID };
            if (roleID.HasValue)
            {
                sql += " and RoleID = @RoleID";
            }
            if (appInfoID.HasValue)
            {
                sql += " and AppInfoID=@AppInfoID";
            }
            using (var dr = dbHelper.GetReader(sql, search))
            {
                List<RoleFunctionPower> powers = new List<RoleFunctionPower>();
                while (dr.Read())
                {
                    Role role = new Role();
                    role.RoleName = (string)dr["RoleName"];
                    role.ID = (int)dr["RoleID"];

                    RoleFunctionPower roleFunctionPower = new RoleFunctionPower();
                    roleFunctionPower.Role = role;
                    roleFunctionPower.RoleID = role.ID;
                    FunctionPower functionPower = new FunctionPower();
                    functionPower.ID = (int)dr["FunctionPowerID"];
                    functionPower.Level = (int)dr["Level"];
                    functionPower.Power = dr["Power"] == DBNull.Value ? "" : (string)dr["Power"];
                    functionPower.Sort = (int)dr["Sort"];
                    functionPower.SortPath = (string)dr["SortPath"];
                    functionPower.Url = dr["Url"] == DBNull.Value ? "" : (string)dr["Url"];
                    functionPower.FunctionType = (string)dr["FunctionType"];
                    functionPower.FunctionName = (string)dr["FunctionName"];
                    functionPower.FatharFunctionID = (int)dr["FatharFunctionID"];
                    functionPower.AppInfoID = (int)dr["AppInfoID"];
                    roleFunctionPower.FunctionPower = functionPower;
                    roleFunctionPower.FunctionPowerID = functionPower.ID;
                    roleFunctionPower.ID = (int)dr["RFPID"];
                    powers.Add(roleFunctionPower);
                }
                return powers;
            }
        }

        public IEnumerable<Role> GetListByCatch()
        {
            return GetList();
        }

        public bool Update(Role role)
        {
            return dbHelper.Update(role);
        }

        public void UpdateFunctionPowers(int roleID, int appInfoID, int[] functionPowerIDList)
        {
            if (functionPowerIDList == null)
                functionPowerIDList = new int[0];
            var oldFunctionPowers = GetPowers(roleID, appInfoID);
            int[] oldFunctionPowerIDList = oldFunctionPowers.Select(x => x.FunctionPowerID).ToArray();
            var deleteFunctionPowers = oldFunctionPowerIDList.Except(functionPowerIDList);
            var insertFunctionPowers = functionPowerIDList.Except(oldFunctionPowerIDList);

            dbHelper.BeginTran();
            try
            {
                foreach (var deleteItem in deleteFunctionPowers)
                {
                    var functionPower
                        = oldFunctionPowers.Where(x => x.FunctionPowerID == deleteItem).First().ID;
                    dbHelper.DeleteAsTran<RoleFunctionPower>(new RoleFunctionPower { ID = functionPower });
                }
                foreach (var insertItem in insertFunctionPowers)
                {
                    dbHelper.InsertAsTran<RoleFunctionPower>(new RoleFunctionPower { RoleID = roleID, FunctionPowerID = insertItem });
                }
                dbHelper.CommitTran();
            }
            catch (Exception ex)
            {
                dbHelper.RollbackTran();
                throw ex;
            }
        }
    }
}
