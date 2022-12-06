using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using qodeless.domain.Enums;

namespace qodeless.domain.Entities
{
    public class VirtualBalance : Entity, IEntityTypeConfiguration<VirtualBalance>
    {
        public string UserPlayId { get; set; }
        public string UserOperationId { get; set; }
        public EBalanceType Type { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
        public Guid? VoucherId { get; set; }
        public virtual Voucher Voucher { get; set; }
        public VirtualBalance(Guid id) { Id = id; }
        public EBalanceState BalanceState { get; set; }
        public VirtualBalance(string userOperationId, EBalanceType type, double amount, string description, Guid? voucherId)
        {
            UserOperationId = userOperationId;
            Type = type;
            Amount = amount;
            Description = description;
            VoucherId = voucherId;
            BalanceState = EBalanceState.Pending;
        }

        public VirtualBalance()
        {
        }

        public void Configure(EntityTypeBuilder<VirtualBalance> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Type).IsRequired();
            builder.Property(x => x.Amount).IsRequired();
            builder.Property(x => x.BalanceState).IsRequired();
        }
    }
}
