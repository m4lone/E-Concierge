using qodeless.domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qodeless.services.WebApi.Model
{
    public class UserLockoutViewModel
    {
        public string UserId{ get; set; }
        public bool Lockout { get; set; }
    }

    public class RegisterViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Role { get; set; }
        public List<ClaimViewModel> Claims { get; set; }
    }

    public class RegisterCollaboratorViewModel
    {
        public string Email { get; set; }
        public string Cpf { get; set; }
        public Guid? EmployerId { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
    }
    public class UpdateCollaboratorViewModel
    {
        public string UserId { get; set; }
        public string Cpf { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class RegisterMobileViewModel
    {
        public string Phone { get; set; }
    }

    public class RegisterTokenViewModel
    {
        public Guid SiteId { get; set; }
        public string Phone { get; set; }
        public string Token { get; set; }
    }

    public class TokenMobileViewModel
    {
        public string Phone { get; set; }
    }
}