using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace qodeless.application.ViewModels
{
    public class RequestAuthViewModel
    {
        [Key]
        [JsonProperty("client_id")]
        public string ClientId { get; set; }
        [JsonProperty("client_secret")]
        public string ClientSecret { get; set; }
        [JsonProperty("grant_type")]
        public string GrantType { get; set; }
    }
}





