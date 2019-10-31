﻿
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BIL.DTO;
using BIL.Extensions;
using BIL.Services.Interrfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
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

        [HttpGet]
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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