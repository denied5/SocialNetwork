using System.Threading.Tasks;

namespace BIL.Services.Interrfaces
{
    public interface ILikeService
    {
        Task<bool> SetLike(int likerId, int postId);
    }
}
