using qodeless.domain.Entities;
using qodeless.domain.Model;
using System;
using System.Collections.Generic;

namespace qodeless.Infra.CrossCutting.Identity.DataModel
{
    public class UserDataModel : IUserDataModel
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public Guid SiteId { get; set; }
        public Guid AccountId { get; set; }
        public List<ClaimViewModel> Claims { get; set; } = new List<ClaimViewModel>();
        public string ClaimsType { get; set; }
        public string ClaimsValue { get; set; }
    }
}
