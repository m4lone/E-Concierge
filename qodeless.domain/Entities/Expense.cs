using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using qodeless.domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qodeless.domain.Entities
{
    public class Expense : Entity, IEntityTypeConfiguration<Expense>
    {

        public double Amount { get; set; }
        public string Description { get; set; }
        public EIncomeType Type { get; set; }
        public Guid? AccountId { get; set; }
        public virtual Account Account { get; set; }
        public Guid? SiteId { get; set; }
        public virtual Site Site { get; set; }
        public Expense(Guid id) { Id = id; }

        public Expense(double amount, string description, EIncomeType type, Guid? accountId, Guid? siteId)
        {
            Amount = amount;
            Description = description;
            Type = type;
            AccountId = accountId;
            SiteId = siteId;
        }

        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Amount).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.Type).IsRequired();
        }
    } 
}
