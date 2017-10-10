using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;

namespace FanLiHang.Mail
{
    public class MailJobService
    {

        string queueName = "sendMail";
        string exchangeName = "MailServcie";
        string routeKey = "";
        public void AddJob(Mail mail)
        {
            ConnectionFactory connectionFactory = new ConnectionFactory()
            {
                UserName = "yy",
                Password = "123",
                HostName = "localhost"
            };
            //连接
            var connection = connectionFactory.CreateConnection();
            //通道
            var channal = connection.CreateModel();
            //channal.QueueDeclare(queueName, false, false, false, null);
            string json = JsonConvert.SerializeObject(mail);
            var sendBytes = Encoding.UTF8.GetBytes(json);
            //定义一个Direct类型交换机
            channal.ExchangeDeclare(exchangeName, ExchangeType.Fanout, false, false, null);
            //定义队列
            channal.QueueDeclare(queueName, false, false, false, null);
            //将队列绑定到交换机
            channal.QueueBind(queueName, exchangeName, routeKey, null);
            //发布消息
            channal.BasicPublish(exchangeName, routeKey, null, sendBytes);            
            channal.Close();
            connection.Close();
        }


        public void Consume(IMailService mailService)
        {
            ConnectionFactory factory = new ConnectionFactory
            {
                UserName = "yy",//用户名
                Password = "123",//密码
                HostName = "localhost"//rabbitmq ip
            };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
                //读取邮件并发布邮件
                var body = Encoding.UTF8.GetString(e.Body);
                Mail mail = JsonConvert.DeserializeObject<Mail>(body);
                mailService.Send(mail);
                channel.BasicAck(e.DeliveryTag, false);
            };
            //绑定到相关的队列
            channel.BasicConsume(queueName, false, consumer);
        }
    }
}
