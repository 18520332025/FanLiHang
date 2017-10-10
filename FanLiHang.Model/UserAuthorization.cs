using FanLiHang.Dapper.ModelData.Attribute;
using System.ComponentModel.DataAnnotations; 
using FanLiHang.ValidationExpand;

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
        [Remote(DataKey = new string[] { "LoginID", "ID" },
            ErrorMessage = "系统内已存在该登录名，请使用其他登陆名",
            Type = "Get",
            Url = "/User/RemoteLoginID",
            RemoteName = "RemoteLoginID")]
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

        [Required(ErrorMessage = "必须输入登录名")]
        [Remote(DataKey = new string[] { "AppInfoID", "ID" },
            ErrorMessage = "系统内已存在该登录名，请使用其他登陆名",
            Type = "Get",
            Url = "/User/RemoteLoginID",
            RemoteName = "RemoteLoginID")]
        [Display(Name = "登陆名")]
        public string LoginID
        {
            get;
            set;
        }
        [Required(ErrorMessage = "必须输入密码")]
        [Display(Name = "登陆密码")]
        [MinLength(8, ErrorMessage = "密码最小长度为8位")]
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
