using qodeless.domain.Entities;
using qodeless.domain.Interfaces.Repositories;
using qodeless.domain.Repositories;
using qodeless.Infra.CrossCutting.Identity.Data;
using System.Linq;

namespace qodeless.Infra.CrossCutting.Identity.Repositories
{
    public class InviteRepository : Repository<Invite>, IInviteRepository
    {
        public InviteRepository(ApplicationDbContext context) : base(context)
        {
        }
        public IQueryable<Invite> GetInvites()
        {
            return Db.Invites;
        }  

    }
}
