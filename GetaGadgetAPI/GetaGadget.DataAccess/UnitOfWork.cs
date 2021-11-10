using GetaGadget.DataAccess.Repositories;
using GetaGadget.Domain.Entities;
using GetaGadget.Domain.Interfaces;
using GetaGadget.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;

namespace GetaGadget.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GetaGadgetContext _context;

        private bool isDisposed;

        private IUserRepository _userRepository;
        private IRepository<UserRole> _userRoleRepository;
        private IProductRepository _productRepository;
        private IRepository<ProductSpecification> _productSpecificationRepository;
        private IRepository<Provider> _providerRepository;
        private IRepository<DeliveryMethod> _deliveryMethodRepository;
        private IRepository<Category> _categoryRepository;
        private IRepository<Wishlist> _wishlistRepository;
        private IRepository<Order> _orderRepository;
        private IRepository<OrderProduct> _orderProductRepository;

        public UnitOfWork(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<GetaGadgetContext>();
            optionsBuilder.UseSqlServer(connectionString);

            _context = new GetaGadgetContext(optionsBuilder.Options);
        }

        public IUserRepository UserRepository => _userRepository ??= new UserRepository(_context);

        public IRepository<UserRole> UserRoleRepository => _userRoleRepository ??= new Repository<UserRole>(_context);

        public IProductRepository ProductRepository => _productRepository ??= new ProductRepository(_context);

        public IRepository<ProductSpecification> ProductSpecificationRepository => _productSpecificationRepository ??= new Repository<ProductSpecification>(_context);

        public IRepository<Provider> ProviderRepository => _providerRepository ??= new Repository<Provider>(_context);

        public IRepository<DeliveryMethod> DeliveryMethodRepository => _deliveryMethodRepository ??= new Repository<DeliveryMethod>(_context);

        public IRepository<Category> CategoryRepository => _categoryRepository ??= new Repository<Category>(_context);

        public IRepository<Wishlist> WishlistRepository => _wishlistRepository ??= new Repository<Wishlist>(_context);

        public IRepository<Order> OrderRepository => _orderRepository ??= new Repository<Order>(_context);

        public IRepository<OrderProduct> OrderProductRepository => _orderProductRepository ??= new Repository<OrderProduct>(_context);

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

    }
}
