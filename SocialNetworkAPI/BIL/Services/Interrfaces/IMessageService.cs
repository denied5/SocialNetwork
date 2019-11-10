using BIL.DTO;
using BIL.Helpers;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BIL.Services.Interrfaces
{
    public interface IMessagesService
    {
        Task<MessageToReturnDTO> GetMessage(int id);
        Task<IEnumerable<MessageToReturnDTO>> GetMessageThread(int userId, int recipientId);
        Task <MessageToReturnDTO> AddMessage(int userId, MessageForCreationDTO messageForCreationDTO);
        Task<PagedList<MessageToReturnDTO>> GetLastMessagesForUser(PagedListParams messageParams);
        Task<bool> DeleteMessage(int id, int userId);
        Task<bool> MarkMessageAsRead(int userId, int id);
    }
}
