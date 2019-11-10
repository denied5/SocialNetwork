using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class Like
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public Post Post { get; set; }
    }
}
