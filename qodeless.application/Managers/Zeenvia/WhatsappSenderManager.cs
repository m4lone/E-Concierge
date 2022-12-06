using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace qodeless.services.api.SysCobrancaClient.Managers.Zenvia
{
    public class WhatsappSenderManager
    {
        public static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public const string SENDER_DEFAULT = "slow-wedelia";
        public static bool SendWhatsapp(string to, string content, string from = "slow-wedelia")
        {
            try
            {
                var URL = "https://api.zenvia.com/v2";
                var URI = "/channels/whatsapp/messages";

                //ENDPOINT 
                //Logger.Info($"Sending log to {to}. DwellerId ${dwellerId}");

                var endpoint = $"{URL}{URI}";
                var client = new RestClient(endpoint);

                RestRequest request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("X-API-TOKEN", "EhYO_gX4lNid3w28wkeWHn0gvQg6lVsTrne_");

                var body = new
                {
                    from = from,
                    to = to,

                    contents = new[]
                {
               new {

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
            }
            catch (Exception e)
            {

            }

            return true;
           
        }
    }
}
