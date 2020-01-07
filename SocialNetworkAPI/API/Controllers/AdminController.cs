using BIL.DTO;
using BIL.Extensions;
using BIL.Services.Interrfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userParams">Params for Pagiantion</param>
        /// <returns>Ok with User</returns>
        [Authorize(Roles = "Admin")]
        [HttpGet("users/roles")]
        public async Task<IActionResult> GetUserWithRole([FromQuery]UserParams userParams)
        {
            var usersForPagination = await _adminService.GetUsersWithRoles(userParams);


            Response.AddPagination(usersForPagination.CurrentPage, usersForPagination.PageSize,
                usersForPagination.TotalCount, usersForPagination.TotalPages);
            List<UserWithRoles> usersForReturn = usersForPagination;
            return Ok(usersForReturn);
        }

        /// <summary>
        /// Edit Roles of User
        /// </summary>
        /// <param name="userName">Name of User To edit</param>
        /// <param name="roleEditDTO">List Of Rooles too edit</param>
        /// <returns>
        ///     Ok with roles - if success 
        ///     BadRequest - if Fail tto Change Roles
        /// </returns>
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

        /// <summary>
        /// Approve Photo
        /// </summary>
        /// <param name="photoId">Id of photo to approve</param>
        /// <returns> 
        ///     Ok with photo, wich was approved
        ///     BadBadRequest if Photo doesn't exsist
        /// </returns>
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

        /// <summary>
        /// Delete Photo
        /// </summary>
        /// <param name="photoId">Id of photo to delete</param>
        /// <returns>
        ///     Ok - if phto is deleted
        ///     BadRequest - if Photo doesn't exsist
        /// </returns>
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

        /// <summary>
        /// api/admin/photos/moderation
        /// Get All photos, with is not approved 
        /// </summary>
        /// <returns>Unmodarate Photos</returns>
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