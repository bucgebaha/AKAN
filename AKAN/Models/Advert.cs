using System.ComponentModel.DataAnnotations;

namespace AKAN.Models
{
    public class Advert
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string BloodType { get; set; }
        [Required]
        public int CreatorID { get; set; }
        [Required]
        public int HospitalID { get; set; }
        public string? Details { get; set; }
        public int Importancy { get; set; } = 3;
        [Required]
        public bool isCompleted { get; set; } = false;
        public DateTime? CompletionTime { get; set; }
        public int? DonatorId { get; set; }
        public bool isActive { get; set; } = true;

        public DateTime CreationTime { get; set; } = DateTime.Now;
    }
}
