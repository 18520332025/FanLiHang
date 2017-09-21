
using FanLiHang.Dapper.ModelData.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FanLiHang.Dapper.Helper
{
    public class DapperBaseSQL
    {
        public string Insert { get; set; }
        public string Update { get; set; }
        public string Get { get; set; }
        public string GetList { get; set; }
        public string Delete { get; set; }
        public DataTableConfig TableConifg
        {
            get;
            set;
        }
    }
}
