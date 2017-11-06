using System;
using System.Collections.Generic;
using System.Text;

namespace FanLiHang.Redis
{
    public class OrderNumberHelper
    {
        SERedisHelper sERedisHelper;
        public OrderNumberHelper(SERedisHelper sERedisHelper)
        {
            this.sERedisHelper = sERedisHelper;
        }

        public string GetNumber(string prefix)
        {
            string key = "order_num_" + prefix;
            string date = DateTime.Now.ToString("yyyyMMdd");
            string number = prefix + date + sERedisHelper.Increment(key, 0).ToString("000000000");
            return number;
        }
    }
}
