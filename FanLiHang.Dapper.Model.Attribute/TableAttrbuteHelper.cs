using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
namespace FanLiHang.Dapper.ModelData.Attribute
{
    public class TableAttrbuteHelper
    {
        public static string GetTableName<T>()
        {
            return GetTableName(typeof(T));
        }

        public static string GetTableName(Type type)
        {
            var member = (TableNameAttribute)type.GetTypeInfo().GetCustomAttribute<TableNameAttribute>();
            if (member != null)
            {                
                return member.TableName;
            }
            else
            {
                return null;
            }
        }

    }
}
