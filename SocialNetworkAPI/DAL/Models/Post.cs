using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Post
    {
        public int Id { get; set; }
        [MaxLength(150)]
        [Required]
        public string Content { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public DateTime DateOfCreation { get; set; }
        public ICollection<Like> Likes { get; set; }
        public ICollection<Comment> Comments { get; set; } 
    }
}
