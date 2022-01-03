using System.ComponentModel.DataAnnotations;

namespace AKAN.Models
{
    public class Photo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Url { get; set; }
        [Required]
        public int AdvertId { get; set; }
    }
}
