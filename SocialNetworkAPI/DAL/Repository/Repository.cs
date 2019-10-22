using System.Collections.Generic;
using DAL.Data;
using DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
    
        private DbSet<T> _context { get; }
        public Repository (DbContext context)
        {
            _context = context.Set<T>();
        }

        public void Add(T value)
        {
            
        }

        public IEnumerable<T> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public T GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(T value)
        {
            throw new System.NotImplementedException();
        }
    }
}