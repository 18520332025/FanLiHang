using FanLiHang.Dapper.ModelData.Attribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FanLiHang.Model
{
    [TableNameAttribute(TableName: "Department")]
    public class Department
    {
        [Column(ColunmType.Key)]
        public int ID
        {
            get;
            set;
        }
        [Display(Name = "部门名称")]
        public string Name
        {
            get;
            set;
        }
        [Display(Name = "创建人")]
        public int CreateBy
        {
            get;
            set;
        }

        public UserInfo CreateUser
        {
            get;
            set;
        }

        public DateTime CreateDate
        {
            get;
            set;
        }
        [Display(Name = "是否删除")]
        public bool Deleted
        {
            get;
            set;
        }
        [Display(Name = "部门主管")]
        public int Manager
        {
            get;
            set;
        }

        public UserInfo ManagerUser
        {
            get;
            set;
        }

        public List<DepartmentFunctionPower> FunctionPowers
        {
            get;
            set;
        }

        public List<Role> Roles
        {
            get;set;
        }
         
    }
}
