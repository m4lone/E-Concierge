using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace qodeless.domain.Entities
{
    public class Group : Entity, IEntityTypeConfiguration<Group>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public Group(Guid id) { Id = id; }
        public int AcceptanceCriteria { get; set; }

        public Group(string code, string name, int acceptanceCriteria)
        {
            Code = code;
            Name = name;
            AcceptanceCriteria = acceptanceCriteria;
        }

        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Code).IsRequired();
            builder.Property(x => x.AcceptanceCriteria).IsRequired();
        }
    }
}