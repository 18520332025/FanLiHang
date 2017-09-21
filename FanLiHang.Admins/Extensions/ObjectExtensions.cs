using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FanLiHang.Admins.Extensions
{
    public static class ObjectExtensions
    {
        public static D Mapper<D, S>(this S s)
        {
            D d = Activator.CreateInstance<D>();
            var Types = s.GetType();//获得类型  
            var Typed = typeof(D);
            foreach (PropertyInfo sp in Types.GetProperties())//获得类型的属性字段  
            {
                foreach (PropertyInfo dp in Typed.GetProperties())
                {
                    try
                    {
                        if (dp.Name == sp.Name)//判断属性名是否相同  
                        {
                            dp.SetValue(d, sp.GetValue(s, null), null);//获得s对象属性的值复制给d对象的属性  
                        }
                    }
                    catch
                    {
                        throw new Exception(sp.Name);
                    }
                }
            }

            return d;
        }

        public static IEnumerable<D> MapperList<D, S>(this IEnumerable<S> list)
        {
            List<D> dList = Activator.CreateInstance<List<D>>();
            foreach (var item in list)
            {
                dList.Add(item.Mapper<D, S>());
            }
            return dList;
        }
    }
}
