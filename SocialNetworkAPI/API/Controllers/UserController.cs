
using System.Threading.Tasks;
using AutoMapper;
using BIL.DTO;
using BIL.Services.Interrfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(UserForListDTO user)
        {
            if(user == null)
            {
                BadRequest();
            }

            if(await _userService.AddUser(user))
                return Ok(user);
            return BadRequest();
        }
    }
}