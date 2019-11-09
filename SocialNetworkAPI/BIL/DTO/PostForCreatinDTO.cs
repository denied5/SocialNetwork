using System;
using System.Collections.Generic;
using System.Text;

namespace BIL.DTO
{
    public class PostForCreatinDTO
    {
        public PostForCreatinDTO()
        {
            DateOfCreation = DateTime.Now;
        }

        public string Content { get; set; }
        public int UserId { get; set; }
        public DateTime DateOfCreation { get; set; }

    }
}
