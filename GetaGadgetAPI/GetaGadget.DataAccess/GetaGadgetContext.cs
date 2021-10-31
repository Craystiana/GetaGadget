using GetaGadget.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GetaGadget.DataAccess
{
    public class GetaGadgetContext : DbContext
    {
        public GetaGadgetContext(DbContextOptions<GetaGadgetContext> options) : base(options) { }

        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductSpecification> ProductSpecifications { get; set; }
        public virtual DbSet<Provider> Providers { get; set; }
        public virtual DbSet<DeliveryMethod> DeliveryMethods { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Wishlist> Wishlists { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderProduct> OrderProducts { get; set; }
    }
}
