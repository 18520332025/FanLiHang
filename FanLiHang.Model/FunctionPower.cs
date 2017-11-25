using FanLiHang.Dapper.ModelData.Attribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FanLiHang.Model
{
    [TableNameAttribute(TableName: "FunctionPower")]
    public class FunctionPower
    {
        [Column(ColunmType.Key)]
        public int ID
        {
            get;
            set;
        }
        [Display(Name = "权限类型")]
        public string FunctionType
        {
            get;
            set;
        }
        [Display(Name = "Url")]
        [Url(ErrorMessage ="Url地址不合法")] 
        public string Url
        {
            get;
            set;
        }
        [Display(Name = "权限名")]
        public string FunctionName
        {
            get;
            set;
        }
        [Display(Name = "父权限")]
        public int FatharFunctionID
        {
            get;
            set;
        }
    
        [Display(Name = "权限码")]
        public string Power
        {
            get;
            set;
        }
        [Display(Name = "所属系统")]
        public int AppInfoID
        {
            get; set;
        }
        [Column(ColunmType.NoDataColumn)]
        public AppInfo AppInfo
        {
            get; set;
        }

        [Display(Name = "级")]
        public int Level
        {
            get; set;
        }
        [Display(Name = "序号")]
        public int Sort
        {
            get; set;
        }
        [Display(Name = "序号排序")]
        public string SortPath
        {
            get; set;
        }
    }
}
