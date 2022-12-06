using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using qodeless.domain.Enums;
using System;
using System.Collections.Generic;

namespace qodeless.domain.Entities
{
    public class Site : Entity
    {
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
        public string PixUser { get; set; }
        public string PixToken { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public double Royalties { get; set; }
        public virtual Account Account { get; set; }
        public Guid AccountId { get; set; }
        public string UserId { get; set; } //AspNetUserId
        public string CellPhone { get; set; } //AspNetUserId
        public string Email { get; set; } //AspNetUserId
        public  ESiteType ESiteType { get; set; }
        public List<int> GameCurrencies { get; set; }
        public Site(Guid id) { Id = id; }

        public Site(string name, string description, string zipCode, string address, int number, string city, string state, string country, string code, string pixToken, string pixUser, Guid accountId, ESiteType eSiteType, string cellPhone, string email)
        {
            Name = name;
            Description = description;
            ZipCode = zipCode;
            Address = address;
            Number = number;
            City = city;
            State = state;
            Country = country;
            AccountId = accountId;
            ESiteType = eSiteType;
            Code = code;
            PixUser = pixUser;
            PixToken = pixToken;
            Email = email;
            CellPhone = cellPhone;
        }
    }
    public static class SiteDbBuilder /*Fluent Class*/
    {
        public static EntityTypeBuilder<TEntity> ConfigureUnique<TEntity>(this EntityTypeBuilder<TEntity> self, ModelBuilder modelBuilder) where TEntity : Site
        {
            self.HasKey(c => c.Id);
            self.Property(x => x.Id).IsRequired();
            self.Property(x => x.Name).IsRequired();
            self.Property(x => x.Code).IsRequired();

            modelBuilder.Entity<Site>().HasIndex(p => new { p.Code }).IsUnique();
            return self;
        }
    }
}
