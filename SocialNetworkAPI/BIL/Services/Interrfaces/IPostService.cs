using BIL.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BIL.Services.Interrfaces
{
    public interface IPostService
    {
        Task<PostForReturnDTO> GetPost(int id);
        Task<PostForReturnDTO> CreatePost(PostForCreatinDTO post);
        Task<IEnumerable<PostForReturnDTO>> GetFeed(int userId);
        Task<IEnumerable<PostForReturnDTO>> GetPosts(int userId);
    }
}
