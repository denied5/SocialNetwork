﻿using System.Collections.Generic;

namespace BIL.DTO
{
    public class UserWithRoles
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public List<string> Roles { get; set; }
    }
}
