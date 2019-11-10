using DAL.Models;
using DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repository
{
    public class LikeRepository : Repository<Like>, ILikeRepository
    {
        private readonly DbContext _context;

        public LikeRepository(DbContext context) : base(context)
        {
            _context = context;
        }
    }
}
