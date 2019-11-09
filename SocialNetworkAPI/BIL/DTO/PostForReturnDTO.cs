using System;
using System.Collections.Generic;
using System.Text;

namespace BIL.DTO
{
    public class PostForReturnDTO
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public IEnumerable<UserForListDTO> Likes { get; set; }
        public int UserId { get; set; }
        public DateTime DateOfCreation { get; set; }
    }
}
