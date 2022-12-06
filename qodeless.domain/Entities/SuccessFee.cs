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
    public class SuccessFee : Entity, IEntityTypeConfiguration<SuccessFee>
    {
        public virtual Account Account { get; set; }
        public Guid AccountId { get; set; }
        public string UserId { get; set; }
        public double Rate { get; set; }
        public EFeeType Type { get; set; }
        public Guid ParentUserId { get; set; }
        public Guid ParentSiteId { get; set; }
        public SuccessFee(Guid id) { Id = id; }
        //public virtual Site Site { get; set; }
        public Guid SiteId { get; set; }

        public SuccessFee(Guid accountId, string userId, double rate, EFeeType type, Guid parentUserId, Guid parentSiteId, Guid siteId  )
        {
            AccountId = accountId;
            UserId = userId;
            Rate = rate;
            Type = type;
            ParentUserId = parentUserId;
            ParentSiteId = parentSiteId;
            SiteId = siteId;
        }

        public void Configure(EntityTypeBuilder<SuccessFee> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.Rate).IsRequired();
            builder.Property(x => x.Type).IsRequired();
        }
    }   
}
