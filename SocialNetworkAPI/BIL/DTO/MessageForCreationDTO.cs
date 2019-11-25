using System;

namespace BIL.DTO
{
    public class MessageForCreationDTO
    {
        public int SenderId { get; set; }
        public int RecipientId { get; set; }
        public string Content { get; set; }
        public DateTime MessageSent { get; set; }
        public MessageForCreationDTO()
        {
            MessageSent = DateTime.Now;
        }
    }
}
