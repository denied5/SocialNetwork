using System;
using System.Collections.Generic;
using System.Text;

namespace BIL.DTO
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string UserKnownAs { get; set; }
        public string UserPhotoUrl { get; set; }
        public DateTime DateOfCreation { get; set; }
    }
}
