using DAL.Data;
using DAL.Models;
using DAL.Repository.Interfaces;

namespace DAL.Repository
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        private readonly DataContext _context;

        public RoleRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
