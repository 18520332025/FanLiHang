 
namespace FanLiHang.Mail
{
    public class SendCloudMailConfig : IMailConfig
    {
        public string apiUser
        {
            get; set;
        }

        public string apiKey
        {
            get; set;
        }

        public string from
        {
            get;
            set;
        }

        public string fromName
        {
            get;
            set;
        }

        public string url
        {
            get;
            set;
        }
    }
}
