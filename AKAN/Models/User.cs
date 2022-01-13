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
        public int? CityId { get; set; }
        public int? DistrictId { get; set; }
        public int? MaxDestination { get; set; }
        public bool isAvailable { get; set; } = true;
        public string photoUrl { get; set; } = "https://firebasestorage.googleapis.com/v0/b/derss5.appspot.com/o/image_2022-01-13_180030.png?alt=media&token=42142e54-fd22-474c-a63d-3597a5238cf3";
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;

    }
}
