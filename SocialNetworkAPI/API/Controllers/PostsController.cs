using BIL.DTO;
using BIL.Extensions;
using BIL.Helpers;
using BIL.Services.Interrfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace api.Controllers
{
    [ServiceFilter(typeof(UpdateUserActivityFilter))]
    [ApiController]
    [Authorize]
    [Route("api/users/{userId}/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IUserService _userService;

        public PostsController(IPostService postService, IUserService userService)
        {
            _postService = postService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts(int userId)
        {
            var userFromRepo = await _userService.GetUser(userId);
            if (userFromRepo == null)
            {
                return BadRequest("User don't exsist");
            }

            var postsToRetrun = _postService.GetPosts(userId);
            return Ok(postsToRetrun);
        }

        [HttpGet("{id}", Name = "GetPost")]
        public async Task<IActionResult> GetPost(int userId, int id)
        {
            var userFromRepo = await _userService.GetUser(userId);
            if (userFromRepo == null)
            {
                return BadRequest("User don't exsist");
            }

            var postToRetrun = await _postService.GetPost(id);
            return Ok(postToRetrun);
        }

        [HttpGet("feed")]
        public async Task<IActionResult> GetFeed(int userId, [FromQuery]PagedListParams postParams)
        {
            if (postParams == null)
            {
                return BadRequest();
            }
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            postParams.UserId = userId;

            var feedFromRepo = await _postService.GetFeed(postParams);
            if (feedFromRepo == null)
            {
                return NoContent();
            }

            Response.AddPagination(feedFromRepo.CurrentPage, feedFromRepo.PageSize,
                feedFromRepo.TotalCount, feedFromRepo.TotalPages);

            List<PostForReturnDTO> feedToReturn = feedFromRepo;
            return Ok(feedToReturn);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(int userId, PostForCreatinDTO post)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            post.UserId = userId;

            var postToReturn = await _postService.CreatePost(post);

            if (postToReturn != null)
            {
                return CreatedAtRoute("GetPost", new { id = postToReturn.Id }, postToReturn);
            }

            return BadRequest("Fail to Save your Post");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            if (await _postService.DeletePost(id))
            {
                return NoContent();
            }
            return BadRequest("Fail On Delete");
        }
    }
}