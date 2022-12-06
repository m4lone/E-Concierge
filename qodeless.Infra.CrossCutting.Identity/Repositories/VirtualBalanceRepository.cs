using qodeless.domain.Entities;
using qodeless.domain.Interfaces.Repositories;
using qodeless.domain.Repositories;
using qodeless.Infra.CrossCutting.Identity.Data;
using System.Linq;

namespace qodeless.Infra.CrossCutting.Identity.Repositories
{
    public class VirtualBalanceRepository : Repository<VirtualBalance>, IVirtualBalanceRepository
    {
        public VirtualBalanceRepository(ApplicationDbContext context) : base(context)
        {
        }

       
    }
}