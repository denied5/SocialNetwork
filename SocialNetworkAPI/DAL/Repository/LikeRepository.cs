using DAL.Data;
using DAL.Models;
using DAL.Repository.Interfaces;

namespace DAL.Repository
{
    public class LikeRepository : Repository<Like>, ILikeRepository
    {
        private readonly DataContext _context;

        public LikeRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
