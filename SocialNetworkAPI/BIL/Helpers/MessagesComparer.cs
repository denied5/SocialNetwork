using DAL.Models;
using System.Collections.Generic;

namespace BIL.Helpers
{
    public class MessagesComparer : IEqualityComparer<Message>
    {

        public bool Equals(Message x, Message y)
        {
            return x.RecipientId == y.RecipientId &&
                x.SenderId == y.SenderId ||
                x.RecipientId == y.SenderId &&
                x.SenderId == y.RecipientId;
        }

        public int GetHashCode(Message message)
        {
            return message.SenderId.GetHashCode() ^ message.RecipientId.GetHashCode();
        }
    }
}
