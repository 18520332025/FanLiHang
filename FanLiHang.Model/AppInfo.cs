using FanLiHang.Dapper.ModelData.Attribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FanLiHang.Model
{
    [TableNameAttribute(TableName: "AppInfo")]
    public class AppInfo
    {
        [Column(ColunmType.Key)]
        public int ID
        {
            get;
            set;
        }

        [Display(Name = "应用代码")]
        public string Code
        {
            get;
            set;
        }

        [Display(Name = "应用名称")]
        public string Name
        {
            get;
            set;
        }
    }
}
