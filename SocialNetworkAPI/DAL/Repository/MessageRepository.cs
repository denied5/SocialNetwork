using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Data;
using DAL.Models;
using DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class MessageRepository :Repository<Message>,   IMessageRepository
    {
        private readonly DataContext _context;

        public MessageRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Message>> GetMessages(int userId)
        {
            var messages =  _context.Messages.Include(u => u.Sender).ThenInclude(p => p.Photos)
                                             .Include(u => u.Recipient).ThenInclude(p => p.Photos)
                                             .Where(u => u.SenderId == userId || u.RecipientId == userId);
            return messages;
        }
    }
}