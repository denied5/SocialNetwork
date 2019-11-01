
namespace DAL.Models
{
    public class Friendship
    {
        public int RecipientId { get; set; }
        public int SenderId { get; set; }
        public User Recipient { get; set; }
        public User Sender { get; set; }
    }
}
