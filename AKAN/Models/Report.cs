using System.ComponentModel.DataAnnotations;

namespace AKAN.Models
{
    public class Report
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ChatRoomId { get; set; }
        [Required]
        public string Details { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;
    }
}
