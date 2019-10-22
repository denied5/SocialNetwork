using System.Collections.Generic;

namespace DAL.Repository.Interfaces
{
    public interface IRepository<T>  where T: class
    {
         T GetById(int id);
         IEnumerable<T> GetAll();
         void Add(T value);
         void Remove(T value);
    }
}