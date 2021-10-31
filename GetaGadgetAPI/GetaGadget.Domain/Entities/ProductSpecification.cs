using System.ComponentModel.DataAnnotations;

namespace GetaGadget.Domain.Entities
{
    public class ProductSpecification
    {
        [Key]
        public int ProductSpecificationId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Value { get; set; }

        public virtual Product Product { get; set; }
    }
}
