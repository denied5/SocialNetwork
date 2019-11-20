using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BIL.DTO;
using BIL.Services.Interrfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("usersWithRoles")]
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

        //[Authorize(Roles = "Moderator")]
        //[HttpGet("photosForModeration")]
        //public Task<IActionResult> GetPhotosForModerator()
        //{
        //    return Ok("admins see this");
        //}
    }
}