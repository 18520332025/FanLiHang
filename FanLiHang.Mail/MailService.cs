using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace FanLiHang.Mail
{
    public class SendCloudMailService : IMailService
    {
        public SendCloudMailService(IMailConfig mailConfig)
        {
            this.mailConfig = mailConfig;
        }
        private readonly IMailConfig mailConfig;
        public bool Send(Mail mail)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage message = null;
            List<KeyValuePair<string, string>> paramList = new List<KeyValuePair<string, string>>();
            paramList.Add(new KeyValuePair<string, string>("apiUser", mailConfig.apiUser));
            paramList.Add(new KeyValuePair<string, string>("apiKey", mailConfig.apiKey));
            paramList.Add(new KeyValuePair<string, string>("from", Guid.NewGuid().ToString() + "@" + mailConfig.from));
            paramList.Add(new KeyValuePair<string, string>("fromName", mailConfig.fromName));
            paramList.Add(new KeyValuePair<string, string>("to", mail.to));
            paramList.Add(new KeyValuePair<string, string>("subject", mail.subject));
            paramList.Add(new KeyValuePair<string, string>("html", mail.html));
            paramList.Add(new KeyValuePair<string, string>("labelId", mail.labelId.ToString()));
            message = httpClient.PostAsync(mailConfig.url, new FormUrlEncodedContent(paramList)).Result;
            string result = message.Content.ReadAsStringAsync().Result;
            return true;
        }
    }
}
