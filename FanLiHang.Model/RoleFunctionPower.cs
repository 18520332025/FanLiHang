using FanLiHang.Dapper.ModelData.Attribute;
using System;
using System.Collections.Generic;
using System.Text;

namespace FanLiHang.Model
{
    [TableNameAttribute(TableName: "RoleFunctionPower")]
    public class RoleFunctionPower
    {
        [Column(ColunmType.Key)]
        public int ID
        {
            get;
            set;
        }

        public int RoleID
        {
            get;
            set;
        }

        public int FunctionPowerID
        {
            get;
            set;
        }

        public Role Role
        {
            get;
            set;
        }

        public FunctionPower FunctionPower
        {
            get;
            set;
        }
    }
}
