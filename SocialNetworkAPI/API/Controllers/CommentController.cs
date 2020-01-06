using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.Helpers;
using BIL.DTO;
using BIL.Helpers;
using BIL.Services.Interrfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ServiceFilter(typeof(UpdateUserActivityFilter))]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("id", Name ="GetComment")]
        public async Task<IActionResult> GetComment(int id)
        {
            var commentFromRepo = await _commentService.GetComment(id);

            if (commentFromRepo == null)
            {
                return BadRequest("Comment doesn't exsist");
            }

            return Ok(commentFromRepo);
        }

        [HttpPost] 
        public async Task<IActionResult> SetComment([FromBody]CommentToCreateDTO comment)
        {
            if (comment.UserId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }
            if (comment.Content == null)
            {
                return BadRequest("Comment Without Content");
            }

            var commentFromRepo = await _commentService.SetComment(comment);
            if (commentFromRepo != null)
            {
                //TODO: CreatedAtRoute
                return CreatedAtRoute("GetComment", new { id = commentFromRepo.Id }, commentFromRepo);
            }
            return BadRequest("Fail on save");
        }

        [HttpDelete("{commentId}")]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (await _commentService.DeleteComment(userId, commentId))
            {
                return NoContent();
            }
            return BadRequest("Fail To Delete");
        }


    }
}