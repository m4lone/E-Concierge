using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace qodeless.domain.Entities
{
    public class Play : Entity, IEntityTypeConfiguration<Play>
    {
        public virtual SessionDevice SessionDevice { get; set; }
        public Guid SessionDeviceId { get; set; }
        public int AmountPlay { get; set; }
        public int  AmountExtraball { get; set; }
        public string UserPlayId { get; set; }         
        public virtual Device Device { get; set; }
        public Guid DeviceId { get; set; }
        public virtual Game Game { get; set; }
        public Guid GameId { get; set; } 
        public virtual Site Site { get; set; }
        public Guid SiteId { get; set; }
        public Play(Guid id) { Id = id; }
       
        public Play(Guid  sessiondeviceid, int amountplay, int amountextraball, string userplayid, Guid deviceid, Guid gameid,Guid siteid)
        {
            SessionDeviceId = sessiondeviceid;
            AmountPlay = amountplay;
            AmountExtraball = amountextraball;
            UserPlayId = userplayid;
            DeviceId = deviceid;
            GameId = gameid;
            SiteId = siteid;       
        }

        public void Configure(EntityTypeBuilder<Play> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.SessionDeviceId).IsRequired();
            builder.Property(x => x.AmountPlay).IsRequired();
            builder.Property(x => x.AmountExtraball).IsRequired();
            builder.Property(x => x.UserPlayId).IsRequired();
            builder.Property(x => x.DeviceId).IsRequired();
            builder.Property(x => x.GameId).IsRequired();
            builder.Property(x => x.SiteId).IsRequired();
        }
    }
}
