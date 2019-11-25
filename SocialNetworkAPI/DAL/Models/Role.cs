using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace DAL.Models
{
    public class Role : IdentityRole<int>
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
