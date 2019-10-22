using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Data;
using DAL.Models;
using DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> GetMainUser(string username)
        {
            return await _context.Users.Include(p => p.Photos).FirstOrDefaultAsync(x => x.Username.ToLower() == username.ToLower());
        }

        public async Task<bool> UserExsist(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Username == username) == null ? false : true;
        }
    }
}