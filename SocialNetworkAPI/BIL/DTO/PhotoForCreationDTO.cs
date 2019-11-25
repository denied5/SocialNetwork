using Microsoft.AspNetCore.Http;
using System;

namespace BIL.DTO
{
    public class PhotoForCreationDTO
    {
        public string URL { get; set; }
        public IFormFile File { get; set; }
        public string Dscription { get; set; }
        public DateTime DateAdded { get; set; }
        public string PublicId { get; set; }

        public PhotoForCreationDTO()
        {
            DateAdded = DateTime.Now;
        }
    }
}