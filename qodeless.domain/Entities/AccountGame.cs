using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using qodeless.domain.Enums;

namespace qodeless.domain.Entities
{
    public class AccountGame : Entity, IEntityTypeConfiguration<AccountGame>
    {
        public virtual Account Account { get; set; }
        public Guid AccountId { get; set; }
        public virtual Game Game { get; set; }
        public Guid GameId { get; set; }        
        public AccountGame(Guid id) {  Id = id; }

        public AccountGame(Guid accountId, Guid gameId)
        {            
            AccountId = accountId;
            GameId = gameId;           
        }

        public void Configure(EntityTypeBuilder<AccountGame> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.Id).IsRequired();
           
        }
    }
}