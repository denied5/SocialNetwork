using AutoMapper;
using BIL.DTO;
using BIL.Helpers;
using BIL.Services.Interrfaces;
using DAL.Data;
using DAL.Models;
using DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BIL.Services
{
    public class PostService : IPostService
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

        public async Task<PostForReturnDTO> CreatePost(PostForCreationDTO post)
        {
            var postToCreate = _mapper.Map<Post>(post);
            _unitOfWork.PostRepository.Add(postToCreate);

            if (postToCreate.Content.Length > EntitysRestrictions.POST_MAX_LENGTH)
            {
                postToCreate.Content = postToCreate.Content.Substring(0,
                    EntitysRestrictions.POST_MAX_LENGTH);
            }

            if (await _unitOfWork.SaveChanges())
            {
                return _mapper.Map<PostForReturnDTO>(postToCreate);
            }

            throw new Exception("Fail on save your post");
        }

        public async Task<PagedList<PostForReturnDTO>> GetFeed(PagedListParams param)
        {
            var feedFromRepo =  _unitOfWork.PostRepository.GetPosts(param.UserId);
            var followingsId = (await _frienshipService.GetFollowing(param.UserId)).Select(f => f.Id);
            var friendsId = (await _frienshipService.GetFriends(param.UserId)).Select(f => f.Id);

            foreach (var id in followingsId)
            {
                feedFromRepo = feedFromRepo.Concat(_unitOfWork.PostRepository.GetPosts(id));
            }
            foreach (var id in friendsId)
            {
                feedFromRepo = feedFromRepo.Concat(_unitOfWork.PostRepository.GetPosts(id));
            }
            feedFromRepo = feedFromRepo.OrderByDescending(p => p.DateOfCreation);
            if (feedFromRepo.Count() == 0)
            {
                return null;
            }

            var feedToReturn = _mapper.Map<IEnumerable<PostForReturnDTO>>(feedFromRepo);

            return PagedList<PostForReturnDTO>.Create(feedToReturn, param.CurrentPage, param.PageSize);
        }

        public async Task<PostForReturnDTO> GetPost(int id)
        {
            var postForReturn = await _unitOfWork.PostRepository.GetPost(id);

            return _mapper.Map<PostForReturnDTO>(postForReturn);
        }

        public async  Task<IEnumerable<PostForReturnDTO>> GetPosts(int userId)
        {
            var postsForReturn = _unitOfWork.PostRepository.GetPosts(userId);
            foreach (var item in postsForReturn)
            {
                item.Comments.OrderBy(x => x.DateOfCreation);
            }

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
