﻿using AutoMapper;
using BIL.DTO;
using BIL.Helpers;
using BIL.Services.Interrfaces;
using DAL.Data;
using DAL.Models;
using DAL.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BIL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<bool> UpdateUser(int id, UserForUpdateDTO userForUpdate)
        {
            if (userForUpdate == null)
            {
                return false;
            }

            if (userForUpdate.LookingFor?.Length > EntitysRestrictions.USER_LOOKINGFOR_MAXLENGTH ||
                userForUpdate.Introduction?.Length > EntitysRestrictions.USER_INTRODUCTION_MAXLENGTH ||
                userForUpdate.Interests?.Length > EntitysRestrictions.USER_INTERESTS_MAXLENGTH)
            {
                throw new Exception("Invalid Data");
            }

            var userFromRepo = await _unitOfWork.UserRepository.GetUser(id);
            if (userFromRepo == null)
            {
                throw new Exception("User dont't exsist");
            }

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

        public async Task<UserForDetailedDTO> GetUser(int id, bool isCurrentUser = false)
        {
            var userFromRepo = await _unitOfWork.UserRepository.GetUser(id, isCurrentUser);
            if (userFromRepo == null)
            {
                throw new Exception("User don't exsist");
            }
            var userToReturn = _mapper.Map<UserForDetailedDTO>(userFromRepo);
            return userToReturn;
        }


        public async Task<bool> UpdateUserActivity(int userId)
        {
            var userFromRepo = await _unitOfWork.UserRepository.GetById(userId);
            userFromRepo.LastActive = DateTime.Now;

            return await _unitOfWork.SaveChanges();
        }

        public async Task<PagedList<UserForListDTO>> GetUsers(UserParams userParams)
        {
            var usersFromRepo = await _unitOfWork.UserRepository.GetUsers();
            List<User> members = new List<User>();

            foreach (var user in usersFromRepo)
            {
                bool isAdminContains = (await _userManager.GetRolesAsync(user)).Contains("Admin");
                bool isModeratorContains = (await _userManager.GetRolesAsync(user)).Contains("Moderator");
                if (!isAdminContains && !isModeratorContains)
                {
                    members.Add(user);
                }
            }

            var usersToReturn = _mapper.Map<IEnumerable<UserForListDTO>>(members);

            usersToReturn = SelectUsers(usersToReturn, userParams);

            return PagedList<UserForListDTO>.Create(usersToReturn,
                userParams.CurrentPage, userParams.PageSize);
        }

        private IEnumerable<UserForListDTO> SelectUsers(IEnumerable<UserForListDTO> users, UserParams userParams)
        {
            users = users.Where(u => u.Id != userParams.UserId);
            if (!string.IsNullOrEmpty(userParams.Gender) && userParams.Gender != "any")
            {
                users = users.Where(u => u.Gender == userParams?.Gender);
            }
            if (!string.IsNullOrEmpty(userParams.Name))
            {
                users = users.Where(u => u.KnownAs.ToLower().Contains(userParams.Name.ToLower()));
            }
            if (userParams.MinAge != 14 || userParams.MaxAge != 99)
            {
                users = users.Where(u => u.Age >= userParams.MinAge && u.Age <= userParams.MaxAge);
            }
            return users;
        }

        public async Task<bool> UserExsist(string username)
        {
            return await _unitOfWork.UserRepository.UserExsist(username);
        }
    }
}
