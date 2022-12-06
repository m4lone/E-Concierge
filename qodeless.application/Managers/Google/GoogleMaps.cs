using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using qodeless.application.ViewModels;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qodeless.application.Managers.Google
{
    public class GoogleMaps
    {
        private IConfigurationRoot _config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
        //para tirar dúvidas sobre as requisições, consulte o link abaixo.
        //https://cloud.google.com/apis/docs/overview
        public GoogleMapsApiViewModel GetByFullAddress(int number, string district, string state, string city, string address)
        {
            var client = new RestClient($"https://maps.googleapis.com/maps/api/geocode/json?" +
                $"key={_config.GetSection("google:key").Value}" +
                $"&address={number},{address} {district},{city}-{state}");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<GoogleMapsApiViewModel>(response.Content);
        }
        public GoogleMapsApiViewModel GetAddressByLatLng(decimal lat, decimal lng)
        {
            var client = new RestClient("https://maps.googleapis.com/maps/api/geocode/json?" +
                $"latlng={lat},{lng}" +
                $"&key={_config.GetSection("google:key").Value}");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<GoogleMapsApiViewModel>(response.Content);
        }
    }
}
