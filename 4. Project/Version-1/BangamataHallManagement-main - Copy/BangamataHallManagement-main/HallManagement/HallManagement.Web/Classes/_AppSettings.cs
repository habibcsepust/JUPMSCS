using Microsoft.AspNetCore.Identity;
using System.Configuration;

namespace Web.Classes
{
    public class _AppSettings
    {
        public _AppSettings()
        {
        }
        public string DbConnectionString { get; set; }
        public int SessionTimeoutSecond { get; set; }
        public string SmsApiKey { get; set; }
        public string SmsApiSecret { get; set; }
        public string SmsApiUrl { get; set; }
        public string BaseUrl { get; set; }
        public int ForgotLinkExpiryTimeout { get; set; }
        public string CryptoKey { get; set; }
    }
}
