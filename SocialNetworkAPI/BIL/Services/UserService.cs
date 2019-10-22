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

        public async Task<bool> AddUser(UserForListDTO user)
        {
            if (user == null)
            {
                Console.WriteLine("Wowowowowowowowo");
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
    }
}
