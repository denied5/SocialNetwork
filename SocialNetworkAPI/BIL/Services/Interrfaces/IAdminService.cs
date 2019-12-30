using BIL.DTO;
using BIL.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BIL.Services.Interrfaces
{
    public interface IAdminService
    {
        Task<PagedList<UserWithRoles>> GetUsersWithRoles(UserParams userParams);
        Task<IList<string>> EditRoles(string userName, RoleEditDTO roleEditDTO);
        Task<PhotoForReturnDTO> ApprovePhoto(int photoId);
        Task<IEnumerable<PhotoForReturnDTO>> GetPhotosForModerator();
        Task<bool> DeletePhoto(int photoId);
    }
}
