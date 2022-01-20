using GetaGadget.Domain.DTO.Order;
using GetaGadget.Domain.Entities;
using GetaGadget.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GetaGadget.BusinessLogic.Services
{
    public class OrderService : BaseService
    {
        private readonly ProductService _productService;

        public OrderService(IUnitOfWork unitOfWork, ProductService productService) : base(unitOfWork) {
            this._productService = productService;
        }

        public bool OrderBelongsToUser(int userId, int orderId)
        {
            var order = UnitOfWork.OrderRepository.Get(orderId);

            return order?.UserId == userId;
        }

        public Order AddProductToOrder(Order order, int productId)
        {
            var existingOrderProduct = order.OrderProducts.FirstOrDefault(op => op.ProductId == productId);
            var product = UnitOfWork.ProductRepository.Get(productId);

            if (existingOrderProduct != null)
            {
                existingOrderProduct.Quantity++;
            }
            else
            {
                UnitOfWork.OrderProductRepository.Add(new OrderProduct
                {
                    ProductId = productId,
                    Order = order,
                    Quantity = 1
                });
            }

            order.TotalValue += product.Price;

            return order;
        }

        public Order AddProductToOrder(int userId, int productId)
        {
            // Get order that does not have a date yet
            var order = UnitOfWork.OrderRepository.GetCurrentOrder(userId);

            if (order == null)
            {
                order = new Order
                {
                    UserId = userId,
                    OrderProducts = new List<OrderProduct>()
                };
                UnitOfWork.OrderRepository.Add(order);
            }

            order = AddProductToOrder(order, productId);
            
            Save();

            return order;
        }
        
        public Order RemoveProductFromOrder(int userId, int productId)
        {
            var order = UnitOfWork.OrderRepository.Find(o => o.UserId == userId && o.OrderDate == null).FirstOrDefault();

            if (order != null)
            {
                var orderProduct = UnitOfWork.OrderProductRepository.Find(op => op.OrderId == order.OrderId && op.ProductId == productId).FirstOrDefault();
                var product = UnitOfWork.ProductRepository.Get(productId);

                orderProduct.Quantity -= 1;
                order.TotalValue -= product.Price;

                if (orderProduct.Quantity <= 0)
                {
                    order.OrderProducts.Remove(orderProduct);
                }
            }

            Save();

            return order;
        }

        public OrderModel GetCurrentOrder(int userId)
        {
            var order = UnitOfWork.OrderRepository.Find(o => o.UserId == userId && o.OrderDate == null).FirstOrDefault();

            if (order != null)
            {
                return GetOrderModel(order.OrderId);
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<OrderHistoryModel> GetHistory(int userId)
        {
            var orders = UnitOfWork.OrderRepository.Find(o => o.UserId == userId && o.OrderDate.HasValue);

            return orders.Select(o => GetOrderModelHistory(o.OrderId));
        }

        public OrderHistoryModel GetOrderModelHistory(int orderId)
        {
            var order = UnitOfWork.OrderRepository.GetOrderDetails(orderId);

            return new OrderHistoryModel
            {
                OrderId = order.OrderId,
                OrderDate = order.OrderDate != null ? ((DateTime)order.OrderDate).ToString("dd MMMM yyyy") : null,
                Address = "County: " + order.County + ", City: " + order.City + ", Postal Code: " + order.PostalCode + ", Details: " + order.FullAddress,
                CardDetails = "Number: " + order.CardNumber + ", CSV: " + order.CardCsv + ", Expiration Date: " + ((DateTime)order.CardExpirationDate).ToString("MMMM yyyy"),
                DeliveryMethod = order.DeliveryMethod != null ? order.DeliveryMethod.Name : null,
                TotalValue = order.TotalValue,
                Products = order.OrderProducts.Select(op => new OrderProductModel
                {
                    ProductId = op.ProductId,
                    ProductName = op.Product.Name,
                    Photo = _productService.ConvertToBase64String(op.Product.Photo),
                    Quantity = op.Quantity,
                    Price = op.Product.Price
                })
            };
        }

        public OrderModel GetOrderModel(int orderId)
        {
            var order = UnitOfWork.OrderRepository.GetOrderDetails(orderId);

            return new OrderModel
            {
                OrderId = order.OrderId,
                OrderDate = order.OrderDate != null ? ((DateTime)order.OrderDate).ToString("dd MMMM yyyy") : null,
                TotalValue = order.TotalValue,
                Products = order.OrderProducts.Select(op => new OrderProductModel
                {
                    ProductId = op.ProductId,
                    ProductName = op.Product.Name,
                    Photo = _productService.ConvertToBase64String(op.Product.Photo),
                    Quantity = op.Quantity,
                    Price = op.Product.Price
                })
            };
        }

        public void PlaceOrder(int userId, PlaceOrderModel model)
        {
            var order = UnitOfWork.OrderRepository.Find(o => o.UserId == userId && o.OrderDate == null).FirstOrDefault();
            var coupon = UnitOfWork.CouponRepository.Find(c => c.Code == model.Coupon).FirstOrDefault();

            order.County = model.County;
            order.City = model.City;
            order.FullAddress = model.FullAddress;
            order.PostalCode = model.PostalCode;
            order.CardNumber = model.CardNumber;
            order.CardCsv = model.CardCsv;
            order.CardExpirationDate = model.CardExpirationDate;
            order.DeliveryMethodId = model.DeliveryMethodId;
            order.OrderDate = System.DateTime.Now;

            if (coupon != null && order.TotalValue > coupon.MinOrderValue)
            {
                order.TotalValue -= order.TotalValue * coupon.Discount / 100;
                order.CouponId = coupon.CouponId;
            }

            // add delivery cost
            order.TotalValue += 20;

            RemoveStockForOrder(order.OrderId);

            Save();
        }

        public void RemoveStockForOrder(int orderId)
        {
            var order = UnitOfWork.OrderRepository.GetOrderDetails(orderId);

            foreach (var op in order.OrderProducts)
            {
                var product = UnitOfWork.ProductRepository.Get(op.ProductId);

                product.Stock -= op.Quantity;
            }

            Save();
        }

        public IEnumerable<CouponModel> GetCoupons()
        {
            return UnitOfWork.CouponRepository.GetAll().Select(c => new CouponModel
            {
                Code = c.Code,
                MinOrderValue = c.MinOrderValue
            });
        }

    }
}
