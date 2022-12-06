using qodeless.domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace qodeless.application.ViewModels
{
    public class VirtualBalanceViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid? VoucherId { get; set; }
        public string UserPlayId { get; set; }
        public string UserOperationId { get; set; }
        public EBalanceType Type { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
        public double TotalBalances { get; set; }
        public EBalanceState? BalanceState { get; set; }
    }

    public class VirtualBalanceResponse
    {
        public double Amount { get; set; }
    }
}
