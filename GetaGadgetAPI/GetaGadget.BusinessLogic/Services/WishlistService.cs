using GetaGadget.Domain.DTO.Wishlist;
using GetaGadget.Domain.Entities;
using GetaGadget.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GetaGadget.BusinessLogic.Services
{
    public class WishlistService : BaseService
    {
        private ProductService _productService;
        public WishlistService(IUnitOfWork unitOfWork, ProductService productService) : base(unitOfWork) 
        {
            this._productService = productService;
        }

        public IEnumerable<WishlistProductModel> GetUserWishlist(int userId)
        {
            return UnitOfWork.WishlistRepository.GetUserWishlist(userId)
                                                .Select(w => new WishlistProductModel
                                                {
                                                    ProductId = w.Product.ProductId,
                                                    ProductName = w.Product.Name,
                                                    ProductPhoto = _productService.ConvertToBase64String(w.Product.Photo),
                                                    ProductPrice = w.Product.Price
                                                });
        }

        public void AddProductToWishlist(int userId, int productId)
        {
            if (UnitOfWork.WishlistRepository.Find(w => w.UserId == userId && w.ProductId == productId).SingleOrDefault() == null)
            {
                var wishlist = new Wishlist
                {
                    UserId = userId,
                    ProductId = productId
                };

                UnitOfWork.WishlistRepository.Add(wishlist);
                Save();
            }
        }

        public void RemoveProductFromWishlist(int userId, int productId)
        {
            var wishlist = UnitOfWork.WishlistRepository.Find(w => w.UserId == userId && w.ProductId == productId).SingleOrDefault();

            UnitOfWork.WishlistRepository.Remove(wishlist);
            Save();
        }
    }
}
