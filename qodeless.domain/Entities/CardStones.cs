using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using qodeless.domain.Enums;

namespace qodeless.domain.Entities
{
    public class CardStones : Entity, IEntityTypeConfiguration<CardStones>
    {
        public CardStones(Guid id) { Id = id; }
        public int CardNumber { get; set; }
        public string StoneNumbersList { get; set; } //Example JSON: "1,20,42,60,3,7,87,13,4,6,9,4,2,36,25"
        public virtual Game Game { get; set; }
        public Guid GameId { get; set; }

        public CardStones(int cardNumber, string stoneNumbersList, Guid gameId)
        {
            CardNumber = cardNumber;
            StoneNumbersList = stoneNumbersList;
            GameId = gameId;
        }
        public void Configure(EntityTypeBuilder<CardStones> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.GameId).IsRequired();
            builder.Property(x => x.CardNumber).IsRequired();
        }
    }
}
