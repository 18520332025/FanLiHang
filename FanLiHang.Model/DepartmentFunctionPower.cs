using FanLiHang.Dapper.ModelData.Attribute;
using System;
using System.Collections.Generic;
using System.Text;

namespace FanLiHang.Model
{
    [TableNameAttribute(TableName: "DepartmentFunctionPower")]
    public class DepartmentFunctionPower
    {
        [Column(ColunmType.Key)]
        public int ID
        {
            get;
            set;
        }

        public Department Department
        {
            get;
            set;
        }

        public int DepartmentID
        {
            get;
            set;
        }

        public int FunctionPowerID
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
