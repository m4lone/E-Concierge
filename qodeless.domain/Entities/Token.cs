using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace qodeless.domain.Entities
{
    public class Token : Entity, IEntityTypeConfiguration<Token>
    {
        public string Code { get; set; }
        public string Phone { get; set; }
        public DateTime DtExpiration { get; set; }
        public Token(Guid id) { Id = id; }

        public Token(string code, string phone, DateTime dtExpiration)
        {
            Code = code;
            Phone = phone;
            DtExpiration = dtExpiration;
        }

        public void Configure(EntityTypeBuilder<Token> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.DtExpiration).IsRequired();
            builder.Property(x => x.Phone).IsRequired();

        }
    }
}