using GetaGadget.Domain.Entities;
using GetaGadget.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace GetaGadget.DataAccess.Repositories
{
    public class WishlistRepository : Repository<Wishlist>, IWishlistRepository
    {
        public WishlistRepository(GetaGadgetContext context) : base(context) { }

        public IEnumerable<Wishlist> GetUserWishlist(int userId)
        {
            return _context.Wishlists.Where(w => w.UserId == userId)
                                     .Include(w => w.Product);
        }
    }
}
