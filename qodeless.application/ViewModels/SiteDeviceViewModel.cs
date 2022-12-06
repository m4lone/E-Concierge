using qodeless.domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qodeless.application.ViewModels
{
    public class SiteDeviceViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid SiteId { get; set; }
        public Guid DeviceId { get; set; }
        public string SiteName { get; internal set; }
        public string DeviceName { get; internal set; }
        public string Name { get; set; }
    }
}

