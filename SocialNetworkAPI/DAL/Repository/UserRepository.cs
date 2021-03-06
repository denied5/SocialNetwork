using DAL.Data;
using DAL.Models;
using DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return _context.Users.Include(p => p.Photos)
                .Include(f => f.FriendshipsSent)
                .Include(f => f.FriendshipsReceived)
                .Include(r => r.UserRoles);
        }

        public async Task<User> GetUser(int id, bool isCurrentUser = false)
        {
            var query = _context.Users
                .Include(p => p.Photos)
                .Include(f => f.FriendshipsSent)
                .Include(f => f.FriendshipsReceived).AsQueryable();

            if (isCurrentUser)
            {
                query = query.IgnoreQueryFilters();
            }

            var user = await query.FirstOrDefaultAsync(x => x.Id == id);

            return user;
        }

        public async Task<User> GetMainUser(string username)
        {
            return await _context.Users.Include(p => p.Photos).FirstOrDefaultAsync(x => x.UserName.ToLower() == username.ToLower());
        }

        public async Task<bool> UserExsist(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.UserName == username) == null ? false : true;
        }
    }
}