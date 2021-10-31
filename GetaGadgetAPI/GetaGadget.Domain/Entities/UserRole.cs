using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GetaGadget.Domain.Entities
{
    public class UserRole
    {
        [Key]
        public int UserRoleId { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
