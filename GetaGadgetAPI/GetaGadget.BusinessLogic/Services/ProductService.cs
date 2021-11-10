using GetaGadget.Domain.DTO.Product;
using GetaGadget.Domain.Entities;
using GetaGadget.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GetaGadget.BusinessLogic.Services
{
    public class ProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ProductModel GetProductDetails(int productId)
        {
            var product = _unitOfWork.ProductRepository.Get(productId);

            return new ProductModel()
            {
                Id = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                Provider = product.Provider.Name,
                DeliveryMethod = product.DeliveryMethod.Name,
                Category = product.Category.Name,
                Photo = ConvertToBase64String(product.Photo)
            };
        }

        public ProductEditModel GetProduct(int productId)
        {
            var product = _unitOfWork.ProductRepository.Get(productId);

            return new ProductEditModel()
            {
                Id = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                Provider = product.ProviderId,
                DeliveryMethod = product.DeliveryMethodId,
                Category = product.CategoryId,
                Photo = ConvertToBase64String(product.Photo)
            };
        }

        public IEnumerable<ProductModel> GetList(ProductQueryModel model)
        {
            return _unitOfWork.ProductRepository.GetList(model.SearchTerm, model.ProviderIds, model.DeliveryMethodIds, model.CategoryIds, model.SortById)
                                                .Select(p => new ProductModel
                                                {
                                                    Id = p.ProductId,
                                                    Name = p.Name,
                                                    Description = p.Description,
                                                    Price = p.Price,
                                                    Stock = p.Stock,
                                                    Provider = p.Provider.Name,
                                                    DeliveryMethod = p.DeliveryMethod.Name,
                                                    Category = p.Category.Name,
                                                    Photo = ConvertToBase64String(p.Photo)
                                                });
        }

        public void Add(ProductEditModel model)
        {
            var product = new Product
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Stock = model.Stock,
                ProviderId = model.Provider,
                DeliveryMethodId = model.DeliveryMethod,
                CategoryId = model.Category,
                Photo = ConvertToByteArray(model.Photo)
            };

            _unitOfWork.ProductRepository.Add(product);
            _unitOfWork.SaveChanges();
        }

        public void Edit(ProductEditModel model)
        {
            var product = _unitOfWork.ProductRepository.Get((int)model.Id);

            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            product.Stock = model.Stock;
            product.ProviderId = model.Provider;
            product.DeliveryMethodId = model.DeliveryMethod;
            product.CategoryId = model.Category;

            if (model.Photo != null)
            {
                product.Photo = ConvertToByteArray(model.Photo);
            }

            _unitOfWork.SaveChanges();
        }

        public void Delete(int productId)
        {
            _unitOfWork.ProductRepository.Remove(_unitOfWork.ProductRepository.Get(productId));
            _unitOfWork.SaveChanges();
        }

        public byte[] ConvertToByteArray(string img)
        {
            return string.IsNullOrEmpty(img) ? null : Convert.FromBase64String(img);
        }

        public string ConvertToBase64String(byte[] byteArray)
        {
            return byteArray == null ? null : Convert.ToBase64String(byteArray);
        }
    }
}
