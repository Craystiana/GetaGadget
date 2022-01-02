using GetaGadget.Domain.Entities;
using System.Collections.Generic;

namespace GetaGadget.Domain.Interfaces.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Order GetOrderDetails(int orderId);

        Order GetCurrentOrder(int userId);
    }
}
