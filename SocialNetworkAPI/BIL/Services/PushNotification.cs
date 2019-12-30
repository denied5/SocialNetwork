using BIL.Helpers;
using BIL.Services.Interrfaces;
using FirebaseNet.Messaging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BIL.Services
{
    public class PushNotification: IPushNotification
    {
        private readonly FCMClient client;

        public PushNotification()
        {
            this.client = new FCMClient(FirebaseSettings.ServerKey);
        }

        public async Task NewMessage(string fromUserKnownAs, string toUserToken)
        {
            var message = new Message()
            {
                To = toUserToken,
                Notification = new AndroidNotification()
                {
                    Body = $"you have new message from {fromUserKnownAs}",
                    Title = "New Message",
                    Icon = "myIcon"
                },
            };
            var result = await client.SendMessageAsync(message);
        }

        public async Task NewFriendRequest(string toUserToken)
        {
            var message = new Message()
            {
                To = toUserToken,
                Notification = new AndroidNotification()
                {
                    Body = $"you have new FriendRequest",
                    Title = "FriendRequest",
                }
            };
            var result = await client.SendMessageAsync(message);
        }
    }
}
