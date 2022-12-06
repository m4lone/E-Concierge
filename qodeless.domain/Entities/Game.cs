using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using qodeless.domain.Enums;

namespace qodeless.domain.Entities
{
    public class Game : Entity
    {
        public EGameCode Code { get; set; }
        public string Name { get; set; }
        public DateTime DtPublish { get; set; }
        public EGameStatus Status { get; set; }
        public EGameType Type { get; set; }
        public Game(Guid id) { Id = id; }
        public string Version { get; set; }

        public Game(EGameCode code, string name, DateTime dtPublish, EGameStatus status, string version, EGameType type)
        {
            Code = code;
            Name = name;
            DtPublish = dtPublish;
            Status = status;
            Version = version;
            Type = type;
        }
    }
    public static class GameDbBuilder /*Fluent Class*/
    {
        public static EntityTypeBuilder<TEntity> ConfigureUnique<TEntity>(this EntityTypeBuilder<TEntity> self, ModelBuilder modelBuilder) where TEntity : Game
        {
            self.HasKey(c => c.Id);
            self.Property(x => x.Id).IsRequired();
            self.Property(x => x.Name).IsRequired();
            self.Property(x => x.Code).IsRequired();
            self.Property(x => x.Version).IsRequired();

            modelBuilder.Entity<TEntity>().HasIndex(p => new { p.Name }).IsUnique();
            return self;
        }
    }
}