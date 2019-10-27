using System;

namespace BIL.DTO
{
    public class PhotoForDetailedDTO
    {
        public int Id { get; set; }
        public string URL { get; set; }
        public string Dscription { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsMain { get; set; }
    }
}