using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using qodeless.domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qodeless.domain.Entities
{
    public class SiteGame : Entity, IEntityTypeConfiguration<SiteGame>
    {
        public virtual Site Site { get; set; }
        public Guid SiteId { get; set; }
        public ESite ESite { get; set; }
        public virtual Game Game { get; set; }
        public Guid GameId { get; set; }
        public SiteGame(Guid id) { Id = id; }

        public SiteGame(Guid siteId, Guid gameId, ESite eSite)
        {
            SiteId = siteId;
            GameId = gameId;
            ESite = eSite;            
        }

        public void Configure(EntityTypeBuilder<SiteGame> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.ESite).IsRequired();
            builder.Property(x => x.SiteId).IsRequired();
            builder.Property(x => x.GameId).IsRequired();
        }
    }
}