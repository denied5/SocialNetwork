using BIL.DTO;
using System.Threading.Tasks;

namespace BIL.Services.Interrfaces
{
    public interface IPhotoService
    {
        Task<PhotoForReturnDTO> GetPhoto(int id);
        Task<bool> SetMainPhoto(int userId, int photoId);
        Task<PhotoForReturnDTO> AddPhotoFromMember(int userId, PhotoForCreationDTO photoForCreationDTO);
        Task<bool> DeletePhoto(int userId, int photoId);
    }
}