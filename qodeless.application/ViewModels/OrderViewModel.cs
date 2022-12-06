using Qodeless.Domain.Enum;
using System;
using System.ComponentModel.DataAnnotations;

namespace qodeless.application.ViewModels
{
    public class OrderViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid? SiteId { get; set; }
        public Guid? AccountId { get; set; }
        public DateTime Date { get; set; }
        public float Value { get; set; }
        public decimal Fee { get; set; }
        public EOrderStatus EOrderStatus { get; set; }
        public string UserId { get; set; }
        public string PixId { get; set; }
        public string QrCode { get; set; }
        public string Code { get; set; }
    }
}
