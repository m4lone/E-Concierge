using qodeless.domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace qodeless.domain.Interfaces.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        //IEnumerable<Order> GetAllOrderNoRegistry();
        Task<IQueryable<Order>> GetAllCorrectly();
    }
}
