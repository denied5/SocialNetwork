using AutoMapper;
using BIL.DTO;
using BIL.Helpers;
using BIL.Services.Interrfaces;
using DAL.Models;
using DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIL.Services
{
    class MessagesService: IMessagesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MessagesService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<MessageToReturnDTO> AddMessage(int userId, MessageForCreationDTO messageForCreationDTO)
        {
            var recipient = await _unitOfWork.UserRepository.GetUser(messageForCreationDTO.RecipientId);
            var sernder = await _unitOfWork.UserRepository.GetUser(userId);
            messageForCreationDTO.SenderId = userId;

            if (recipient == null)
                throw new Exception("Recipient don't exsist");

            var message = _mapper.Map<Message>(messageForCreationDTO);
            _unitOfWork.MessageRepository.Add(message);

            if (await _unitOfWork.SaveChanges())
            {
                return _mapper.Map<MessageToReturnDTO>(message);
            }
            throw new Exception("Message faild to save");
        }

        public async Task<MessageToReturnDTO> GetMessage(int id)
        {
            var message = await _unitOfWork.MessageRepository.GetById(id);
            var messageToReturn = _mapper.Map<MessageToReturnDTO>(message);
            return messageToReturn;
        }

        public async Task<PagedList<MessageToReturnDTO>> GetLastMessagesForUser(MessageParams messageParams)
        {
            var messages = await _unitOfWork.MessageRepository.GetMessages(messageParams.UserId);

            messages = messages.OrderByDescending(m => m.MessageSent);
            var messagesToReturn = messages.Distinct(new MessagesComparer());
            var messaggesToPaginate = _mapper.Map<IEnumerable<MessageToReturnDTO>>(messagesToReturn);
            
            return PagedList<MessageToReturnDTO>.Create(messaggesToPaginate,
                messageParams.PageNumber, messageParams.PageSize);
        }

        public async Task<IEnumerable<MessageToReturnDTO>> GetMessageThread(int userId, int recipientId)
        {
            var messages = await _unitOfWork.MessageRepository.GetMessages(userId);

            messages =  messages.Where(u =>
                                    (u.RecipientId == userId && u.SenderId == recipientId)
                                    || (u.RecipientId == recipientId && u.SenderId == userId))
                                    .OrderByDescending(m => m.MessageSent);
            var messagesToReturn = _mapper.Map<IEnumerable<MessageToReturnDTO>>(messages);

            foreach (var message in messagesToReturn)
            {
                if (message.IsRead == false && message.RecipientId == userId)
                {
                    await MarkMessageAsRead(userId, message.Id);
                }
            }
            return messagesToReturn;
        }

        public async Task<bool> DeleteMessage(int id, int userId)
        {
            var messageFromRepo = await _unitOfWork.MessageRepository.GetById(id);
            if (messageFromRepo.SenderId == userId)
                messageFromRepo.SenderDeleted = true;

            if (messageFromRepo.RecipientId == userId)
                messageFromRepo.RecipientDeleted = true;

            if (messageFromRepo.RecipientDeleted == true && messageFromRepo.SenderDeleted == true)
                _unitOfWork.MessageRepository.Remove(messageFromRepo);

            if (await _unitOfWork.SaveChanges())
                return true;
            return false;
        }

        public async Task<bool> MarkMessageAsRead(int userId, int id)
        {
            var messageFromRepo = await _unitOfWork.MessageRepository.GetById(id);

            if (messageFromRepo.RecipientId != userId)
                throw new Exception("You can't mark not your messages as Read");

            messageFromRepo.IsRead = true;
            messageFromRepo.DateRead = DateTime.Now;

            if (await _unitOfWork.SaveChanges())
            {
                return true;
            }
            return false;
        }
    }
}
