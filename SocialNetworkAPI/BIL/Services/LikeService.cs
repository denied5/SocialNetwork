using AutoMapper;
using BIL.Services.Interrfaces;
using DAL.Models;
using DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
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

        public async Task<bool> SetLike(int likerId, int postId)
        {
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
