using qodeless.domain.Entities;
using qodeless.domain.Interfaces.Repositories;
using qodeless.domain.Repositories;
using qodeless.Infra.CrossCutting.Identity.Data;

namespace qodeless.Infra.CrossCutting.Identity.Repositories
{
    public class VouscherRepository : Repository<Voucher>, IVouscherRepository
    {
        public VouscherRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
