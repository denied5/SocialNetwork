using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
        public string Content { get; set; }
        public DateTime DateOfCreation { get; set; }
    }
}
