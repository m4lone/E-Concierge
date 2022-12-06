using qodeless.domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qodeless.services.WebApi.Model
{
    public class RoleViewModel
    {
        public string Role { get; set; }
        public List<ClaimViewModel> Claims { get; set; }
    }
}
