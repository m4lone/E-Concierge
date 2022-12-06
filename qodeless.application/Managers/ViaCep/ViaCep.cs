using Newtonsoft.Json;
using qodeless.application.ViewModels;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qodeless.application.Managers.ViaCep
{
    public class ViaCep
    {
        //para tirar dúvidas sobre as requisições, consulte no link abaixo.
        //https://viacep.com.br
        public AddressViewModel GetAddressByZipCode(string zipCode)
        {
            var client = new RestClient($"https://viacep.com.br/ws/{zipCode}/json/unicode/");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<AddressViewModel>(response.Content);
        }
        public List<AddressViewModel> GetZipCodeByAddress(string state, string city, string address)
        {
            var client = new RestClient($"https://viacep.com.br/ws/{state}/{city}/{address}/json/");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<List<AddressViewModel>>(response.Content);
        }
    }
}
