using System;
using System.Collections.Generic;
using System.Text;
using FanLiHang.Model;
namespace FanLiHang.Data
{
    public interface IAppInfoDataService
    {
        IEnumerable<AppInfo> GetList();
    }
}
