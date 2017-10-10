using FanLiHang.Data;
using FanLiHang.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fanlihang.TagHelpers.TagHelpers
{
    public class RoleDropDownList : BaseDropDownListTagHelper<Role>
    {
        IRoleDataService dataService;
        public RoleDropDownList(IRoleDataService dataService) : base(x => x.ID, x => x.RoleName)
        {
            this.dataService = dataService;
        }

        public override IEnumerable<Role> GetData()
        {
            return dataService.GetList();
        }
    }
}
