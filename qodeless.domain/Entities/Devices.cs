using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using qodeless.domain.Enums;
using System;


namespace qodeless.domain.Entities
{
    public class Devices : Entity
    {
        public string DeviceSn { get; set; }
        public Devices(Guid id) { Id = id; }
        public Guid? RoomId { get; set; }

        public Devices(string deviceSn)
        {
            DeviceSn = deviceSn;   
        }

    }
    public static class DevicesDbBuilder /*Fluent Class*/
    {
        public static EntityTypeBuilder<TEntity> ConfigureUnique<TEntity>(this EntityTypeBuilder<TEntity> self, ModelBuilder modelBuilder) where TEntity : Devices
        {
            self.HasKey(c => c.Id);
            self.Property(x => x.Id).IsRequired();
            self.Property(x => x.DeviceSn).IsRequired();
            self.Property(x => x.RoomId).IsRequired();

            modelBuilder.Entity<TEntity>().HasIndex(p => new { p.DeviceSn}).IsUnique();
            return self;
        }
    }
}
