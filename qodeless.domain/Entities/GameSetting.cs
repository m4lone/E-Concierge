using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qodeless.domain.Entities
{
    public class GameSetting : Entity
    {
        public decimal ExtraBallValue { get; set; }
        public decimal BallValue { get; set; }
        public virtual Game Game { get; set; }
        public Guid GameId { get; set; }
    }

    public static class GameSettingDbBuilder /*Fluent Class*/
    {
        public static EntityTypeBuilder<TEntity> ConfigureUnique<TEntity>(this EntityTypeBuilder<TEntity> self, ModelBuilder modelBuilder) where TEntity : GameSetting
        {
            self.HasKey(c => c.Id);
            self.Property(x => x.Id).IsRequired();
            self.Property(x => x.BallValue).IsRequired();

            return self;
        }
    }
}
