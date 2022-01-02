using GetaGadget.Domain.DTO.Generic;
using GetaGadget.Domain.DTO.Order;
using GetaGadget.Domain.DTO.Product;
using GetaGadget.Domain.Entities;
using GetaGadget.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GetaGadget.BusinessLogic.Services
{
    public class OrderService : BaseService
    {
        public OrderService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

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
                var orderProduct = order.OrderProducts.First(op => op.ProductId == productId);

                orderProduct.Quantity -= 1;

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

        public IEnumerable<OrderModel> GetHistory(int userId)
        {
            var orders = UnitOfWork.OrderRepository.Find(o => o.UserId == userId && o.OrderDate.HasValue);

            return orders.Select(o => GetOrderModel(o.OrderId));
        }

        public OrderModel GetOrderModel(int orderId)
        {
            var order = UnitOfWork.OrderRepository.GetOrderDetails(orderId);

            return new OrderModel
            {
                OrderId = order.OrderId,
                OrderDate = order.OrderDate,
                TotalValue = order.TotalValue,
                Products = order.OrderProducts.Select(op => new OrderProductModel
                {
                    ProductId = op.ProductId,
                    Quantity = op.Quantity,
                    Price = op.Product.Price
                })
            };
        }

    }
}
