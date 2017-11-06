using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ;
using RabbitMQ.Client;
using StackExchange.Redis;
using RabbitMQ.Client.Events;

namespace FanLiHang.OrderDownload
{
    public class DownOrders
    {
        public void Down()
        {
            string path = "http://10.0.0.252:810/data/order";
            string[] files = {
                @"1.json",
                @"2.json",
                @"3.json"
            };

            string queueName = "Order.Analysis";
            string exchangeName = "Order";
            string routeKey = "";
            ConnectionFactory connectionFactory = new ConnectionFactory()
            {
                UserName = "yy",
                Password = "123",
                HostName = "localhost"
            };
            var connection = connectionFactory.CreateConnection();
            //通道
            var channal = connection.CreateModel();
            channal.ExchangeDeclare(exchangeName, ExchangeType.Fanout, false, false, null);
            channal.QueueDeclare(queueName, false, false, false, null);
            channal.QueueBind(queueName, exchangeName, routeKey, null);

            System.Net.WebClient webClient = new System.Net.WebClient();
            foreach (var fileName in files)
            {
                string content = webClient.DownloadString(path + fileName);
                var sendBytes = Encoding.UTF8.GetBytes(content);
                channal.BasicPublish(exchangeName, routeKey, null, sendBytes);
            }
            channal.Close();
            connection.Close();
        }

        public void Analysis()
        {

            string exchangeName = "Order";
            string routeKey = "";
            string queueName = "queueName";
            ConnectionFactory connectionFactory = new ConnectionFactory()
            {
                UserName = "yy",
                Password = "123",
                HostName = "localhost"
            };
            var connection = connectionFactory.CreateConnection();
            //通道
            var channal = connection.CreateModel();
            EventingBasicConsumer consumer = new EventingBasicConsumer(channal);
            consumer.Received += (sender, e) =>
            {
                var content = Encoding.UTF8.GetString(e.Body);
                IEnumerable<OrderBuy> orderBuys = OrderBuy.LoadOrder(content);
                foreach (var order in orderBuys)
                {
                    ///订单解析逻辑
                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(order);
                    var sendBytes = Encoding.UTF8.GetBytes(json);
                    channal.BasicPublish(exchangeName, routeKey, null, sendBytes);
                    channal.BasicAck(e.DeliveryTag, false);
                }
            };
            decimal d = 1.1m;
            int d2 = (int)d;
            //绑定到相关的队列
            channal.BasicConsume(queueName, false, consumer);
            channal.Close();
            connection.Close();
        }


        public bool OrderDBService()
        {
            Redis.SERedisHelper sERedisHelper = new Redis.SERedisHelper(ConnectionMultiplexer.Connect("47.104.4.117:6379"));
            //foreach (var item in )
            return true;
        }
    }
}
