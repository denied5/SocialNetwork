using System;
using System.Collections.Generic;

namespace BIL.DTO
{
    public class PostForReturnDTO
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
        public DateTime DateOfCreation { get; set; }
        public string UserKnownAs { get; set; }
        public string UserPhotoUrl { get; set; }
        public ICollection<LikersDTO> Likers { get; set; }
    }
}
