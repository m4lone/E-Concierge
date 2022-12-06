using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace qodeless.domain.Entities
{
    public class RequestAuth : Entity, IEntityTypeConfiguration<RequestAuth>
    {

        public string Username { get; set; }
        public string Password { get; set; }

        public RequestAuth(Guid id) { Id = id; }

        public RequestAuth(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public void Configure(EntityTypeBuilder<RequestAuth> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Username).IsRequired();
            builder.Property(x => x.Password).IsRequired();
        }
    }
}
