using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BIL.DTO;
using BIL.Services.Interrfaces;
using DAL.Models;
using DAL.UnitOfWork;

namespace BIL.Services
{
    public class PostService: IPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PostService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PostForReturnDTO> CreatePost(PostForCreatinDTO post)
        {
            var postToCreate = _mapper.Map<Post>(post);
            _unitOfWork.PostRepository.Add(postToCreate);

            if (await _unitOfWork.SaveChanges())
            {
                return _mapper.Map<PostForReturnDTO>(postToCreate);
            }

            throw new Exception("Fail on save your post"); 
        }

        public async Task<IEnumerable<PostForReturnDTO>> GetFeed(int userId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<PostForReturnDTO> GetPost(int id)
        {
            var postForReturn = await _unitOfWork.PostRepository.GetPost(id);

            return _mapper.Map<PostForReturnDTO>(postForReturn);
        }

        public async Task<IEnumerable<PostForReturnDTO>> GetPosts(int userId)
        {
            var postsForReturn = _unitOfWork.PostRepository.GetPosts(userId);

            return _mapper.Map<IEnumerable<PostForReturnDTO>>(postsForReturn);
        }
    }
}
