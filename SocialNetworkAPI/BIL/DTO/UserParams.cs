using System;
using System.Collections.Generic;
using System.Text;

namespace BIL.DTO
{
    public class UserParams
    {
        private const int MaxPageSize = 50;
        public int CurrentPage { get; set; } = 1;
        private int pageSize = 10;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }

        public int UserId { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int MaxAge { get; set; } = 99;
        public int MinAge { get; set; } = 14;
    }
}
