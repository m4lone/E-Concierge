using Newtonsoft.Json;
using qodeless.domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace qodeless.application.ViewModels
{
    public class RequestViewModel
    {
        [JsonProperty("value")]
        public float Value { get; set; }
        [JsonProperty("account_number")]
        public string AccountNumber { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}





