using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data
{
    public class DataContext: IdentityDbContext<User, Role, int, 
        IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, 
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {

        public DataContext(DbContextOptions<DataContext> options): base(options){}

        public DbSet<Message> Messages {get; set;}
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<Post>  Posts { get; set; }
        public DbSet<Like> Like { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserRole>(userRole => {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            builder.Entity<Message>()
                .HasOne(u => u.Sender)
                .WithMany(m => m.MessagesSent)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Message>()
                .HasOne(u => u.Recipient)
                .WithMany(m => m.MessagesReceived)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Friendship>()
                .HasKey(k => new { k.RecipientId, k.SenderId });

            builder.Entity<Friendship>()
                .HasOne(u => u.Sender)
                .WithMany(f => f.FriendshipsReceived)
                .HasForeignKey(u => u.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Friendship>()
                .HasOne(u => u.Recipient)
                .WithMany(f => f.FriendshipsSent)
                .HasForeignKey(u => u.RecipientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Like>()
                .HasKey(k => new { k.PostId, k.UserId });

            builder.Entity<Like>()
                .HasOne(u => u.User)
                .WithMany(l => l.Likes)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Like>()
                .HasOne(p => p.Post)
                .WithMany(l => l.Likes)
                .HasForeignKey(p => p.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Photo>().HasQueryFilter(p => p.Approved);
        }  
    }
}