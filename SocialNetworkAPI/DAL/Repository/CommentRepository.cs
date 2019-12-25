using DAL.Data;
using DAL.Models;
using DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        private readonly DataContext _context;
        public CommentRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Comment> GetComment(int id)
        {
            return await _context.Comments.Include(x => x.User).ThenInclude(p => p.Photos)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
