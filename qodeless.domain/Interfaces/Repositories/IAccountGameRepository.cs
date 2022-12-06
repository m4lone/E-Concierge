using qodeless.domain.Entities;
using qodeless.domain.Model;
using System.Linq;

namespace qodeless.domain.Interfaces.Repositories
{
    public interface IAccountGameRepository : IRepository<AccountGame> //SOLID
    {
        IQueryable<AccountDropDownViewModel> GetAccountsDrop();
        IQueryable<GameDropDownViewModel> GetGamesDrop();
    }
}
