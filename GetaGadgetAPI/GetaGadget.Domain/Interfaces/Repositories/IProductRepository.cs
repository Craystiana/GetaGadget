using GetaGadget.Domain.Entities;
using System.Collections.Generic;

namespace GetaGadget.Domain.Interfaces.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetList(string search, IEnumerable<int> providerIds, IEnumerable<int> deliveryMethodIds, IEnumerable<int> categoryIds, bool? inStock, int? sortById);
    }
}
