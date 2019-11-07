using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BIL.DTO;
using BIL.Extensions;
using BIL.Helpers;
using BIL.Services.Interrfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ServiceFilter(typeof(UpdateUserActivityFilter))]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery]UserParams userParams)
        {
            var currentId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            userParams.UserId = currentId;
           
            var usersForPagination = await _userService.GetUsers(userParams);
            if (usersForPagination == null)
            {
                return BadRequest("Users don't exsist");
            }

            Response.AddPagination(usersForPagination.CurrentPage, usersForPagination.PageSize,
                usersForPagination.TotalCount, usersForPagination.TotalPages);

            List<UserForListDTO> usersForReturn = usersForPagination;
            return Ok(usersForReturn);
        }

        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            var userToReturn = await _userService.GetUser(id);
            if (userToReturn == null)
            {
                return BadRequest("User don't exsist");
            }
            return Ok(userToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserForUpdateDTO userForUpdate)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))//read value from token
                return Unauthorized();

           if (await _userService.UpdateUser(id, userForUpdate))
           {
               return NoContent();
           }
           return BadRequest("Some problem in Updating User");
        }

        
    }
}