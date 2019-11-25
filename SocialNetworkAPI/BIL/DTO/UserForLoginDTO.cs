using System.ComponentModel.DataAnnotations;

namespace BIL.DTO
{
    public class UserForLoginDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "password length from 4 to 8")]
        public string Password { get; set; }
    }
}
