﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public DateTime DateOfCreation { get; set; }
    }
}