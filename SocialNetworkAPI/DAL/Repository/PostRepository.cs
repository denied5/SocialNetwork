using DAL.Data;
using DAL.Models;
using DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repository
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        private readonly DataContext _context;
        public PostRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
