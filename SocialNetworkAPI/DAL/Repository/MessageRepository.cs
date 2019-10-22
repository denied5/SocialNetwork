using System.Collections.Generic;
using DAL.Data;
using DAL.Models;
using DAL.Repository.Interfaces;

namespace DAL.Repository
{
    public class MessageRepository :Repository<Message>,   IMessageRepository
    {
        private readonly DataContext _context;

        public MessageRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}