using DAL.Data;
using DAL.Models;
using DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        private readonly DataContext _context;
        public PostRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public Task<Post> GetPost(int id)
        {
            var postForRetrun = _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
            return postForRetrun;
        }

        public IEnumerable<Post> GetPosts(int userId)
        {
            var postsForReturn = _context.Posts
                                         .Where(p => p.UserId == userId)
                                         .Include(u => u.User)
                                         .Include(l => l.Likes)
                                         .Include(c => c.Comments)
                                         .OrderByDescending(p => p.DateOfCreation);

            return postsForReturn;
        }
    }
}
