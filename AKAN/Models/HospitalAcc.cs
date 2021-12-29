using System.ComponentModel.DataAnnotations;

namespace AKAN.Models
{
    public class HospitalAcc
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int HospitalId { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;
    }
}
