using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.Helpers;
using API.Helpers;
using BIL.DTO;
using BIL.Helpers;
using BIL.Services.Interrfaces;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace API.Controllers
{
    [ServiceFilter(typeof(UpdateUserActivityFilter))]
    [Authorize]
    [ApiController]
    [Route("api/users/{userId}/photos")]
    public class PhotosController : ControllerBase
    {
        private readonly IPhotoService _photoService;
        private readonly IUserService _userService;

        public PhotosController(IPhotoService photoService,
             IUserService  userService)
        {
            _photoService = photoService;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> AddPhotoForUser(int userId,
            [FromForm]PhotoForCreationDTO photoForCreationDTO)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var photoToReturn = await _photoService.AddPhotoForUser(userId, photoForCreationDTO);

            if(photoToReturn != null)
            {
                return CreatedAtRoute("GetPhoto", new { id = photoToReturn.Id }, photoToReturn);
            }

            return BadRequest("Can't upload photos");
        }

        [HttpGet("{id}", Name = "GetPhoto")]
        public async Task<IActionResult> GetPhoto(int id)
        {
            var photoToReturn = await _photoService.GetPhoto(id);
            return Ok(photoToReturn);
        }

        [HttpPost("{id}/setMain")]
        public async Task<IActionResult> SetMainPhoto(int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            if(await _photoService.SetMainPhoto(userId, id))
            {
                return NoContent();
            }

            return BadRequest("Could't set photo to main");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhoto (int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            if(await _photoService.DeletePhoto(userId, id))
                return Ok();

            return BadRequest("Fail to delete");
        }
    }
}