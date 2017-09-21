using System;
using FanLiHang.Dapper.ModelData.Attribute;
using System.ComponentModel.DataAnnotations;

namespace FanLiHang.Model
{
    [TableName("UserInfo")]
    public class UserInfo
    {
        [Column(ColunmType.Key)]
        [Display(Name = "ID")]
        public int ID
        {
            get;
            set;
        }
        [Display(Name = "姓名")]
        public string Name
        {
            get;
            set;
        }
        [Display(Name = "电话")]
        public string Phone
        {
            get;
            set;
        }
        [Display(Name = "短号")]
        public string Cornet
        {
            get;
            set;
        }
        [Display(Name = "邮箱")]
        public string Email
        {
            get;
            set;
        }
        [Display(Name = "QQ")]
        public string QQ
        {
            get;
            set;
        }
        [Display(Name = "地址")]
        public string Address
        {
            get;
            set;
        }
        [Display(Name = "籍贯")]
        public string Domicile
        {
            get;
            set;
        }
        [Display(Name = "头像")]
        public string PhotoUrl
        {
            get; set;
        }

    }
}
