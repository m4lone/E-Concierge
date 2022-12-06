using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using qodeless.domain.Entities;
using Qodeless.Domain.Enum;
using System;

namespace qodeless.domain.Entities
{
    public class Order : Entity
    {

        public DateTime Date { get; set; }
        public float Value { get; set; }
        public decimal Fee { get; set; }
        public EOrderStatus EOrderStatus { get; set; }
        public string PixId { get; set; }
        public string QrCode { get; set; }
        public string UserId { get; set; }
        public string Code { get; set; }
        public virtual Site Site { get; set; }
        public Guid? SiteId { get; set; }
        public virtual Account Account { get; set; }
        public Guid? AccountId { get; set; }
    }
    public static class OrderDbBuilder /*Fluent Class*/
    {
        public static EntityTypeBuilder<TEntity> ConfigureUnique<TEntity>(this EntityTypeBuilder<TEntity> self, ModelBuilder modelBuilder) where TEntity : Order
        {
            self.HasKey(c => c.Id);
            self.Property(x => x.Id).IsRequired();
            self.Property(x => x.Date).IsRequired();
            self.Property(x => x.Value).IsRequired();
            self.Property(x => x.PixId).IsRequired();
            self.Property(x => x.Code).IsRequired();

            modelBuilder.Entity<TEntity>().HasIndex(p => new { p.Code,p.PixId }).IsUnique();
            return self;
        }
    }
}
