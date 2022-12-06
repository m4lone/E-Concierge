using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qodeless.services.WebApi.Model
{
    public class LoginUnityResponseVM
    {
        public string NickName { get; set; }
        public string UserId { get; set; }
        public string PhoneNumber { get; set; }
        public bool RegisterCompleted { get; set; }
        public List<RecSitesUnityResponseVM> RecSites { get; set; } = new List<RecSitesUnityResponseVM>();

    }
}
