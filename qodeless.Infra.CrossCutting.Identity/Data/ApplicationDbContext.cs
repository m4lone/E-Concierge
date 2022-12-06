using System;
using System.IO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using qodeless.domain.Entities;
using qodeless.Infra.CrossCutting.Identity.Entities;

namespace qodeless.Infra.CrossCutting.Identity.Data
{
    public interface IAppDbContext 
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IAppDbContext
    {
        #region ACL_ENTITIES
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<IdentityRole> Roles { get; set; }
        public DbSet<IdentityUserRole<string>> UserRoles { get; set; }
        public DbSet<IdentityUserClaim<string>> UserClaims { get; set; }
        public DbSet<IdentityUserLogin<string>> UserLogins { get; set; }
        public DbSet<IdentityUserToken<string>> UserTokens { get; set; }
        public DbSet<IdentityRoleClaim<string>> RoleClaims { get; set; }
        #endregion //ACL_ENTITIES
        public DbSet<Order> Orders { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountGame> AccountGames { get; set; }
        public DbSet<Site> Sites { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceGame> DeviceGames { get; set; }
        public DbSet<Play> Plays { get; set; }
        public DbSet<SessionDevice> SessionDevices { get; set; }
        public DbSet<SessionSite> SessionSites { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<SiteGame> SiteGames { get; set; }
        public DbSet<SitePlayer> SitePlayers { get; set; }
        public DbSet<SuccessFee> SuccessFees { get; set; }
        public DbSet<Currency> Currency { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<IncomeType> IncomeTypes { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseRequest> ExpensesRequest { get; set; }        
        public DbSet<Token> Tokens { get; set; }
        public DbSet<SiteDevice> SiteDevices { get; set; }
        public DbSet<VirtualBalance> VirtualBalances { get; set; }
        public DbSet<ResponseAuth> ResponseAuths { get; set; }
        public DbSet<RequestAuth> RequestAuths { get; set; }
        public DbSet<Invite> Invites { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<Exchange> Exchanges { get; set; }
        public DbSet<GameSetting> GameSettings { get; set; }
        public DbSet<CardStones> CardStones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new Account(Guid.Empty).Configure(modelBuilder.Entity<Account>().ToTable("Account"));
            new AccountGame(Guid.Empty).Configure(modelBuilder.Entity<AccountGame>().ToTable("AccountGame"));
            new DeviceGame(Guid.Empty).Configure(modelBuilder.Entity<DeviceGame>().ToTable("DeviceGame"));
            new Play(Guid.Empty).Configure(modelBuilder.Entity<Play>().ToTable("Play"));
            new SessionDevice(Guid.Empty).Configure(modelBuilder.Entity<SessionDevice>().ToTable("SessionDevice"));
            new SessionSite(Guid.Empty).Configure(modelBuilder.Entity<SessionSite>().ToTable("SessionSite"));
            new SiteGame(Guid.Empty).Configure(modelBuilder.Entity<SiteGame>().ToTable("SiteGame"));
            new SitePlayer(Guid.Empty).Configure(modelBuilder.Entity<SitePlayer>().ToTable("SitePlayer"));
            new SuccessFee(Guid.Empty).Configure(modelBuilder.Entity<SuccessFee>().ToTable("SuccessFee"));
            new Currency(Guid.Empty).Configure(modelBuilder.Entity<Currency>().ToTable("Currency"));
            new Income(Guid.Empty).Configure(modelBuilder.Entity<Income>().ToTable("Income"));
            new Expense(Guid.Empty).Configure(modelBuilder.Entity<Expense>().ToTable("Expense"));            
            new IncomeType(Guid.Empty).Configure(modelBuilder.Entity<IncomeType>().ToTable("IncomeType"));
            new Token(Guid.Empty).Configure(modelBuilder.Entity<Token>().ToTable("Tokens"));
            new Expense(Guid.Empty).Configure(modelBuilder.Entity<Expense>().ToTable("Expense"));
            new ExpenseRequest(Guid.Empty).Configure(modelBuilder.Entity<ExpenseRequest>().ToTable("ExpenseRequest"));
            new Group(Guid.Empty).Configure(modelBuilder.Entity<Group>().ToTable("Group"));
            new Exchange(Guid.Empty).Configure(modelBuilder.Entity<Exchange>().ToTable("Exchange"));
            new VirtualBalance(Guid.Empty).Configure(modelBuilder.Entity<VirtualBalance>().ToTable("VirtualBalance"));
            new SiteDevice(Guid.Empty).Configure(modelBuilder.Entity<SiteDevice>().ToTable("SiteDevice"));
            new RequestAuth(Guid.Empty).Configure(modelBuilder.Entity<RequestAuth>().ToTable("RequestAuth"));
            new ResponseAuth(Guid.Empty).Configure(modelBuilder.Entity<ResponseAuth>().ToTable("ResponseAuth"));
            new CardStones(Guid.Empty).Configure(modelBuilder.Entity<CardStones>().ToTable("CardStones"));
            modelBuilder.Entity<Order>().ConfigureUnique(modelBuilder).ToTable("Order");
            modelBuilder.Entity<Voucher>().ConfigureUnique(modelBuilder).ToTable("Voucher");
            modelBuilder.Entity<Device>().ConfigureUnique(modelBuilder).ToTable("Device");
            modelBuilder.Entity<Game>().ConfigureUnique(modelBuilder).ToTable("Game");
            modelBuilder.Entity<Site>().ConfigureUnique(modelBuilder).ToTable("Site");
            modelBuilder.Entity<Invite>().ConfigureUnique(modelBuilder).ToTable("Invite");
            modelBuilder.Entity<GameSetting>().ConfigureUnique(modelBuilder).ToTable("GameSetting");
            modelBuilder.Entity<ApplicationUser>()
                .HasIndex(p => new { p.Cpf, p.PixKey,p.PhoneNumber}).IsUnique();

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseNpgsql(connectionString);
            }
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public ApplicationDbContext() { }
    }
}
