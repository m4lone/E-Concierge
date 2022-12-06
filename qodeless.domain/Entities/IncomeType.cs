using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace qodeless.domain.Entities
{
    public class IncomeType : Entity, IEntityTypeConfiguration<IncomeType>
    {
        public string Name { get; set; }
        public int Code { get; set; }
        public IncomeType(Guid id) { Id = id; }

        public IncomeType ( string name, int code)
        {
           Name = name;
           Code = code;
        }

        public void Configure(EntityTypeBuilder<IncomeType> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Code).IsRequired();
        }
    }
}