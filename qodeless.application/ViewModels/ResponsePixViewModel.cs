using Newtonsoft.Json;
using System;

namespace qodeless.application.ViewModels
{
    public class Refund
    {
        [JsonProperty("refund_date")]
        public DateTime RefundDate { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public class ResponsePixViewModel
    {
        [JsonProperty("pix_id")]
        public string PixId { get; set; }

        [JsonProperty("type")]
        public object Type { get; set; }

        [JsonProperty("paid")]
        public bool Paid { get; set; }

        [JsonProperty("base64")]
        public object Base64 { get; set; }

        [JsonProperty("qrcode")]
        public string Qrcode { get; set; }

        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("expiration_date")]
        public DateTime ExpirationDate { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("is_refunded")]
        public bool IsRefunded { get; set; }

        [JsonProperty("refund")]
        public Refund Refund { get; set; }
        public string Code { get; set; }
        public string Serie { get; set; }
    }
}
