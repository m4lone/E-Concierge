using qodeless.application.ViewModels;
using qodeless.domain.Entities;
using qodeless.domain.Model;
using System.Collections.Generic;

namespace qodeless.presentation.WebApp.Models
{
    public class PartnerDevicesViewModel
    {
        public string SiteName { get; set; }
        public List<Device> Devices { get; set; } = new List<Device>();
    }
}
