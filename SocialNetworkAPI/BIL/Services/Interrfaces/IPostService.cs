using BIL.DTO;
using BIL.Helpers;
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
        Task<PagedList<PostForReturnDTO>> GetFeed(PagedListParams param);
        IEnumerable<PostForReturnDTO> GetPosts(int userId);
    }
}
