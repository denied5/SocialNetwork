using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public DateTime DateOfCreation { get; set; }
        public ICollection<Like> Likes { get; set; }
    }
}
