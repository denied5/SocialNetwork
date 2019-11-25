using DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repository.Interfaces
{
    public interface IMessageRepository : IRepository<Message>
    {
        Task<IEnumerable<Message>> GetMessages(int userId);
    }
}