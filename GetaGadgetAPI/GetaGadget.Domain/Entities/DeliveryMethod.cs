using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GetaGadget.Domain.Entities
{
    public class DeliveryMethod
    {
        [Key]
        public int DeliveryMethodId { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
