using BIL.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BIL.Services.Interrfaces
{
    public interface ICommentService
    {
        Task<CommentDTO> SetComment(CommentToCreateDTO comment);
        Task<bool> DeleteComment(int userId, int commentId);
        Task<CommentDTO> GetComment(int id);
    }
}
