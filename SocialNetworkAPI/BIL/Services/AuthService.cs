using AutoMapper;
using BIL.DTO;
using BIL.Services.Interrfaces;
using DAL.Models;
using DAL.UnitOfWork;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BIL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AuthService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public string GenerateToken(UserForListDTO user, string keyWord)
        {
            var claims = new[]
           {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyWord));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<UserForListDTO> LogIn(UserForLoginDTO user)
        {
            var mainUser = await _unitOfWork.UserRepository.GetMainUser(user.Username);
            if (mainUser == null)
                return null;

            Rfc2898DeriveBytes passwordHasher = new Rfc2898DeriveBytes(user.Password, mainUser.PasswordSalt, 1000);
            byte[] tempHash = passwordHasher.GetBytes(16);
            for (int i = 0; i < tempHash.Length; i++)
            {
                if (tempHash[i] != mainUser.PasswordHash[i])
                    return null;
            }
            return _mapper.Map<UserForListDTO>(mainUser);
        }

        public async Task<UserForRegisterDTO> Register(UserForRegisterDTO user)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            Rfc2898DeriveBytes passwordHasher = new Rfc2898DeriveBytes(user.Password, salt, 1000);
            var userToCreate = _mapper.Map<User>(user);
            userToCreate.PasswordSalt = salt;
            userToCreate.PasswordHash = passwordHasher.GetBytes(16);
            userToCreate.Username = user.Username.ToLower();

            _unitOfWork.UserRepository.Add(userToCreate);

            if (await _unitOfWork.SaveChanges())
                return user;

            return null;
        }
    }
}
