using System;
using System.Collections.Generic;
using System.Text;

namespace FanLiHang.Mail
{
    public static class Program
    {
        public static void Main()
        {
            MailJobService mailJobService = new MailJobService();
            SendCloudMailConfig sendCloudMailConfig = new SendCloudMailConfig();
            sendCloudMailConfig.apiUser = "fanlihang_test_FoAqJi";
            sendCloudMailConfig.apiKey = "ASpSprbILLu5ezSr";
            sendCloudMailConfig.from = "2GzcX3sSD0QTvWbRt6VByvu9x9BIrydQ.sendcloud.org";
            sendCloudMailConfig.fromName = "FanLiHang";
            sendCloudMailConfig.url = "http://api.sendcloud.net/apiv2/mail/send";
            SendCloudMailService mailService = new SendCloudMailService(sendCloudMailConfig);
         
            mailJobService.Consume(mailService);
            mailJobService.AddJob(new Mail
            {
                html = "测试",
                to = "444705079@qq.com",
                labelId = 222120,
                respEmailId = "true",
                subject = "测试发送"
            });

        }
    }
}
