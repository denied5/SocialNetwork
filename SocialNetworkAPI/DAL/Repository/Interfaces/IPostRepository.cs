using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Interfaces
{
    public interface IPostRepository: IRepository<Post>
    {
        Task<Post> GetPost(int id);
        IEnumerable<Post> GetPosts(int userId);
    }
}
