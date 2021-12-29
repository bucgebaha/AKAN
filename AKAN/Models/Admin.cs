using System.ComponentModel.DataAnnotations;

namespace AKAN.Models
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;
    }
}
