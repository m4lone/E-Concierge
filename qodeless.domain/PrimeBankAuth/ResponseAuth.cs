using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace qodeless.domain.Entities
{
    public class ResponseAuth : Entity, IEntityTypeConfiguration<ResponseAuth>
    {
        
        public string Token { get; set; }
        public string Type { get; set; }
        public DateTime ExpiresIn { get; set; }

        public ResponseAuth(Guid id) { Id = id; }

        public ResponseAuth(string token, string type, DateTime expiresIn)
        {
            Token = token;
            Type = type;
            ExpiresIn = expiresIn;
        }

        public void Configure(EntityTypeBuilder<ResponseAuth> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Token).IsRequired();
            builder.Property(x => x.Type).IsRequired();
        }
    }
}
