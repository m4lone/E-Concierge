using qodeless.domain.Entities;
using qodeless.domain.Interfaces.Repositories;
using qodeless.domain.Model;
using qodeless.domain.Repositories;
using qodeless.Infra.CrossCutting.Identity.Data;
using System.Linq;

namespace qodeless.Infra.CrossCutting.Identity.Repositories
{
    public class DeviceGameRepository : Repository<DeviceGame>, IDeviceGameRepository
    {
        public DeviceGameRepository(ApplicationDbContext context) : base(context)
        {
        }
        public IQueryable<DeviceDropDownViewModel> GetDevicesDrop()
        {
            return Db.Devices.Select(x => new DeviceDropDownViewModel { Id = x.Id, Code = x.Code }).AsQueryable();
        }
        public IQueryable<GameDropDownViewModel> GetGamesDrop()
        {
            return Db.Games.Select(x => new GameDropDownViewModel { Id = x.Id, Name = x.Name }).AsQueryable();
        }
    }
}
