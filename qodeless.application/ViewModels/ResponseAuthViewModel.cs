
using Newtonsoft.Json;
using System;

namespace qodeless.application.ViewModels
{
    public class ResponseAuthViewModel
    {
        [JsonProperty("expires_in")]
        public string ExpiresIn { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }
}



