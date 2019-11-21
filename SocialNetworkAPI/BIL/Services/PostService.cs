using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BIL.DTO;
using BIL.Helpers;
using BIL.Services.Interrfaces;
using DAL.Models;
using DAL.UnitOfWork;

namespace BIL.Services
{
    public class PostService: IPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFrienshipService _frienshipService;

        public PostService(IUnitOfWork unitOfWork, IMapper mapper, IFrienshipService frienshipService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _frienshipService = frienshipService;
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

        public async Task<PagedList<PostForReturnDTO>> GetFeed(PagedListParams param)
        {
            var feedFromRepo = _unitOfWork.PostRepository.GetPosts(param.UserId);
            var friendsId = (await _frienshipService.GetFriends(param.UserId)).Select(f => f.Id);

            foreach (var id in friendsId)
            {
                feedFromRepo = feedFromRepo.Concat(_unitOfWork.PostRepository.GetPosts(id));
            }
            feedFromRepo = feedFromRepo.OrderByDescending(p => p.DateOfCreation);
            if (feedFromRepo.Count() == 0)
                return null;

            var feedToReturn = _mapper.Map<IEnumerable<PostForReturnDTO>>(feedFromRepo);

            return PagedList<PostForReturnDTO>.Create(feedToReturn, param.CurrentPage, param.PageSize);
        }

        public async Task<PostForReturnDTO> GetPost(int id)
        {
            var postForReturn = await _unitOfWork.PostRepository.GetPost(id);

            return _mapper.Map<PostForReturnDTO>(postForReturn);
        }

        public IEnumerable<PostForReturnDTO> GetPosts(int userId)
        {
            var postsForReturn = _unitOfWork.PostRepository.GetPosts(userId);

            return _mapper.Map<IEnumerable<PostForReturnDTO>>(postsForReturn);
        }

        public async Task<bool> DeletePost(int id)
        {
            var postToDelete = await _unitOfWork.PostRepository.GetPost(id);

            _unitOfWork.PostRepository.Remove(postToDelete);

            return await _unitOfWork.SaveChanges();
        }
    }
}
