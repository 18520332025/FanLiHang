using FanLiHang.Model;
using System;
using System.Collections.Generic;
using System.Text;
using FanLiHang.Data;
namespace Fanlihang.TagHelpers
{
    public class UserDropDownListTagHelper : BaseDropDownListTagHelper<UserInfo>
    {
        IUserInfoDataService userInfoDataService;
        public UserDropDownListTagHelper(IUserInfoDataService userInfoDataService) : base(x => x.ID, x => x.Name)
        {
            this.userInfoDataService = userInfoDataService;
        }

        public override IEnumerable<UserInfo> GetData()
        {
            return userInfoDataService.GetListAtCache();
        }
    }
}
