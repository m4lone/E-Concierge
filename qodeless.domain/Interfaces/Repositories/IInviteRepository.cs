using qodeless.domain.Entities;
using System.Linq;

namespace qodeless.domain.Interfaces.Repositories
{
    public interface IInviteRepository : IRepository<Invite> //SOLID
    {
        IQueryable<Invite> GetInvites();
    }
}
