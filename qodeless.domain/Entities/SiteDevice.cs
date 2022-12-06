using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace qodeless.domain.Entities
{
    public class SiteDevice : Entity, IEntityTypeConfiguration<SiteDevice>
    {
        public virtual Site Site { get; set; }
        public Guid SiteId { get; set; }
        public virtual Device Device { get; set; }
        public Guid DeviceId { get; set; }
        public SiteDevice(Guid id) { Id = id; }

        public SiteDevice(Guid siteId, Guid deviceId) 
        {
            SiteId = siteId;
            DeviceId = deviceId;
        }

        public void Configure(EntityTypeBuilder<SiteDevice> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.SiteId).IsRequired();
            builder.Property(x => x.DeviceId).IsRequired();
        }
    }
}