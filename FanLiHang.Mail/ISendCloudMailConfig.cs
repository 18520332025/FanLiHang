namespace FanLiHang.Mail
{
    public interface IMailConfig
    {
        string apiKey { get; set; }
        string apiUser { get; set; }
        string from { get; set; }
        string fromName { get; set; }
        string url { get; set; }
    }
}