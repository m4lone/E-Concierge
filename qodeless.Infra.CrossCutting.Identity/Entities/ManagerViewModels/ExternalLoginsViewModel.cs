using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace qodeless.Infra.CrossCutting.Identity.Entities.ManagerViewModels
{
    public class ExternalLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }

        public bool ShowRemoveButton { get; set; }

        public string StatusMessage { get; set; }
    }
}
