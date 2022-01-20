using GetaGadget.Domain.Entities;
using GetaGadget.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GetaGadget.DataAccess.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(GetaGadgetContext context) : base(context) { }

        public Order GetCurrentOrder(int userId)
        {
            return _context.Orders.Include(o => o.OrderProducts)
                                  .FirstOrDefault(o => o.UserId == userId && o.OrderDate == null);
        }

        public Order GetOrderDetails(int orderId)
        {
            return _context.Orders.Include(o => o.DeliveryMethod)
                                  .Include(o => o.OrderProducts)
                                  .ThenInclude(op => op.Product)
                                  .FirstOrDefault(o => o.OrderId == orderId);
        }

    }
}
