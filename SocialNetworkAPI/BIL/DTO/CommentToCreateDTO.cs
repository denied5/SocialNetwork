using System;
using System.Collections.Generic;
using System.Text;

namespace BIL.DTO
{
    public class CommentToCreateDTO
    {
        public CommentToCreateDTO(DateTime dateOfCreation)
        {
            DateOfCreation = DateTime.Now;
        }

        public string Content { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public DateTime DateOfCreation { get; set; }
    }
}
