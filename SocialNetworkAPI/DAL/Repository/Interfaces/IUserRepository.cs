using DAL.Models;
using System.Threading.Tasks;

namespace DAL.Repository.Interfaces
{
    public interface IUserRepository:IRepository<User>
    {
        Task<bool> UserExsist(string username);
        Task<User> GetMainUser(string username);
        Task<User> GetUser(int id);
    }
}