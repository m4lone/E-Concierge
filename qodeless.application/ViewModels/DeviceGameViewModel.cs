using qodeless.domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace qodeless.application.ViewModels
{
    public class DeviceGameViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid DeviceId { get; set; }
        public Guid GameId { get; set; }
        public string DeviceName { get; internal set; }
        public string GameName { get; internal set; }
    }
}
