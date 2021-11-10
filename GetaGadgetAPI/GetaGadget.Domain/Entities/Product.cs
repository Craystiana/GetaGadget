using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GetaGadget.Domain.Entities
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        public int Stock { get; set; }

        [Required]
        public int ProviderId { get; set; }

        [Required]
        public int DeliveryMethodId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public byte[] Photo { get; set; }

        public virtual ICollection<ProductSpecification> ProductSpecifications { get; set; }
        public virtual Provider Provider { get; set; }
        public virtual DeliveryMethod DeliveryMethod { get; set; }
        public virtual Category Category { get; set; }
    }
}
