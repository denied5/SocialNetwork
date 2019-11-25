using DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repository.Interfaces
{
    public interface IFriendshipRepository : IRepository<Friendship>
    {
        Task<Friendship> GetFriendship(int senderId, int recipientId);
        Task<IEnumerable<Friendship>> GetFriendshipsSent(int userid);
        Task<IEnumerable<Friendship>> GetFriendshipsRequest(int userid);
    }
}
