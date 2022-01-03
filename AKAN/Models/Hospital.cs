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
        public int CityId { get; set; }
        [Required]
        public int DistrictId { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;
    }
}
