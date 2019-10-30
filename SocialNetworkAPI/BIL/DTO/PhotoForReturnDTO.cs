using System;

namespace BIL.DTO
{
    public class PhotoForReturnDTO
    {
        public int Id { get; set; }
        public string URL { get; set; }
        public string Dscription { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }
    }
}