using System;
using FanLiHang.Dapper.Helper;
using FanLiHang.Model;
using System.Collections.Generic;

namespace FanLiHang.Data
{
    public interface IDepartmentDataService
    {
        PagerResultSet<Department> GetList(PagerParameter pageParameter, Action<IDepartmentDataLeave> option = null);
        IEnumerable<Department> GetList();
        Department Get(int autoID);
        Department Get(string name);
        Department Add(Department department);
        bool Update(Department department);
        bool Remove(Department department);
        List<DepartmentFunctionPower> GetPowers(int? departmentID, int? appInfoID);
        void UpdateFunctionPowers(int DepartmentID, int AppinfoID, int[] functionPowerIDList);
    }

    public interface IDepartmentDataLeave
    {
        bool HasRotes { get; set; }
        bool HasPowers { get; set; }
        bool HasUser { get; set; }
    }
}
