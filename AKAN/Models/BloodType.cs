using System.ComponentModel.DataAnnotations;

namespace AKAN.Models
{
    public class BloodType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Type { get; set; }
    }
}
