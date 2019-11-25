using BIL.DTO;
using BIL.Helpers;
using System.Threading.Tasks;

namespace BIL.Services.Interrfaces
{
    public interface IUserService
    {
        Task<bool> AddUser(UserForListDTO user);
        Task<bool> UserExsist(string username);
        Task<UserForDetailedDTO> GetUser(int id, bool isCurrentUser = false);
        Task<PagedList<UserForListDTO>> GetUsers(UserParams userParams);
        Task<bool> UpdateUser(int id, UserForUpdateDTO userForUpdate);
        Task<bool> UpdateUserActivity(int userId);
    }
}
