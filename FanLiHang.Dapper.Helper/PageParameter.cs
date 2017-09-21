using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FanLiHang.Dapper.Helper
{
    public class PagerParameter
    {
        /// <summary>
        /// 唯一键值
        /// </summary>
        public string OrderBy
        {
            get;
            set;
        }
        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize
        {
            get;
            set;
        }
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex
        {
            get;
            set;
        }

        public bool Desc
        {
            get;
            set;
        }
        
    }
}
