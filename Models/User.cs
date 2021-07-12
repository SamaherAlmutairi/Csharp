using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace exam.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = " must be at least 2 characters long")]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = " must be at least 2 characters long")]
        [Display(Name = "LastName")]
        public string LastName { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = " must be between 3 and 15 characters ")]
        [MaxLength(15, ErrorMessage = " must be between 3 and 15 characters ")]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password must be 8 characters or longer!")]
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        [NotMapped]
        [Required]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string Confirm { get; set; }
        public List<Hobby> PostedHobbies  { get; set; }
        public List<Like> LikedHobbies  { get; set; }
    }
}