using DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private DbSet<T> _context { get; }
        public Repository(DbContext context)
        {
            _context = context.Set<T>();
        }

        public void Add(T value)
        {
           _context.Add(value);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _context.FindAsync(id);
        }

        public void Remove(T value)
        {
            _context.Remove(value);
        }
    }
}