using System.ComponentModel.DataAnnotations;

namespace AKAN.Models
{
    public class Banned
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        public string? Reason { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;
    }
}
