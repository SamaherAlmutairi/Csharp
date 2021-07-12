using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace exam.Models
{
    public class Hobby
    {
        [Key]
        [Required]
        public int HobbyId { get; set; }
        [Required]
        // [MinLength(5, ErrorMessage = "Must be at least 5 characters")]
        public string Name { get; set; }
        [Required]
        public string Description {get; set;}

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public int UserId { get; set; }
        public User Amateur { get; set; }
        public List<Like> Hlikes { get; set; }
    }
}