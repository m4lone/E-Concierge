using Newtonsoft.Json;
using qodeless.application.ViewModels;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Net;
using System.Text;

namespace qodeless.application.Managers.Pix
{
    public class PixManager
    {
        public ResponsePixViewModel GetPix(string token, float value)
        {
            var client = new RestClient("https://privilege.wallet.corebanx.com/api/generate-qrcodes")
            {
                Timeout = -1
            };
            var date = DateTime.Now;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", $"Bearer {token}");
            var body = new RequestViewModel
            {
                Value = value,
                AccountNumber = "56543760",
                Description = "pix"
            };
            request.AddParameter("application/json", JsonConvert.SerializeObject(body), ParameterType.RequestBody);
            var result = client.Execute(request);
            if (result.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(result.Content.ToString());
            }
            return JsonConvert.DeserializeObject<ResponsePixViewModel>(result.Content);
        }
        public ResponsePixViewModel GetPixStatus(string token, string pixId)
        {
            var client = new RestClient("https://privilege.wallet.corebanx.com" + pixId)
            {
                Timeout = -1
            };
            var request = new RestRequest(Method.GET);
            request.AddHeader("content-type", "application/json");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", $"Bearer {token}");
            request.AddParameter("application/json", string.Empty, ParameterType.RequestBody);
            var response = client.ExecuteAsync(request);
            var resultado = response.Result;
            if (!resultado.IsSuccessful)
            {
                throw new Exception(resultado.Content.ToString());
            }
            return JsonConvert.DeserializeObject<ResponsePixViewModel>(resultado.Content);
        }

        public ResponseAuthViewModel GetPixAuth(string clientId, string clientSecret)
        {
            var client = new RestClient("https://privilege.wallet.corebanx.com/oauth/token")
            {
                Timeout = -1
            };
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddHeader("Accept", "application/json");
            var body = new RequestAuthViewModel
            {
                ClientId = clientId,
                ClientSecret = clientSecret,
                GrantType = "client_credentials"
            };
            //request.AddHeader("Authorization", $"Basic {Convert.ToBase64String(Encoding.ASCII.GetBytes($"{apiKey}{apiSecret}"))}");
            request.AddParameter("application/json", JsonConvert.SerializeObject(body), ParameterType.RequestBody);
            var result = client.Execute(request);
            if (result.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(result.Content.ToString());
            }
            return JsonConvert.DeserializeObject<ResponseAuthViewModel>(result.Content);
        }
    }
}
