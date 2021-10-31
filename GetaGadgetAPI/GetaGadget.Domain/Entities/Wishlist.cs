using System.ComponentModel.DataAnnotations;

namespace GetaGadget.Domain.Entities
{
    public class Wishlist
    {
        [Key]
        public int WishlistId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int ProductId { get; set; }

        public virtual User User { get; set; }
        public virtual Product Product { get; set; }
    }
}
