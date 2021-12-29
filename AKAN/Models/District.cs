using System.ComponentModel.DataAnnotations;

namespace AKAN.Models
{
    public class District
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int CityId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
