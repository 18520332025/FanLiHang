using System;
using System.Collections.Generic;
using System.Text;
using FanLiHang.Model;
using FanLiHang.Dapper.Helper;
using System.Linq.Expressions;
using System.Linq;
using System.Data;
using Dapper;
namespace FanLiHang.Data
{
    public class DepartmentDataLeval : IDepartmentDataLeave
    {
        public bool HasRotes
        {
            get; set;
        }

        public bool HasPowers
        {
            get; set;
        }

        public bool HasUser
        {
            get; set;
        }
    }

    public class DepartmentDataService : IDepartmentDataService
    {
        IDbHelper _dbHelper;
        IUserInfoDataService userInfoDataService;
        IRoleDataService roleDataService;
        public DepartmentDataService(IDbHelper dbHelper, IUserInfoDataService userInfoDataService, IRoleDataService roleDataService)
        {
            this.userInfoDataService = userInfoDataService;
            this.roleDataService = roleDataService;
            _dbHelper = dbHelper;

        }

        public Department Add(Department department)
        {
            _dbHelper.Insert(department);
            return department;
        }

        public Department Get(int autoID)
        {
            return _dbHelper.GetModel(new Department { ID = autoID });
        }

        public Department Get(string name)
        {
            return _dbHelper.GetModelList<Department>(" and name='" + name + "'").FirstOrDefault();
        }

        public PagerResultSet<Department> GetList(PagerParameter pageParameter, Action<IDepartmentDataLeave> option = null)
        {
            string sql = "select * from Department";
            var departmentList = _dbHelper.GetModelList<Department>(sql, null, pageParameter);

            DepartmentDataLeval dataLeval = new DepartmentDataLeval();
            if (option != null)
            {
                option(dataLeval);
                if (dataLeval.HasUser)
                {
                    var users = new List<UserInfo>(userInfoDataService.GetListAtCache());
                    foreach (var department in departmentList.Rows)
                    {
                        department.CreateUser = users.Where(x => x.ID.Equals(department.CreateBy)).FirstOrDefault();
                        department.ManagerUser = users.Where(x => x.ID.Equals(department.Manager)).FirstOrDefault();
                    }
                }
                if (dataLeval.HasRotes)
                {
                    var roles = roleDataService.GetListByCatch();
                    foreach (var department in departmentList.Rows)
                    {
                        department.Roles = roles.Where(x => x.DepartmentID.Equals(department.ID)).ToList();
                    }
                }
            }
            return departmentList;
        }

        public IEnumerable<Department> GetList()
        {
            return _dbHelper.GetModelList<Department>(null);
        }

        public List<DepartmentFunctionPower> GetPowers(int? departmentID, int? appInfoID)
        {
            string sql = @"select DepartmentFunctionPower.ID as DFPID, FunctionPower.*,DepartmentFunctionPower.*,Department.* from DepartmentFunctionPower 
                                join FunctionPower on FunctionPower.ID = DepartmentFunctionPower.FunctionPowerID
                                join Department on Department.ID = DepartmentFunctionPower.DepartmentID
                                where 1=1";
            var search = new { DepartmentID = departmentID, AppInfoID = appInfoID };
            if (departmentID.HasValue)
            {
                sql += " and departmentID = @DepartmentID";
            }
            if (appInfoID.HasValue)
            {
                sql += " and AppInfoID=@AppInfoID";
            }
            using (var dr = _dbHelper.GetReader(sql, search))
            {
                List<DepartmentFunctionPower> powers = new List<DepartmentFunctionPower>();
                while (dr.Read())
                {
                    Department department = new Department();
                    department.Name = (string)dr["Name"];
                    department.ID = (int)dr["DepartmentID"];
                    department.Manager = (int)dr["Manager"];
                    department.CreateBy = (int)dr["CreateBy"];
                    department.Deleted = (bool)dr["Deleted"];
                    department.CreateDate = (DateTime)dr["CreateDate"];
                    DepartmentFunctionPower departmentFunctionPower = new DepartmentFunctionPower();
                    departmentFunctionPower.Department = department;
                    departmentFunctionPower.DepartmentID = department.ID;
                    departmentFunctionPower.ID = (int)dr["DFPID"];
                    FunctionPower functionPower = new FunctionPower();
                    functionPower.ID = (int)dr["FunctionPowerID"];
                    functionPower.Level = (int)dr["Level"];
                    functionPower.Power = dr["Power"] == DBNull.Value ? "" : (string)dr["Power"];
                    functionPower.Sort = (int)dr["Sort"];
                    functionPower.SortPath = dr["SortPath"] == DBNull.Value ? "" : (string)dr["SortPath"];
                    functionPower.Url = dr["Url"] == DBNull.Value ? "" : (string)dr["Url"];
                    functionPower.FunctionType = (string)dr["FunctionType"];
                    functionPower.FunctionName = (string)dr["FunctionName"];
                    functionPower.FatharFunctionID = (int)dr["FatharFunctionID"];
                    functionPower.AppInfoID = (int)dr["AppInfoID"];
                    departmentFunctionPower.FunctionPower = functionPower;
                    departmentFunctionPower.FunctionPowerID = functionPower.ID;
                    powers.Add(departmentFunctionPower);
                }
                return powers;
            }
        }

        public bool Remove(Department department)
        {
            return _dbHelper.Delete(department);
        }

        public bool Update(Department department)
        {
            return _dbHelper.Update(department);
        }
    }
}
