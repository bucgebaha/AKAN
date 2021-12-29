using System.ComponentModel.DataAnnotations;

namespace AKAN.Models
{
    public class Hospital
    {
        [Key]
        public int Id { get; set; }
        public string? ContactNumber { get; set; }
        [Required]
        public string HospitalName { get; set; }
        [Required]
        public string Location { get; set; }
        public string? Adress { get; set; }
        [Required]
        public City City { get; set; }
        [Required]
        public District District { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;
    }
}
