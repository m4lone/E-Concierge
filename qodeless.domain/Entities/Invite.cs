using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace qodeless.domain.Entities
{
    public class Invite : Entity
    {
        public string InviteToken { get; set; }
        public virtual Site Site { get; set; }
        public Guid SiteId { get; set; }
        public DateTime DtExpiration { get; set; }
        public bool IsActive { get; set; }
        public Invite(Guid id) { Id = id; IsActive = true; }
        public Invite(string inviteToken, Guid siteId, DateTime dtExpiration)
        {
            InviteToken = inviteToken;
            SiteId = siteId;
            DtExpiration = dtExpiration;
            IsActive = true;
        }
    }

    public static class InviteDbBuilder /*Fluent Class*/
    {
        public static EntityTypeBuilder<TEntity> ConfigureUnique<TEntity>(this EntityTypeBuilder<TEntity> self, ModelBuilder modelBuilder) where TEntity : Invite
        {
            self.HasKey(c => c.Id);
            self.Property(x => x.InviteToken).IsRequired();

            modelBuilder.Entity<Invite>().HasIndex(p => new { p.InviteToken }).IsUnique();
            return self;
        }
    }
}
