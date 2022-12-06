using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using qodeless.domain.Enums;

namespace qodeless.domain.Entities
{
    public class Account : Entity, IEntityTypeConfiguration<Account>
    {
        public string Email { get; set; }
        public string CellPhone { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Royalties { get; set; }
        public string UserId { get; set; } //AspNetUserId
        public virtual Account SubAccount { get; set; }
        public Guid? SubAccountId { get; set; }
        public EAccountStatus Status { get; set; }
        public EActivityStatus ActivityStatus { get; set; }
        public Account(Guid id) { Id = id; }
        public Account(string email,string cellPhone, string name, string description, EAccountStatus status, EActivityStatus activityStatus)
        {
            Email = email;
            CellPhone = cellPhone;
            Name = name;
            Description = description;
            Status = status;
            ActivityStatus = activityStatus;
        }

        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Status).IsRequired();
        }
    }
}