using BIL.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BIL.Services.Interrfaces
{
    public interface IAdminService
    {
        Task<List<UserWithRoles>> GetUsersWithRoles();
        Task<IList<string>> EditRoles(string userName, RoleEditDTO roleEditDTO);
        Task<PhotoForReturnDTO> ApprovePhoto(int photoId);
        Task<IEnumerable<PhotoForReturnDTO>> GetPhotosForModerator();
        Task<bool> DeletePhoto(int photoId);
    }
}
