using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using qodeless.domain.Enums;
using System;

namespace qodeless.domain.Entities
{
    public class DeviceGame : Entity, IEntityTypeConfiguration<DeviceGame>
    {
        public virtual Device Device { get; set; }
        public Guid DeviceId { get; set; }        
        public virtual Game Game { get; set; }
        public Guid GameId { get; set; }
        public DeviceGame(Guid id) { Id = id; }

        public DeviceGame(Guid deviceId, Guid gameId)
        {
            DeviceId = deviceId;

            GameId = gameId;
        }

        public void Configure(EntityTypeBuilder<DeviceGame> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.DeviceId).IsRequired();
            builder.Property(x => x.GameId).IsRequired();
        }
    }
}
