using Newtonsoft.Json;
using NLog;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace qodeless.services.api.SysCobrancaClient.Managers.Zenvia
{
    public class SmsSenderManager
    {
        public static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public const string SENDER_DEFAULT = "glittery-yttrium";

        public static bool SendSms(string to, string content, string from = "honored-summer")
        {
            try
            {
                var URL = "https://api.zenvia.com/v2";
                var URI = "/channels/sms/messages";

                //ENDPOINT 
                //Logger.Info($"Sending log to {to}. DwellerId ${dwellerId}");

                var endpoint = $"{URL}{URI}";
                var client = new RestClient(endpoint);

                RestRequest request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("X-API-TOKEN", "MfJ7sGemrJZ9CYBo7vRJhjLvioNXqc-7-mcT");

                var body = new
                {
                    from = from,
                    to = to,
                    contents = new[] { new {
               type = "text",
               text = content

                   }
                }
                };

                request.AddJsonBody(body);
                var response = client.Execute(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }

                //TODO: Revalidar Error Code ZEnvia
                return true;
            }
            catch (Exception e)
            {
            }


            return true;
        }

    }
}