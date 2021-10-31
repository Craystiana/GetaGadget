using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GetaGadget.Domain.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public int UserRoleId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string EmailAddress { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string Salt { get; set; }

        public virtual UserRole UserRole { get; set; }
        public virtual ICollection<Wishlist> Wishlist { get; set; }
        public virtual ICollection<Order> Order { get; set; }
    }
}
