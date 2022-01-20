using System;
using System.Collections.Generic;

namespace GetaGadget.Domain.DTO.Order
{
    public class OrderModel
    {
        public int OrderId { get; set; } 

        public string OrderDate { get; set; }

        public float TotalValue { get; set; }

        public IEnumerable<OrderProductModel> Products { get; set; }
    }
}
