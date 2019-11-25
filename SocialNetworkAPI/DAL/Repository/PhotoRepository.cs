using DAL.Data;
using DAL.Models;
using DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class PhotoRepository : Repository<Photo>, IPhotoRepository
    {
        private readonly DataContext _context;
        public PhotoRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Photo> GetMainPhotoForUser(int userId)
        {
            return await _context.Photos.Where(u => u.UserId == userId).FirstOrDefaultAsync(p => p.IsMain == true);
        }

        public async Task<IEnumerable<Photo>> GetUnapprovedPhotos()
        {
            var query = _context.Photos.AsQueryable();

            query = query.IgnoreQueryFilters();
            var photos = query.Where(p => p.Approved == false);
            return photos;
        }

        public async Task<Photo> GetUnapprovedPhoto(int id)
        {
            var photo = _context.Photos.IgnoreQueryFilters()
                .FirstOrDefault(p => p.Id == id);

            return photo;
        }

        public async Task<Photo> AddPhotoForUser(Photo photo, User user)
        {
            if (!user.Photos.Any(u => u.IsMain))
            {
                photo.IsMain = true;
            }

            user.Photos.Add(photo);

            return photo;
        }
    }
}