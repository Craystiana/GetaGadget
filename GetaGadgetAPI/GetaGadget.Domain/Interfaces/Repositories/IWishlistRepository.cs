using GetaGadget.Domain.Entities;
using System.Collections.Generic;

namespace GetaGadget.Domain.Interfaces.Repositories
{
    public interface IWishlistRepository : IRepository<Wishlist>
    {
        public IEnumerable<Wishlist> GetUserWishlist(int userId);
    }
}
