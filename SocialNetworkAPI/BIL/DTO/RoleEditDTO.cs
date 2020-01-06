using System.ComponentModel.DataAnnotations;

namespace BIL.DTO
{
    public class RoleEditDTO
    {
        [Required]
        public string[] RoleNames { get; set; }
    }
}
