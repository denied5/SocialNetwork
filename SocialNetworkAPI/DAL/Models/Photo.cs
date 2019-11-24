using System;

namespace DAL.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string URL { get; set; }
        public string Dscription { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsMain { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public string PublicId { get; set; }
        public bool Approved { get; set; }
    }
}