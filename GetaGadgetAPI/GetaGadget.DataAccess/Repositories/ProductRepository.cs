using GetaGadget.Common.Enums;
using GetaGadget.Domain.Entities;
using GetaGadget.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GetaGadget.DataAccess.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(GetaGadgetContext context) : base(context) { }

        public Product Get(int productId)
        {
            return _context.Products.Include(c => c.Provider)
                                .Include(c => c.DeliveryMethod)
                                .Include(c => c.Category)
                                .FirstOrDefault(c => c.ProductId == productId);
        }

        public IEnumerable<Product> GetList(string search,
                                        IEnumerable<int> providerIds,
                                        IEnumerable<int> deliveryMethodIds,
                                        IEnumerable<int> categoryIds,
                                        bool? inStock,
                                        int? sortById)
        {
            IQueryable<Product> products = _context.Products.Include(c => c.Provider)
                                                 .Include(c => c.DeliveryMethod)
                                                 .Include(c => c.Category);

            if (!string.IsNullOrEmpty(search))
            {
                products = products.Where(c => c.Name.Contains(search));
            }

            if (providerIds != null && providerIds.Any())
            {
                products = products.Where(c => providerIds.Contains(c.ProviderId));
            }

            if (deliveryMethodIds != null && deliveryMethodIds.Any())
            {
                products = products.Where(c => deliveryMethodIds.Contains(c.DeliveryMethodId));
            }

            if (categoryIds != null && categoryIds.Any())
            {
                products = products.Where(c => categoryIds.Contains(c.CategoryId));
            }

            if (inStock == true)
            {
                products = products.Where(p => p.Stock > 0);
            }

            if (sortById != null)
            {
                switch (sortById)
                {
                    case (int)SortType.Name:
                        products = products.OrderBy(c => c.Name);
                        break;

                    case (int)SortType.Price:
                        products = products.OrderBy(c => c.Price);
                        break;

                    case (int)SortType.Stock:
                        products = products.OrderBy(c => c.Stock);
                        break;
                }
            }

            return products;
        }
    }
}
