using DAL.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace BIL.Helpers
{
    public class Seed
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public Seed(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void SeedRole()
        {
            if (!_roleManager.Roles.Any())
            {
                var roles = new List<Role>
                {
                    new Role {Name = "Admin"},
                    new Role {Name = "Member"},
                    new Role {Name = "Moderator"}
                };

                foreach (var role in roles)
                {
                    _roleManager.CreateAsync(role).Wait();
                }
            }
        }

        public void SeedAdmin()
        {
            if (_userManager.Users.FirstOrDefault(p => p.UserName == "Admin") == null)
            {
                var adminUser = new User
                {
                    UserName = "Admin"
                };

                IdentityResult result = _userManager.CreateAsync(adminUser, "password").Result;

                if (result.Succeeded)
                {
                    var admin = _userManager.FindByNameAsync("Admin").Result;
                    _userManager.AddToRolesAsync(admin, new[] { "Admin", "Moderator" }).Wait();
                }
            }
        }

        public void SeedModerator()
        {
            if (_userManager.Users.FirstOrDefault(p => p.UserName == "Moderator") == null)
            {
                var adminUser = new User
                {
                    UserName = "Moderator"
                };

                IdentityResult result = _userManager.CreateAsync(adminUser, "password").Result;

                if (result.Succeeded)
                {
                    var moderator = _userManager.FindByNameAsync("Moderator").Result;
                    _userManager.AddToRolesAsync(moderator, new[] { "Moderator" }).Wait();
                }
            }
        }
    }
}
