using qodeless.domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace qodeless.application.ViewModels
{
    public class PlayViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid SessionDeviceId { get; set; }
        public int AmountPlay { get; set; }
        public int AmountExtraball { get; set; }
        public Guid DeviceId { get; set; }
        public Guid GameId { get; set; }
        public Guid SiteId { get; set; }
        public string UserPlayId { get; set; }
        public string Email { get; set; }
        public string LastIpAdress { get; set; }
        public string DeviceCode { get; set; }
        public string GameName { get; set; }
        public string SiteName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
