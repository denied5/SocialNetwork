using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class User : IdentityUser<int>
    {
        [Required]
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        [Required]
        [MaxLength(20)]
        public string KnownAs { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        [MaxLength(2000)]
        public string Introduction { get; set; }
        [MaxLength(2000)]
        public string LookingFor { get; set; }
        [MaxLength(2000)]
        public string Interests { get; set; }
        [MaxLength(50)]
        public string City { get; set; }
        [MaxLength(50)]
        public string Country { get; set; }
        public string FairbaseToken { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Like> Likes { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Message> MessagesSent { get; set; }
        public ICollection<Message> MessagesReceived { get; set; }
        public ICollection<Friendship> FriendshipsSent { get; set; }
        public ICollection<Friendship> FriendshipsReceived { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}