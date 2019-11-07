using System.Security.Claims;
using System.Threading.Tasks;
using API.Helpers;
using BIL.DTO;
using BIL.Helpers;
using BIL.Services.Interrfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ServiceFilter(typeof(UpdateUserActivityFilter))]
    [Route("api/users/{userId}/friendship")]
    [ApiController]
    [Authorize]
    public class FriendsController : ControllerBase
    {

        private readonly IFrienshipService _frienshipService;
        private readonly IUserService _userService;

        public FriendsController(IFrienshipService frienshipService, IUserService userService )
        {
            _frienshipService = frienshipService;
            _userService = userService;
        }

        [HttpPost("{recipientId}")]
        public async Task<IActionResult> AddFriend(int userId, int recipientId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))//read value from token
                return Unauthorized();

            if (await _frienshipService.IsFriendshipExsist(userId, recipientId))
            {
                return BadRequest("This is your friend allready");
            }

            if (await _userService.GetUser(recipientId) == null)
            {
                return NotFound();
            }

            if (await _frienshipService.AddFriend(userId, recipientId))
            {
                return Ok();
            }
            return BadRequest("Failed to add Friend");
        }

        [HttpGet()]
        public async Task<IActionResult> GetFriends(int userId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))//read value from token
                return Unauthorized();

            FriendsDTO friends = new FriendsDTO
            {
                Friends = await _frienshipService.GetFriends(userId),
                Followers = await _frienshipService.GetFollowers(userId),
                Followings = await _frienshipService.GetFollowing(userId),
            };
            return Ok(friends);
        }

        [HttpDelete("{recipientId}")]
        public async Task<IActionResult> DeleteFriend(int userId, int recipientId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))//read value from token
                return Unauthorized();

            if (await _frienshipService.DeleteFriendship(userId, recipientId))
            {
                return Ok();
            }
            return BadRequest("Fail To Delete");
        }
    }
}