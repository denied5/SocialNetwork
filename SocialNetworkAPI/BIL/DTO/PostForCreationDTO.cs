using System;
using System.ComponentModel.DataAnnotations;

namespace BIL.DTO
{
    public class PostForCreationDTO
    {
        public PostForCreationDTO()
        {
            DateOfCreation = DateTime.Now;
        }
        [Required]
        public string Content { get; set; }
        [Required]
        public int? UserId { get; set; }
        public DateTime DateOfCreation { get; set; }
    }
}
