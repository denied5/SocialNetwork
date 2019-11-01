using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BIL.DTO;
using BIL.Helpers;
using BIL.Services.Interrfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DAL.Models;
using DAL.UnitOfWork;
using Microsoft.Extensions.Options;

namespace BIL.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IOptions<CloudinarySettings> _cloudinoryConfig;
        private readonly Cloudinary _cloudinary;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PhotoService(IUnitOfWork unitOfWork, IMapper mapper,
            IOptions<CloudinarySettings> cloudinoryConfig)
        {
            _cloudinoryConfig = cloudinoryConfig;
            _mapper = mapper;
            _unitOfWork = unitOfWork;

            Account acc = new Account(
                _cloudinoryConfig.Value.CloudName,
                _cloudinoryConfig.Value.ApiKey,
                _cloudinoryConfig.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(acc);
        }

        public async Task<PhotoForReturnDTO> GetPhoto(int id)
        {
            var photo = await _unitOfWork.PhotoRepository.GetById(id);
            var photoToReturn = _mapper.Map<PhotoForReturnDTO>(photo);
            return photoToReturn;
        }

        private PhotoForCreationDTO AddPhotoInCloudinary(PhotoForCreationDTO photoForCreationDTO)
        {
            var file = photoForCreationDTO.File;
            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream),
                        Transformation = new Transformation()
                            .Width(500).Height(500).Crop("fill").Gravity("face")
                    };

                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }

            var photoToReturn = photoForCreationDTO;
            photoToReturn.URL = uploadResult.Uri.ToString();
            photoToReturn.PublicId = uploadResult.PublicId;
            return photoToReturn;
        }

        public async Task<PhotoForReturnDTO> AddPhotoForUser(int userId, PhotoForCreationDTO photoForCreationDTO)
        {
            var userFromRepo = await _unitOfWork.UserRepository.GetUser(userId);

            var photoFromCloudinary = AddPhotoInCloudinary(photoForCreationDTO);
            var photoToUpload = _mapper.Map<Photo>(photoFromCloudinary);

            var photo = await _unitOfWork.PhotoRepository.AddPhotoForUser(photoToUpload, userFromRepo);

            if (await _unitOfWork.SaveChanges())
            {
                return _mapper.Map<PhotoForReturnDTO>(photo);
            }
            throw new Exception("Fail on save photo");
        }

        public async Task<bool> SetMainPhoto(int userId, int photoId)
        {
            var user = await _unitOfWork.UserRepository.GetUser(userId);
            if (!user.Photos.ToList().Any(p => p.Id == photoId))
               throw new Exception("Photo don't exsist");

            var photoFromRepo = await _unitOfWork.PhotoRepository.GetById(photoId);

            if (photoFromRepo.IsMain == true)
                throw new Exception("Photo already main");

            var currentMainPhoto = await _unitOfWork.PhotoRepository.GetMainPhotoForUser(userId);
            currentMainPhoto.IsMain = false;
            photoFromRepo.IsMain = true;

            if (await _unitOfWork.SaveChanges())
                return true;
            return false;
        }

        public async Task<bool> DeletePhoto (int userId, int photoId)
        {
            var user = await _unitOfWork.UserRepository.GetUser(userId);
            if (!user.Photos.ToList().Any(p => p.Id == photoId))
               throw new Exception("Photo don't exsist");

            var photoFromRepo = await _unitOfWork.PhotoRepository.GetById(photoId);

            if (photoFromRepo.IsMain == true)
                throw new Exception("This photo is main. Set New Main Photo First");

            var deleteParams = new DeletionParams(photoFromRepo.PublicId);
            var response = _cloudinary.Destroy(deleteParams);

            if (response.Result == "ok")
            {
                _unitOfWork.PhotoRepository.Remove(photoFromRepo);
            }

            if (await _unitOfWork.SaveChanges())
               return true;

            return false;
        }
    }
}