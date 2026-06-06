using Microsoft.AspNetCore.DataProtection;
using Newtonsoft.Json;
using System.Net;
using System.Security.Policy;
using System.Text;
using WebGrease.Css.Ast;

namespace HallManagement.Web.Classes
{
    public class SmsApiClient
    {
        private string _apiKey;
        private string _apiSecret;
        private string _apiUrl;


        public SmsApiClient(string apiKey, string apiSecret, string apiUrl)
        {
            _apiKey = apiKey;
            _apiSecret = apiSecret;
            _apiUrl = apiUrl;
        }

        public async Task<SmsResponse> SendSms(string mobileNumber, string smsBody)
        {
            var smsResponse = new SmsResponse();
            string responseData2 = "{\"request_type\": \"SINGLE_SMS\",\"campaign_uid\": \"C6189131710437848\",\"sms_uid\": \"S0275481710437848\",\"invalid_numbers\": [],\"api_response_code\": 200,\"api_response_message\": \"SUCCESS\"}";
            var responseObject2 = JsonConvert.DeserializeObject<dynamic>(responseData2);
            Console.WriteLine(responseObject2);
            smsResponse.IsSuccess = true;
            smsResponse.SmsSentTime = DateTime.Now;
            smsResponse.SmsUid = responseObject2.sms_uid;
            return smsResponse;

            var parameters = new
            {
                api_key = _apiKey,
                api_secret = _apiSecret,
                request_type = "SINGLE_SMS",
                message_type = "TEXT",
                mobile = mobileNumber,
                message_body = smsBody
            };
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var json = JsonConvert.SerializeObject(parameters);
                    var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(_apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseData = await response.Content.ReadAsStringAsync();
                        var responseObject = JsonConvert.DeserializeObject<dynamic>(responseData);
                        Console.WriteLine(responseObject);
                        smsResponse.IsSuccess = true;
                        smsResponse.SmsSentTime = DateTime.Now;
                        smsResponse.SmsUid = responseObject.sms_uid;

                    }
                }
                catch(Exception ex)
                {
                    smsResponse.IsSuccess = false;
                    smsResponse.Error = "SMS Sending Failed." + ex.Message;
                }
            }
            return smsResponse;
        }
    }
}
