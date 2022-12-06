using qodeless.domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace qodeless.application.ViewModels
{
    public class SiteViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string ZipCode { get; set; }
        public string Address { get; set; }
        public int Number { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public Guid AccountId { get; set; }
        public ESiteType ESiteType { get; set; }
        public int TotalSites { get; set; }
        public double Royalties { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; } //AspNetUserId
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}
