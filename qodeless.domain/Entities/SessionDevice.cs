using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace qodeless.domain.Entities
{
    public class SessionDevice : Entity, IEntityTypeConfiguration<SessionDevice>
    {
        public SessionDevice(Guid id) { Id = id; }
        public string UserPlayId { get; set; }
        public DateTime DtBegin { get; set; }
        public DateTime DtEnd { get; set; }
        public virtual Device Device { get; set; }
        public Guid DeviceId { get; set; }
        public string LastIpAddress { get; set; }

        public void Configure(EntityTypeBuilder<SessionDevice> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.UserPlayId).IsRequired();
            builder.Property(x => x.DtBegin).IsRequired();
            builder.Property(x => x.DtEnd).IsRequired();
        }
    }
}
