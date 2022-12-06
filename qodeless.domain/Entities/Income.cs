using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using qodeless.domain.Enums;
using System;

namespace qodeless.domain.Entities
{
    public class Income : Entity, IEntityTypeConfiguration<Income>
    {
        public EIncomeType Type { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
        public virtual Account Account { get; set; }
        public Guid? AccountId { get; set; }
        public virtual Site Site { get; set; }
        public Guid? SiteId { get; set; }
        public Income(Guid id) { Id = id; }

        public Income(EIncomeType type, double amount, string description, Guid? accountId, Guid? siteId)
        {
            Type = type;
            Amount = amount;
            Description = description;
            AccountId = accountId;
            SiteId = siteId;
        }

        public void Configure(EntityTypeBuilder<Income> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.Type).IsRequired();
            builder.Property(x => x.Amount).IsRequired();
            builder.Property(x => x.Description).IsRequired();
        }
    }
}