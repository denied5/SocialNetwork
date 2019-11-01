﻿using BIL.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BIL.Services.Interrfaces
{
    public interface IFrienshipService
    {
        Task<bool> IsFriendshipExsist(int senderId, int recipientId);
        Task<bool> AddFriend(int senderId, int recipientId);
        Task<IEnumerable<UserForListDTO>> GetFriends(int userId);
        Task<bool> DeleteFriendship(int userId, int recipientId);
    }
}
