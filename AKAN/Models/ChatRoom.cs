using System.ComponentModel.DataAnnotations;

namespace AKAN.Models
{
    public class ChatRoom
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ReceiverId { get; set; }
        [Required]
        public int TransmitterId { get; set; }
        public int UnreadMessageCount { get; set; } = 0;
        public bool isActive { get; set; } = true;
        public DateTime CreationTime { get; set; } = DateTime.Now;
    }
}
