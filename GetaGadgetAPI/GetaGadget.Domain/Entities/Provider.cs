using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GetaGadget.Domain.Entities
{
    public class Provider
    {
        [Key]
        public int ProviderId { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
