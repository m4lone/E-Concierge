using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using qodeless.domain.Enums;

namespace qodeless.domain.Entities
{
    public class Currency : Entity, IEntityTypeConfiguration<Currency>
    {
        public double VlrToBRL { get; set; }
        public ECurrencyCode Code { get; set; }
        public Currency(Guid id) { Id = id; }

        public Currency(double vlrtobrl, ECurrencyCode code)
        {
            VlrToBRL = vlrtobrl;
            Code = code;
        }

        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.VlrToBRL).IsRequired();
            builder.Property(x => x.Code).IsRequired();

        }
    }
}