using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BIL.DTO
{
    public class CommentToCreateDTO
    {
        public CommentToCreateDTO(DateTime dateOfCreation)
        {
            DateOfCreation = DateTime.Now;
        }
        [Required]
        public string Content { get; set; }
        [Required]
        public int? UserId { get; set; }
        [Required]
        public int? PostId { get; set; }
        public DateTime DateOfCreation { get; set; }
    }
}
