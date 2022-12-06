using System;
using System.Collections.Generic;

namespace qodeless.messaging.Worker.ViewModels.Request
{
    public class PlayRequest
    {
        public Guid SessionDeviceId { get; set; }
        public Guid DeviceId { get; set; }
        public Guid SiteId { get; set; }
        public string UserPlayId { get; set; }
        public List<LocalPlay> LocalPlays { get; set; }

        public PlayRequest(Guid sessionDeviceId, Guid deviceId, Guid siteId, string userPlayId, List<LocalPlay> localPlays)
        {
            SessionDeviceId = sessionDeviceId;
            DeviceId = deviceId;
            SiteId = siteId;
            UserPlayId = userPlayId;
            LocalPlays = localPlays;
        }
    }

    public class LocalPlay
    {
        public LocalPlay(int rowId, int amountPlay, int amountExtraball, DateTime createdAt)
        {
            RowId = rowId;
            AmountPlay = amountPlay;
            AmountExtraball = amountExtraball;
            CreatedAt = createdAt;
        }
        public int RowId { get; set; }
        public int AmountPlay { get; set; }
        public int AmountExtraball { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}