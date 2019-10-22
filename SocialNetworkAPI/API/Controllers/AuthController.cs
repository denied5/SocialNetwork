using AutoMapper;
using BIL.DTO;
using BIL.Services.Interrfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public AuthController(IAuthService authService, IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDTO user)
        {
            if (await _userService.UserExsist(user.Username))
                return BadRequest("Username already exsist");

            var createdUser = await _authService.Register(user);
            if (createdUser is null)
                return BadRequest("Fail to create user");
            return Ok(createdUser);
        }

        [HttpGet("login")]
        public async Task<IActionResult> Login(UserForLoginDTO user)
        {
            //check for valid
            var userFromDb = await _authService.LogIn(user);
            if (userFromDb == null)
                return Unauthorized();

            //generate token
            var userToken = _authService.GenerateToken(userFromDb);

            //return MainUserDTo
            return Ok(new
            {
                userToken,
                userFromDb
            });
        }
    }
}