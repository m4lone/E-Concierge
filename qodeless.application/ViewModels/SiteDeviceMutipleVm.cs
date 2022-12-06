using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qodeless.application.ViewModels
{
    public class SiteDeviceMultipleVm
    {
        public Guid Site { get; set; }
        public List<Guid> DeviceIds { get; set; }
    }
}
