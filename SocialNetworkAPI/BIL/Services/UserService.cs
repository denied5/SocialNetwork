using AutoMapper;
using BIL.DTO;
using BIL.Services.Interrfaces;
using DAL.Models;
using DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BIL.Services
{
    public class UserService: IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> UpdateUser(int id, UserForUpdateDTO userForUpdate)
        {
            if (userForUpdate == null)
                return false;

            var userFromRepo = await _unitOfWork.UserRepository.GetUser(id);
            if (userFromRepo == null)
                throw new Exception("User dont't exsist");
            
            _mapper.Map(userForUpdate, userFromRepo);

            if (await _unitOfWork.SaveChanges())
            {
                return true;
            }
            throw new Exception("Update failed in save");
        }

        public async Task<bool> AddUser(UserForListDTO user)
        {
            if (user == null)
            {
                return false;
            }

            var u = _mapper.Map<User>(user);

            _unitOfWork.UserRepository.Add(u);
            if (await _unitOfWork.SaveChanges())
            {
                return true;
            }
            return false;
        }

        public async Task<UserForDetailedDTO> GetUser(int id)
        {
            var userFromRepo = await _unitOfWork.UserRepository.GetUser(id);

            var userToReturn = _mapper.Map<UserForDetailedDTO>(userFromRepo);
            return userToReturn;
        }

        public async Task<IEnumerable<UserForListDTO>> GetUsers()
        {
            var usersFromRepo = await _unitOfWork.UserRepository.GetAll();
            var usersToReturn = _mapper.Map<IEnumerable<UserForListDTO>>(usersFromRepo);
            return usersToReturn;
        } 

        public async Task<bool> UserExsist(string username)
        {
            return await _unitOfWork.UserRepository.UserExsist(username);
        }
    }
}
