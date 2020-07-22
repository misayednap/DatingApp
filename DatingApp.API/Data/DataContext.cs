using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;
namespace DatingApp.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }
        public DbSet<Value> Values { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Like> Likes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Setting up many to many relationship 

            // primary key contains both Liker and Likee ids
            // this will create two columns LikerId and LikeeId
            builder.Entity<Like>()
                .HasKey(k => new {k.LikerId, k.LikeeId});
            
            // relationship setup that says one Likee can have multiple Likers
            // we have a forign key back to the Users table
            // wed don't want to cascade delete - so deleting a like will not delete the user
            builder.Entity<Like>()
                .HasOne(u => u.Likee)
                .WithMany(u => u.Likers)
                .HasForeignKey(u => u.LikeeId)
                .OnDelete(DeleteBehavior.Restrict);

            // relationship that says one Liker has many Likeees
            builder.Entity<Like>()
                .HasOne(u => u.Liker)
                .WithMany(u => u.Likees)
                .HasForeignKey(u => u.LikerId)
                .OnDelete(DeleteBehavior.Restrict);              
        }
    }
}