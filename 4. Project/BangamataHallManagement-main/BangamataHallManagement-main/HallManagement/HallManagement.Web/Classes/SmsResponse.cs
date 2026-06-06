namespace HallManagement.Web.Classes
{
    public class SmsResponse
    {
        public bool IsSuccess { get; set; }
        public DateTime SmsSentTime { get; set; }
        public string SmsUid { get; set; }
        public string Error { get; set; }
    }
}
