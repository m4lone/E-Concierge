using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using qodeless.domain.Enums;
using System;

namespace qodeless.domain.Entities
{
    public class SessionSite : Entity, IEntityTypeConfiguration<SessionSite>
    {
        public string UserOperationId { get; set; }
        public DateTime DtBegin { get; set; }
        public DateTime DtEnd { get; set; }
        public EStatusSessionSite Status { get; set; }
        public virtual Site Site { get; set; }
        public Guid SiteId { get; set; }        
        public SessionSite(Guid id) { Id = id; }

        public SessionSite(string userOperationId, DateTime dtBegin, DateTime dtEnd, EStatusSessionSite status, Guid siteId)
        {
            UserOperationId = userOperationId;
            DtBegin = dtBegin;
            DtEnd = dtEnd;
            Status = status;
            SiteId = siteId;
        }

        public void Configure(EntityTypeBuilder<SessionSite> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.UserOperationId).IsRequired();
            builder.Property(x => x.DtBegin).IsRequired();
            builder.Property(x => x.DtEnd).IsRequired();
        }
    }
}
