using System.Collections.Generic;

namespace BIL.DTO
{
    public class FriendsDTO
    {
        public IEnumerable<UserForListDTO> Friends { get; set; }
        public IEnumerable<UserForListDTO> Followers { get; set; }
        public IEnumerable<UserForListDTO> Followings { get; set; }
    }
}
