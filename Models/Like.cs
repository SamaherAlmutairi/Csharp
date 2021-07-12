using System.ComponentModel.DataAnnotations;

namespace exam.Models
{
    public class Like
    {
        [Key]
        public int LikeId { get; set; }
        public int UserId { get; set; }
        public User Hlike { get; set; }
        public int HobbyId { get; set; }
        public Hobby HlikeOf { get; set; }
    }
}