using System.ComponentModel.DataAnnotations;

namespace AKAN.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required][EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string BloodType { get; set; }
        public string? Location { get; set; }
        public int? MaxDestination { get; set; }
        public bool isAvailable { get; set; } = true;
        public DateTime CreationTime { get; set; } = DateTime.Now;

    }
}
