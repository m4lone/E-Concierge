using qodeless.domain.Entities;
using qodeless.domain.Interfaces.Repositories;
using qodeless.domain.Model;
using qodeless.domain.Repositories;
using qodeless.Infra.CrossCutting.Identity.Data;
using System;
using System.Linq;

namespace qodeless.Infra.CrossCutting.Identity.Repositories
{
    public class AccountGameRepository : Repository<AccountGame>, IAccountGameRepository
    {
        public AccountGameRepository(ApplicationDbContext context) : base(context)
        {
        }
        public IQueryable<AccountGame> GetAccountGames()
        {
            return Db.AccountGames;
        }
        public IQueryable<AccountDropDownViewModel> GetAccountsDrop()
        {
            return Db.Accounts.Select(x => new AccountDropDownViewModel { Id = x.Id, Name = x.Name }).AsQueryable();
        }
        public IQueryable<GameDropDownViewModel> GetGamesDrop()
        {
            return Db.Games.Select(x => new GameDropDownViewModel { Id = x.Id, Name = x.Name }).AsQueryable();
        }
        public bool IsValidAccountGameId(Guid accountId)
        {
            return Db.AccountGames.Any(j => j.Id == accountId);
        }
    }
}
