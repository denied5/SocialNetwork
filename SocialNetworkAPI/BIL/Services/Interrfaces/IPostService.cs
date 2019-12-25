using BIL.DTO;
using BIL.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BIL.Services.Interrfaces
{
    public interface IPostService
    {
        Task<PostForReturnDTO> GetPost(int id);
        Task<PostForReturnDTO> CreatePost(PostForCreatinDTO post);
        Task<PagedList<PostForReturnDTO>> GetFeed(PagedListParams param);
        Task<IEnumerable<PostForReturnDTO>> GetPosts(int userId);
        Task<bool> DeletePost(int id);
    }
}
