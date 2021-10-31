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

        public string CardNumber { get; set; }

        public int CardCsv { get; set; }

        public DateTime CardExpirationDate { get; set; }

        public int DeliveryMethodId { get; set; }

        public int TotalValue { get; set; }

        public DateTime OrderDate { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }

    }
}
