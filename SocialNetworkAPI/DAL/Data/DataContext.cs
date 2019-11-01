using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options){}
        public DbSet<Message> Messages {get; set;}
        public DbSet<User> Users { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Friendship> Friendships { get; set; }

         protected override void OnModelCreating(ModelBuilder builder)
        {
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
        }  
    }
}