using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FanLiHang.Dapper.ModelData.Attribute
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class TableNameAttribute : System.Attribute
    {
        /// <summary>
        /// 设置Model所对应的数据表
        /// </summary>
        /// <param name="TableName">数据表名</param>
        public TableNameAttribute(string TableName)
        {
            this.TableName = TableName;
        }
        public string TableName
        {
            get;
            set;
        }

    }
}
