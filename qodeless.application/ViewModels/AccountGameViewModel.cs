using qodeless.domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace qodeless.application.ViewModels
{
    public class AccountGameViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public Guid GameId { get; set; }
        public string GameName { get; internal set; }
        public string AccountName { get; internal set; }
    }
}
