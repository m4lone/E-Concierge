using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using qodeless.domain.Enums;
using System;


namespace qodeless.domain.Entities
{
    public class Device : Entity
    {
        public string Code { get; set; }
        public string SerialNumber { get; set; }
        public EDeviceType Type { get; set; }
        public EDeviceStatus Status { get; set; }
        public EDeviceAvailability Availability { get; set; }
        public Device(Guid id) { Id = id; }
        public string MacAddress { get; set; }

        public Device(string code, string serialNumber, EDeviceType type, EDeviceStatus status, EDeviceAvailability availability, string macAddress)
        {
            Code = code;
            SerialNumber = serialNumber;
            Type = type;
            Status = status;
            Availability = availability;
            MacAddress = macAddress;            
        }

        //public void Configure(EntityTypeBuilder<Device> builder)
        //{
        //    builder.HasKey(c => c.Id);
        //    builder.Property(x => x.Id).IsRequired();
        //    builder.Property(x => x.Code).IsRequired();
        //    builder.Property(x => x.Type).IsRequired();
        //    builder.Property(x => x.Status).IsRequired();
        //}
    }
    public static class DeviceDbBuilder /*Fluent Class*/
    {
        public static EntityTypeBuilder<TEntity> ConfigureUnique<TEntity>(this EntityTypeBuilder<TEntity> self, ModelBuilder modelBuilder) where TEntity : Device
        {
            self.HasKey(c => c.Id);
            self.Property(x => x.Id).IsRequired();
            self.Property(x => x.Code).IsRequired();
            self.Property(x => x.Type).IsRequired();
            self.Property(x => x.Status).IsRequired();

            modelBuilder.Entity<TEntity>().HasIndex(p => new { p.Code,p.SerialNumber,p.MacAddress }).IsUnique();
            return self;
        }
    }
}
