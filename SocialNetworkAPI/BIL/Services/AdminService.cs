using AutoMapper;
using BIL.DTO;
using BIL.Services.Interrfaces;
using DAL.Models;
using DAL.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BIL.Services
{
    class AdminService : IAdminService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IPhotoService _photoService;

        public AdminService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager,
            IPhotoService photoService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _photoService = photoService;
        }

        public async Task<List<UserWithRoles>> GetUsersWithRoles()
        {
            var users = await _unitOfWork.UserRepository.GetUsers();
            var roles = await _unitOfWork.RoleRepository.GetAll();
            var userList = (from user in users
                            orderby user.UserName
                            select new UserWithRoles
                            {
                                Id = user.Id,
                                UserName = user.UserName,
                                Roles = (from userRole in user.UserRoles
                                         join role in roles
                                         on userRole.RoleId equals role.Id
                                         select role.Name).ToList()
                            }).ToList();

            return userList;
        }

        public async Task<IList<string>> EditRoles(string userName, RoleEditDTO roleEditDTO)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var userRoles = await _userManager.GetRolesAsync(user);

            var selectedRoles = roleEditDTO.RoleNames;
            selectedRoles = selectedRoles ?? new string[] { };

            var result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));

            if (!result.Succeeded)
            {
                return null;
            }

            result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));

            if (!result.Succeeded)
            {
                return null;
            }

            return await _userManager.GetRolesAsync(user);
        }

        public async Task<IEnumerable<PhotoForReturnDTO>> GetPhotosForModerator()
        {
            var photos = await _unitOfWork.PhotoRepository.GetUnapprovedPhotos();
            var photosForModerator = photos?.Where(p => p.Approved == false);

            return _mapper.Map<IEnumerable<PhotoForReturnDTO>>(photosForModerator);
        }

        public async Task<PhotoForReturnDTO> ApprovePhoto(int photoId)
        {
            var photoFromRepo = await _unitOfWork.PhotoRepository.GetUnapprovedPhoto(photoId);
            if (photoFromRepo == null)
            {
                return null;
            }

            photoFromRepo.Approved = true;
            if (await _unitOfWork.SaveChanges())
            {
                return _mapper.Map<PhotoForReturnDTO>(photoFromRepo);
            }
            throw new Exception("Fail on save photo");
        }

        public async Task<bool> DeletePhoto(int photoId)
        {
            var photo = await _unitOfWork.PhotoRepository.GetUnapprovedPhoto(photoId);

            return await _photoService.DeletePhoto(photo.UserId, photo.Id);
        }
    }
}
