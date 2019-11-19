using API.Helpers;
using BIL.DTO;
using BIL.Helpers;
using BIL.Services.Interrfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        public readonly IConfiguration _configuration;

        public AuthController(IAuthService authService, 
            IUserService userService, IConfiguration configuration)
        {
            _configuration = configuration;
            _userService = userService;
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDTO user)
        {
            var createdUser = await _authService.Register(user);
            if (createdUser is null)
                return BadRequest("Fail to create user");
            return CreatedAtRoute("GetUser",
                new { controller = "Users", id = createdUser.Id}, createdUser);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDTO user)
        {
            //check for valid
            var userFromDb = await _authService.LogIn(user);
            if (userFromDb == null)
                return Unauthorized();

            //generate token
            var userToken = _authService.GenerateToken(userFromDb, 
                _configuration.GetSection("AuthKey:Token").Value);

            //return MainUserDTo
            return Ok(new
            {
                userToken,
                userFromDb
            });
        }
    }
}