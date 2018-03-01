using System.ComponentModel.DataAnnotations;

namespace FBFInventory.Domain.Entity
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public bool IsActive { get; set; }

        [Required]
        public virtual Role Role { get; set; }
    }
}
