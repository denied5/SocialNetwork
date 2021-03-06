﻿using AutoMapper;
using BIL.DTO;
using BIL.Services.Interrfaces;
using DAL.Models;
using DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BIL.Services
{
    public class FrienshipService : IFrienshipService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPushNotification _pushNotification;

        public FrienshipService(IUnitOfWork unitOfWork, IMapper mapper, IPushNotification pushNotification)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _pushNotification = pushNotification;
        }

        public async Task<bool> IsFriendshipExsist(int senderId, int recipientId)
        {
            var friendship = await _unitOfWork.FriendshipRepository.GetFriendship(senderId, recipientId);
            if (friendship == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> AddFriend(int senderId, int recipientId)
        {
            Friendship friendship = new Friendship
            {
                RecipientId = recipientId,
                SenderId = senderId
            };

            var recipientToken = (await _unitOfWork.UserRepository.GetById(recipientId)).FairbaseToken;
            if (recipientToken != null)
            {
                await _pushNotification.NewFriendRequest(recipientToken);
            }

            _unitOfWork.FriendshipRepository.Add(friendship);
            if (await _unitOfWork.SaveChanges())
            {
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<UserForListDTO>> GetFriends(int userId)
        {
            var friendshipsRequsted = (await _unitOfWork.FriendshipRepository.GetFriendshipsRequest(userId)).ToList();
            var friendshipsSent = (await _unitOfWork.FriendshipRepository.GetFriendshipsSent(userId)).ToList();
            var friendsFromRepo = await _unitOfWork.UserRepository.GetUsers();

            var friendsId = friendshipsSent.Where(s =>
                      friendshipsRequsted.Where(r => r.RecipientId == s.SenderId
                                                 && r.SenderId == s.RecipientId)
                                                 .FirstOrDefault() != null)
                                         .Select(i => i.RecipientId);

            friendsFromRepo = friendsFromRepo.Where(u => friendsId.Contains(u.Id));
            var friendsToReturn = _mapper.Map<IEnumerable<UserForListDTO>>(friendsFromRepo);

            return friendsToReturn;
        }

        public async Task<IEnumerable<UserForListDTO>> GetFollowers(int userId)
        {
            var friendshipsRequsted = await _unitOfWork.FriendshipRepository.GetFriendshipsRequest(userId);
            var friendshipsSent = await _unitOfWork.FriendshipRepository.GetFriendshipsSent(userId);
            var requstsFromRepo = await _unitOfWork.UserRepository.GetUsers();

            var followersId = friendshipsRequsted.Where(r =>
                                friendshipsSent.Where(s => s.SenderId == r.RecipientId && s.RecipientId == r.SenderId)
                                    .FirstOrDefault() == null)
                                .Select(i => i.SenderId);

            requstsFromRepo = requstsFromRepo.Where(u => followersId.Contains(u.Id));

            var requstsToReturn = _mapper.Map<IEnumerable<UserForListDTO>>(requstsFromRepo);

            return requstsToReturn;
        }

        public async Task<IEnumerable<UserForListDTO>> GetFollowing(int userId)
        {
            var friendshipsRequsted = await _unitOfWork.FriendshipRepository.GetFriendshipsRequest(userId);
            var friendshipsSent = await _unitOfWork.FriendshipRepository.GetFriendshipsSent(userId);
            var requstsFromRepo = await _unitOfWork.UserRepository.GetUsers();

            var followingId = friendshipsSent.Where(r =>
                                friendshipsRequsted.Where(s => s.SenderId == r.RecipientId && s.RecipientId == r.SenderId)
                                    .FirstOrDefault() == null)
                                .Select(i => i.RecipientId);

            requstsFromRepo = requstsFromRepo.Where(u => followingId.Contains(u.Id));

            var requstsToReturn = _mapper.Map<IEnumerable<UserForListDTO>>(requstsFromRepo);

            return requstsToReturn;
        }

        public async Task<bool> DeleteFriendship(int userId, int recipientId)
        {
            var userFromRepo = await _unitOfWork.UserRepository.GetUser(userId);
            if (userFromRepo.FriendshipsSent.Any(f => f.RecipientId == recipientId))
            {
                throw new Exception("User don't have this friend");
            }
            var friendshipToDelete = (await _unitOfWork.FriendshipRepository.GetAll())
                                    .Where(u => u.SenderId == userId && u.RecipientId == recipientId)
                                    .FirstOrDefault();
            _unitOfWork.FriendshipRepository.Remove(friendshipToDelete);

            if (await _unitOfWork.SaveChanges())
            {
                return true;
            }
            return false;
        }
    }
}
