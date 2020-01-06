using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace BIL.DTO
{
    public class PhotoForCreationDTO
    {
        public string URL { get; set; }
        [Required]
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