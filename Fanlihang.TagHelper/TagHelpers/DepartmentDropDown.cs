using System;
using System.Collections.Generic;
using System.Text;
using FanLiHang.Data;
using FanLiHang.Model;
namespace Fanlihang.TagHelpers.TagHelpers
{
    public class DepartmentDropDown:BaseDropDownListTagHelper<Department>
    {
        IDepartmentDataService dataService;
        public DepartmentDropDown(IDepartmentDataService dataService) : base(x => x.ID, x => x.Name)
        {
            this.dataService = dataService;
        }

        public override IEnumerable<Department> GetData()
        {
            return this.dataService.GetList();
        }
    }
}
