using GetaGadget.Domain.Entities;
using GetaGadget.Domain.Interfaces.Repositories;
using System;

namespace GetaGadget.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<UserRole> UserRoleRepository { get; }
        IUserRepository UserRepository { get; }
        IRepository<Product> ProductRepository { get; }
        IRepository<ProductSpecification> ProductSpecificationRepository { get; }
        IRepository<Provider> ProviderRepository { get; }
        IRepository<DeliveryMethod> DeliveryMethodRepository { get; }
        IRepository<Category> CategoryRepository { get; }
        IRepository<Wishlist> WishlistRepository { get; }
        IRepository<Order> OrderRepository { get; }
        IRepository<OrderProduct> OrderProductRepository { get; }

        int SaveChanges();
    }
}
