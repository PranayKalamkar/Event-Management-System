namespace Event_Management_App.Models
{
    public class GetEmailSettingModel
    {
        public string? Email { get; set; }
        public string? SecretKey { get; set; }
        public string? From { get; set; }
        public string? SmtpServer { get; set; }
        public int Port { get; set; }
        public bool EnableSSL { get; set; }
    }
}
