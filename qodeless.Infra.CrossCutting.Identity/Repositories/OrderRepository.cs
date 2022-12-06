using Microsoft.EntityFrameworkCore;
using qodeless.domain.Entities;
using qodeless.domain.Interfaces.Repositories;
using qodeless.domain.Repositories;
using qodeless.Infra.CrossCutting.Identity.Data;
using Qodeless.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qodeless.Infra.CrossCutting.Identity.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
        }

        //public IEnumerable<Order> GetAllOrderNoRegistry()
        //{
        //    return Db.Orders.Where(x => x.EOrderStatus.Equals(EOrderStatus.Unpaid)).AsNoTracking();
        //}

        public async Task<IQueryable<Order>> GetAllCorrectly()
        {
            var orders = await Task.Run(() => Db.Orders.Where(_ => true).ToList());
            var correctlyOrders = new List<Order>();

            await Task.Run(() =>
            {
                foreach (var x in orders)
                {
                    correctlyOrders.Add(
                        new Order() { 
                            Code = x.Code, 
                            QrCode = x.QrCode, 
                            PixId = x.PixId, 
                            UserId = x.UserId, 
                            Date = x.Date, 
                            EOrderStatus = x.EOrderStatus, 
                            Value = (float)Decimal.Parse((x.Value / 100).ToString("F2")),
                            Fee = Decimal.Parse((x.Fee/ 100m).ToString("F2")),
                        });
                }
            });

            return correctlyOrders.AsQueryable();
        }
    }
}
