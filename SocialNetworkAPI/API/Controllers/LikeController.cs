using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BIL.Services.Interrfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/posts/{postId}/like/{userId}")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly ILikeService _likeService;

        public LikeController(ILikeService likeService)
        {
            _likeService = likeService;
        }

        [HttpPost]
        public async Task<IActionResult> SetLike(int postId, int userId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
               return Unauthorized();

            if (await _likeService.SetLike(userId, postId))
            {
                return NoContent();
            }
            return BadRequest("Fail on save");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteLike (int postId, int userId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            if (await _likeService.DeleteLike(userId, postId))
            {
                return NoContent();
            }
            return BadRequest("Fail To Delete");
        }
    }
}