using qodeless.domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace qodeless.application.ViewModels
{
    public class AccountViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; } //AspNetUserId
        public EAccountStatus Status { get; set; }
        public double Royalties { get; set; }
        public Guid? SubAccountId { get; set; }
    }
}
