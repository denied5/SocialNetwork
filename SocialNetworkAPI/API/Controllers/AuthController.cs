using BIL.DTO;
using BIL.Services.Interrfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService,
            IConfiguration configuration, ILogger<AuthController> logger)
        {
            _configuration = configuration;
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDTO user)
        {
            if (user == null)
                return BadRequest();
            
            _logger.LogInformation("Logon by user:{0}", user.Username);
            var createdUser = await _authService.Register(user);
            if (createdUser is null)
            {
                _logger.LogWarning("Fail to create user");
                return BadRequest("Fail to create user");
            }
            _logger.LogInformation("Success to LogOn User: {}", user.Username);
            return CreatedAtRoute("GetUser",
                new { controller = "Users", id = createdUser.Id }, createdUser);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDTO user)
        {
            _logger.LogInformation("Login by user:{0}", user.Username);
            //check for valid
            var userFromDb = await _authService.LogIn(user);
            if (userFromDb == null)
            {
                _logger.LogInformation("Unauthorize for User:{0}", user.Username);
                return Unauthorized();
            }

            var key = _configuration.GetSection("AuthKey:Token").Value;
            if (key == null)
            {
                _logger.LogError("AuthKey Is Invalid");
                throw new Exception("AuthKey Is Invalid");
            }

            //generate token
            var userToken = await _authService.GenerateToken(userFromDb, key);

            _logger.LogInformation("Success to LogIn User: {}", user.Username);
            //return MainUserDTo
            return Ok(new
            {
                userToken,
                userFromDb
            });
        }
    }
}