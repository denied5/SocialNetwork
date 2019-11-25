using BIL.DTO;
using BIL.Services.Interrfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IPhotoService _photoService;

        public AdminController(IAdminService adminService, IPhotoService photoService)
        {
            _adminService = adminService;
            _photoService = photoService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("users/roles")]
        public async Task<IActionResult> GetUserWithRole()
        {
            var userToReturn = await _adminService.GetUsersWithRoles();

            return Ok(userToReturn);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("editRoles/{userName}")]
        public async Task<IActionResult> EditRoles(string userName, RoleEditDTO roleEditDTO)
        {
            var roles = await _adminService.EditRoles(userName, roleEditDTO);
            if (roles == null)
            {
                return BadRequest("Fail tto Change Roles");
            }

            return Ok(roles);
        }

        [Authorize(Roles = "Moderator, Admin")]
        [HttpPut("photos/{photoId}")]
        public async Task<IActionResult> ApprovePhoto(int photoId)
        {
            var photo = await _adminService.ApprovePhoto(photoId);
            if (photo == null)
            {
                return BadRequest("Photo doesn't exsist");
            }

            return Ok(photo);
        }

        [Authorize(Roles = "Moderator, Admin")]
        [HttpDelete("photos/{photoId}")]
        public async Task<IActionResult> DeletePhoto(int photoId)
        {
            if (await _adminService.DeletePhoto(photoId))
            {
                return Ok();
            }
            return BadRequest("Photo doesn't exsist");
        }


        [Authorize(Roles = "Moderator")]
        [HttpGet("photos/moderation")]
        public async Task<IActionResult> GetPhotosForModerator()
        {
            var photo = await _adminService.GetPhotosForModerator();
            if (photo == null)
            {
                return NoContent();
            }

            return Ok(photo);
        }
    }
}