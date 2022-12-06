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
    public class Exchange : Entity
    {
        public string Note { get; set; }
        public double Value { get; set; }
        public EExchangeStatus ExchangeStatus { get; set; }
        public DateTime DateConcluded { get; set; }
        public virtual Account Account { get; set; }
        public Guid AccountId { get; set; }
      
        public Exchange(Guid id) { Id = id; }


        public Exchange(string note, double value, Guid accountId, DateTime dateConclued, EExchangeStatus exchangeStatus)
        {
            Note = note;
            Value = value;
            AccountId = accountId;
            DateConcluded = dateConclued;
            ExchangeStatus = exchangeStatus;
        }

        public void Configure(EntityTypeBuilder<Exchange> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Note).IsRequired();
        }
    } 
}
