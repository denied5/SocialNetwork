using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Interfaces
{
    public interface ICommentRepository: IRepository<Comment>
    {
        Task<Comment> GetComment(int id);
    }
}
