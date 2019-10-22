using System.Collections.Generic;
using DAL.Data;
using DAL.Models;
using DAL.Repository.Interfaces;

namespace DAL.Repository
{
    public class MessageRepository :Repository<User>,   IMessageRepository
    {
        private readonly DataContext _context;

        public MessageRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public void Add(Message value)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Message> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Message GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(Message value)
        {
            throw new System.NotImplementedException();
        }
    }
}