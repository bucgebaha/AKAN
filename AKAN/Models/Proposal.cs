using System.ComponentModel.DataAnnotations;

namespace AKAN.Models
{
    public class Proposal
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int AdvertId { get; set; }
        [Required]
        public int TransmitterId { get; set; }
        public bool isAccepted { get; set; } = false;
        public int? ChatId { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;
    }
}
