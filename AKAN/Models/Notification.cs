﻿using System.ComponentModel.DataAnnotations;

namespace AKAN.Models
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Details { get; set; }
        public bool Seen { get; set; } = false;
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;
    }
}
