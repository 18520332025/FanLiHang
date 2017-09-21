using System;
using System.Collections.Generic;
using System.Text;
using FanLiHang.Model;
using FanLiHang.Data;
namespace Fanlihang.TagHelpers.TagHelpers
{
    public class AppInfoDropDownList : BaseDropDownListTagHelper<AppInfo>
    {
        IAppInfoDataService appInfoDataService;
        public AppInfoDropDownList(IAppInfoDataService appInfoDataService) : base(x => x.ID, x => x.Name)
        {
            this.appInfoDataService = appInfoDataService;
        }

        public override IEnumerable<AppInfo> GetData()
        {
            return appInfoDataService.GetList();
        }
    }
}
