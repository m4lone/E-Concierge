using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace qodeless.domain.Entities
{
    public class SitePlayer : Entity, IEntityTypeConfiguration<SitePlayer>
    {
        public virtual Site Site { get; set; }
        public Guid SiteId { get; set; }
        public string UserPlayerId { get; set; }
        public int CreditAmount { get; set; }
        public SitePlayer(Guid id) { Id = id; }

        public SitePlayer(Guid siteId, string userPlayId)
        {
            SiteId = siteId;
            UserPlayerId = userPlayId;
        }

        public SitePlayer(Guid siteId, string userPlayerId, int creditAmount) : this(siteId, userPlayerId)
        {
            CreditAmount = creditAmount;
        }

        public void Configure(EntityTypeBuilder<SitePlayer> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.UserPlayerId).IsRequired();
            builder.Property(x => x.SiteId).IsRequired();
        }
    }
}
