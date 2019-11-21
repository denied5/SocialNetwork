using System;
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
    [Route("api/users/{userId}/[controller]")]
    [ApiController]
    [Authorize]
    public class MessagesController : ControllerBase
    {
        private readonly IMessagesService _messagesService;

        public MessagesController(IMessagesService messagesService)
        {
            _messagesService = messagesService;
        }

        [HttpGet("{id}", Name = "GetMessage")]
        public async Task<IActionResult> GetMessage(int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            if (userId == id)
                return BadRequest();

            var messageFromRepo = await _messagesService.GetMessage(id);

            if (messageFromRepo == null)
                return NotFound();

            return Ok(); 
        }

        [HttpPost]
        public async Task<IActionResult> PostMessage(int userId, MessageForCreationDTO messageForCreationDTO)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            if (messageForCreationDTO.RecipientId == userId)
                return BadRequest("You can't do this!");

            var message = await _messagesService.AddMessage(userId, messageForCreationDTO);

            return CreatedAtRoute("GetMessage", new { id = message.Id }, message);
        }

        [HttpGet]
        public async Task<IActionResult> GetMessagesForUser(int userId, [FromQuery]PagedListParams messageParams)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            messageParams.UserId = userId;

            var messagesFromRepo = await _messagesService.GetLastMessagesForUser(messageParams);

            Response.AddPagination(messagesFromRepo.CurrentPage, messagesFromRepo.PageSize,
                messagesFromRepo.TotalCount, messagesFromRepo.TotalPages);
            List<MessageToReturnDTO> messagesToReturn = messagesFromRepo;

            return Ok(messagesToReturn);
        }

        [HttpGet("thread/{recipientId}")]
        public async Task<IActionResult> GetMessageThread(int userId, int recipientId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            if (userId == recipientId)
                return BadRequest();

            var messagesToReturn = await _messagesService.GetMessageThread(userId, recipientId);

            return Ok(messagesToReturn);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage(int id, int userId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            if (userId == id)
                return BadRequest();

            if (await _messagesService.DeleteMessage(id, userId))
            {
                return NoContent();
            }

            throw new Exception("Error deleting");
        }

        [HttpPost("{id}/read")]
        public async Task<IActionResult> MarkMessageAsRead(int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
               return Unauthorized();
            if (userId == id)
                return BadRequest();

            if (await _messagesService.MarkMessageAsRead(userId, id))
            {
                return NoContent();
            }
            return BadRequest();
        }
    }
}