using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qodeless.application.ViewModels
{
    public class SiteGameMutipleVm
    {
        public Guid Site { get; set; }
        public List<Guid> GameIds { get; set; }
    }
}
