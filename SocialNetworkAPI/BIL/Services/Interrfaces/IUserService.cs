using BIL.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BIL.Services.Interrfaces
{
    public interface IUserService
    {
        Task<bool> AddUser(UserForListDTO user);
    }
}
