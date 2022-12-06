using Microsoft.AspNetCore.Identity;
using qodeless.domain.Enums;
using System;

namespace qodeless.Infra.CrossCutting.Identity.Entities
{
    public class ApplicationUser : IdentityUser
    {
        #region Custom properties
        public DateTime CreationDate { get; set; }
        public bool Enabled { get; set; }   
        public string NickName { get; set; }
        public DateTime BirthDate { get; set; }
        public string FullName { get; set; }
        public string Cpf { get; set; }
        public string PixKey { get; set; }
        public bool RegisterCompleted { get; set; }
        public EGender Gender { get; set; }
        public Guid? EmployerId { get; set; }
        #endregion
    }
}
