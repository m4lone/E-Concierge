using Newtonsoft.Json;
using qodeless.domain.Enums;
using System;
using System.Collections.Generic;

namespace qodeless.application.ViewModels
{
    public class GamePlayerSummary
    {
        [JsonProperty("AccountId")]
        public Guid AccountId { get; set; }

        [JsonProperty("SiteId")]
        public Guid SiteId { get; set; }

        [JsonProperty("AccountStatus")]
        public EAccountStatus AccountStatus { get; set; }

        [JsonProperty("IsUserBlocked")]
        public bool IsUserBlocked { get; set; }

        [JsonProperty("Amount")]
        public int Amount { get; set; }

        [JsonProperty("Devices")]
        public List<Device> Devices { get; set; }

        public class Device
        {
            [JsonProperty("Id")]
            public Guid Id { get; set; }

            [JsonProperty("SerialNumber")]
            public string SerialNumber { get; set; }

            [JsonProperty("GameId")]
            public Guid GameId { get; set; }

            [JsonProperty("GameName")]
            public string GameName { get; set; }

            [JsonProperty("GameType")]
            public EGameType GameType { get; set; }
        }
    }
}
