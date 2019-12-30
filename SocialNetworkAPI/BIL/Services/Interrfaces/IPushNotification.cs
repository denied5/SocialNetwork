using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BIL.Services.Interrfaces
{
    public interface IPushNotification
    {
        Task NewMessage(string fromUserKnownAs, string toUserToken);
        Task NewFriendRequest(string toUserToken);
    }
}
