using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace qodeless.domain.Entities
{
    public class Voucher : Entity
    {
        public string UserOperationId { get; set; }
        public string QrCodeKey { get; set; }
        public string QrCodeSecret { get; set; }
        public DateTime DueDate { get; set; }
        public double Amount { get; set; }
        public bool IsActive { get; set; }
        public virtual Site Site {get; set; }
        public  Guid SiteID { get; set; }
        public Voucher(Guid id) { Id = id; }
        public Voucher(string userOperationId, string qrCodeSecret, string qrCodeKey, DateTime dueDate, double amount, Guid siteId)
        {
            UserOperationId = userOperationId;
            QrCodeKey = qrCodeKey;
            QrCodeSecret = qrCodeSecret;
            DueDate = dueDate;
            Amount = amount;
            SiteID = siteId;
            IsActive = true;
        }

        public Voucher()
        {
        } 
    }

    public static class VoucherDbBuilder /*Fluent Class*/
    {
        public static EntityTypeBuilder<TEntity> ConfigureUnique<TEntity>(this EntityTypeBuilder<TEntity> self, ModelBuilder modelBuilder) where TEntity : Voucher
        {
            self.HasKey(c => c.Id);
            self.Property(x => x.Id).IsRequired();
            self.Property(x => x.UserOperationId).IsRequired();
            self.Property(x => x.SiteID).IsRequired();
            self.Property(x => x.QrCodeKey).IsRequired();
            self.Property(x => x.DueDate).IsRequired();
            self.Property(x => x.Amount).IsRequired();

            modelBuilder.Entity<TEntity>().HasIndex(p => new { p.QrCodeKey }).IsUnique();
            return self;
        }
    }
}