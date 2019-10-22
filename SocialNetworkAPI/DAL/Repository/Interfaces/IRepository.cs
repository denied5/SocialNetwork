using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repository.Interfaces
{
    public interface IRepository<T>  where T: class
    {
         Task<T> GetById(int id);
         Task<IEnumerable<T>> GetAll();
         void Add(T value);
         void Remove(T value);
    }
}