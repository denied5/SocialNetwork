using DAL.Data;
using DAL.Models;
using DAL.Repository.Interfaces;

namespace DAL.Repository
{
    public class AuthRepository : Repository<User>, IAuthRepository
    {
        private readonly DataContext _context;
        public AuthRepository(DataContext context) : base(context)
        {
            this._context = context;
        }
    }
}