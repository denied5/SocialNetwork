using AutoMapper;
using BIL.Services.Interrfaces;
using DAL.Models;
using DAL.UnitOfWork;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BIL.Services
{
    public class LikeService : ILikeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LikeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> DeleteLike(int userId, int postId)
        {
            var likes = await _unitOfWork.LikeRepository.GetAll();
            var like = likes.Where(p => p.UserId == userId && p.PostId == postId).FirstOrDefault();
            if (like == null)
            {
                throw new Exception("Like don't exsist");
            }

            _unitOfWork.LikeRepository.Remove(like);

            return await _unitOfWork.SaveChanges();
        }

        public async Task<bool> SetLike(int likerId, int postId)
        {
            var likes = await _unitOfWork.LikeRepository.GetAll();
            if (likes.Any(p => p.PostId == postId && p.UserId == likerId))
            {
                throw new Exception("Like already exsist");
            }
            Like like = new Like
            {
                PostId = postId,
                UserId = likerId
            };

            _unitOfWork.LikeRepository.Add(like);

            return await _unitOfWork.SaveChanges();
        }
    }
}
