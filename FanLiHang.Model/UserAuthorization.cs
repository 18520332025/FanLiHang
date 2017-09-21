using FanLiHang.Dapper.ModelData.Attribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FanLiHang.Model
{
    [TableNameAttribute(TableName: "UserAuthorization")]
    public class UserAuthorization
    {
        [Display(Name = "ID")]
        [Column(ColunmType.Key)]
        public int ID
        {
            get;
            set;
        }

        [Column(ColunmType.NoDataColumn)]
        public UserInfo User
        {
            get;
            set;
        }
        [Column(ColunmType.NoDataColumn)]
        public Department Department
        {
            get; set;
        }

        [Column(ColunmType.NoDataColumn)]
        public AppInfo AppInfo
        {
            get;
            set;
        }
        [Required]
        [Display(Name = "AppID")]
        public int AppInfoID
        {
            get;
            set;
        }
        [Required]
        [Display(Name = "用户ID")]
        public int UserID
        {
            get;
            set;
        }
        [Required]
        [Display(Name = "登陆ID")]
        public string LoginID
        {
            get;
            set;
        }
        [Required]
        
        [Display(Name = "登陆密码")]
        [DataType(DataType.Password)]
        public string Password
        {
            get;
            set;
        }
        [Required]
        [Display(Name = "部门ID")]
        public int DepartmentID
        {
            get;
            set;
        }

    }
}
