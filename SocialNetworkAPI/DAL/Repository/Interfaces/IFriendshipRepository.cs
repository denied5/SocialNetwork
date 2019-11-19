using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Interfaces
{
    public interface IFriendshipRepository : IRepository<Friendship>
    {
        Friendship GetFriendship(int senderId, int recipientId);
        IEnumerable<Friendship> GetFriendshipsSent(int userid);
        IEnumerable<Friendship> GetFriendshipsRequest(int userid);
    }
}
