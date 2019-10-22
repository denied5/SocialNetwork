using System.Collections.Generic;
using DAL.Data;
using DAL.Models;
using DAL.Repository.Interfaces;

namespace DAL.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public void Add(User value)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public User GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(User value)
        {
            throw new System.NotImplementedException();
        }
    }
}