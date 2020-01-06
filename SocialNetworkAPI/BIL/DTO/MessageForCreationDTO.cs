using System;
using System.ComponentModel.DataAnnotations;

namespace BIL.DTO
{
    public class MessageForCreationDTO
    {
        public int? SenderId { get; set; }
        [Required]
        public int? RecipientId { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime MessageSent { get; set; }
        public MessageForCreationDTO()
        {
            MessageSent = DateTime.Now;
        }
    }
}
