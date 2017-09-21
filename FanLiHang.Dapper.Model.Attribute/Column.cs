using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FanLiHang.Dapper.ModelData.Attribute
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class Column : System.Attribute
    {
        public string Alias { get; set; }
        public bool Key { get; set; }
        public bool NoDataColumn { get; set; }
        public string ColumnCnName { get; set; }        

        public Column(ColunmType Key)
        {
            this.Key = Key == ColunmType.Key;
            this.NoDataColumn = Key == ColunmType.NoDataColumn;
        }
    }

    public enum ColunmType
    {
        Key = 0,
        NoDataColumn = 1,
        Default = 2
    }
}
