using BIL.DTO;
using DAL.Models;
using System.Threading.Tasks;

namespace BIL.Services.Interrfaces
{
    public interface IAuthService
    {
        Task<UserForRegisterDTO> Register(UserForRegisterDTO user);
        Task<UserForListDTO> LogIn(UserForLoginDTO user);
        string GenerateToken(UserForListDTO user, string keyWord);
    }
}