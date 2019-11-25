using DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repository.Interfaces
{
    public interface IPhotoRepository : IRepository<Photo>
    {
        Task<Photo> GetMainPhotoForUser(int userId);
        Task<Photo> AddPhotoForUser(Photo photo, User user);
        Task<IEnumerable<Photo>> GetUnapprovedPhotos();
        Task<Photo> GetUnapprovedPhoto(int id);
    }
}