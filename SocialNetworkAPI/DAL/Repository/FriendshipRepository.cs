using DAL.Data;
using DAL.Models;
using DAL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class FriendshipRepository: Repository<Friendship>, IFriendshipRepository
    {
        private readonly DataContext _context;
        public FriendshipRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public Friendship GetFriendship(int senderId, int recipientId)
        {
            return _context.Friendships.FirstOrDefault(u => u.SenderId == senderId && u.RecipientId == recipientId);
        }

        public IEnumerable<Friendship> GetFriendshipsSent(int userid)
        {
            return _context.Friendships.Where(u => u.SenderId == userid);
        }

        public IEnumerable<Friendship> GetFriendshipsRequest(int userid)
        {
            return _context.Friendships.Where(u => u.RecipientId == userid);
        }

    }
}
