using FanLiHang.Dapper.ModelData.Attribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FanLiHang.Model
{
    [TableNameAttribute(TableName: "Role")]
    public class Role
    {
        [Column(ColunmType.Key)]
        public int ID
        {
            get;
            set;
        }
        [Display(Name = "角色名称")]
        public string RoleName
        {
            get;
            set;
        }

        public int DepartmentID
        {
            get;
            set;
        }

        [Column(ColunmType.NoDataColumn)]
        public List<RoleFunctionPower> RoleFunctionPower
        {
            get;
            set;
        }
        [Column(ColunmType.NoDataColumn)]
        public Department Department
        {
            get; set;
        }
    }
}
