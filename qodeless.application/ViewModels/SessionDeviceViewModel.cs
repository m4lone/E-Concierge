using qodeless.domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace qodeless.application.ViewModels
{
    public class SessionDeviceViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public string UserPlayId { get; set; }
        public DateTime DtBegin { get; set; }
        public DateTime DtEnd { get; set; }
        public string LastIpAddress { get; set; }
        public Guid DeviceId { get; set; }
        public string DeviceName { get; internal set; }
    }
}
