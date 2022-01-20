using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GetaGadget.Domain.Entities
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public int UserId { get; set; }

        public string County { get; set; }

        public string City { get; set; }

        public string FullAddress { get; set; }

        public string PostalCode { get; set; }

        public string CardNumber { get; set; }

        public int? CardCsv { get; set; }

        public DateTime? CardExpirationDate { get; set; }

        public int? DeliveryMethodId { get; set; }

        public float TotalValue { get; set; }

        public DateTime? OrderDate { get; set; }

        public int? CouponId { get; set; }

        public virtual User User { get; set; }
        public virtual DeliveryMethod DeliveryMethod { get; set; }
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
        public virtual Coupon Coupon { get; set; } 

    }
}
