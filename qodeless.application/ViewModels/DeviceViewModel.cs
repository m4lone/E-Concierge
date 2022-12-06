using qodeless.domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace qodeless.application.ViewModels
{
    public class DeviceViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string SerialNumber { get; set; }
        public EDeviceType Type { get; set; }
        public EDeviceStatus Status { get; set; }
        public string TextoStatus { get; set; }
        public EDeviceAvailability Availability { get; set; }
        public string MacAddress { get; set; }
        public int TotalDevices { get; set; }
    }
}
