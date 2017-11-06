using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace FanLiHang.OrderDownload
{
    public class OrderBuy
    {
        public string OrderNumber
        {
            get;
            set;
        }

        public OrderBuyUserAddress OrderBuyUserAddress
        {
            get;
            set;
        }

        public IEnumerable<OrderBuyItems> OrderBuyItems
        {
            get;
            set;
        }

        private OrderBuy()
        {
        }

        public static IEnumerable<OrderBuy> LoadOrder(string content)
        {
            return JsonConvert.DeserializeObject<IEnumerable<OrderBuy>>(content);
        }
    }
}
