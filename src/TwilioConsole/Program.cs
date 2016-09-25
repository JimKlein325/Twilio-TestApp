using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TwilioConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            var client = new RestClient("https://api.twilio.com/2010-04-01");
            string AccountSid = "ACfbbd8982f403529b2b3e85be978c0723";
            string AuthToken = "38e3240fb14c60f7ca0fa21e28436989";
            //1
            var request = new RestRequest("Accounts/"+ AccountSid +"/Messages", Method.GET);
            client.Authenticator = new HttpBasicAuthenticator(AccountSid, AuthToken);
            //2
            var response = new RestResponse();
            //request.AddParameter("To", "+15037408645");
            //request.AddParameter("From", "+15005550006");//using magic number from docs https://www.twilio.com/docs/api/rest/test-credentials#test-sms-messages
            //request.AddParameter("Body", "Hello world!");
            ////request.AddParameter("To", "+15037408645");
            ////request.AddParameter("From", "+15034867935");

            //3a
            Task.Run(async () =>
            {
                response = await GetResponseContentAsync(client, request) as RestResponse;
            }).Wait();
            //4
            //JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(response.Content);
            //Console.WriteLine(jsonResponse["body"]);
            //Console.ReadLine();
            //var message = JsonConvert.DeserializeObject<Message>(jsonResponse["body"].ToString());
            Console.WriteLine(response.Content);

            Console.ReadLine();
        }

        //3b
        public static Task<IRestResponse> GetResponseContentAsync(RestClient theClient, RestRequest theRequest)
        {
            var tcs = new TaskCompletionSource<IRestResponse>();
            theClient.ExecuteAsync(theRequest, response => {
                tcs.SetResult(response);
            });
            return tcs.Task;
        }
        //public static void Main(string[] args)
        //{
        //    //1
        //    var client = new RestClient("https://api.twilio.com/2010-04-01");
        //    //2
        //    var request = new RestRequest("Accounts/ACfbbd8982f403529b2b3e85be978c0723/Messages", Method.POST);
        //    //3
        //    request.AddParameter("To", "+15034867935");
        //    request.AddParameter("From", "+15037408645");
        //    //request.AddParameter("To", "+15037408645");
        //    //request.AddParameter("From", "+15034867935");
        //    request.AddParameter("Body", "Hello world!");
        //    //4
        //    client.Authenticator = new HttpBasicAuthenticator("ACfbbd8982f403529b2b3e85be978c0723", "38e3240fb14c60f7ca0fa21e28436989");
        //    //5
        //    client.ExecuteAsync(request, response =>
        //    {
        //        Console.WriteLine(response);
        //    });
        //    Console.ReadLine();
        //}
    }
    public class Message
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Body { get; set; }
        public string Status { get; set; }
    }
}
