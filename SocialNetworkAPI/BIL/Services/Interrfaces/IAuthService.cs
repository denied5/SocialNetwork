﻿using BIL.DTO;
using System.Threading.Tasks;

namespace BIL.Services.Interrfaces
{
    public interface IAuthService
    {
        Task<UserForDetailedDTO> Register(UserForRegisterDTO user);
        Task<UserForListDTO> LogIn(UserForLoginDTO user);
        Task<string> GenerateToken(UserForListDTO user, string keyWord);
    }
}