using AutoMapper;
using BIL.DTO;
using BIL.Services.Interrfaces;
using DAL.Models;
using DAL.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthService(IUnitOfWork unitOfWork, IMapper mapper, 
            UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
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
            var mainUser = await _userManager.FindByNameAsync(user.Username);
            if (mainUser == null)
                return null;

            var result = await _signInManager
                .CheckPasswordSignInAsync(mainUser, user.Password, false);

            if (result.Succeeded)
            {
                var appUser = await _userManager.Users.Include(p => p.Photos)
                    .FirstOrDefaultAsync(u => u.NormalizedUserName == mainUser.UserName.ToUpper());

                return _mapper.Map<UserForListDTO>(appUser);
            }

            return null;
        }

        public async Task<UserForDetailedDTO> Register(UserForRegisterDTO user)
        {
            var userToCreate = _mapper.Map<User>(user);

            var result = await _userManager.CreateAsync(userToCreate, user.Password);

            var userToReturn = _mapper.Map<UserForDetailedDTO>(userToCreate);

            if (result.Succeeded)
            {
                return userToReturn;
            }
            return null;
        }
    }
}
