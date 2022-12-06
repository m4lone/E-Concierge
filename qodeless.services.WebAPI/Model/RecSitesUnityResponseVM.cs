using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qodeless.services.WebApi.Model
{
    public class RecSitesUnityResponseVM
    {
        public string Name { get; set; }
        public Guid AccountId { get; set; }
        public int CreditAmount { get; set; }
        public DateTime LastAccessAt { get; set; }
        public Guid Id { get; set; }
        public string Code { get; set; }
    }
}
