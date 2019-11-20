using System;
using System.Collections.Generic;
using System.Text;

namespace BIL.DTO
{
    public class UserWithRoles
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public List<string> Roles { get; set; }
    }
}
