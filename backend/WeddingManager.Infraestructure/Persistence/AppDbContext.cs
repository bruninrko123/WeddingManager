using Microsoft.EntityFrameworkCore;
using WeddingManager.Domain.Entities;

namespace WeddingManager.Infraestructure.Persistence;



    public class AppDbContext : DbContext
    {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

        public DbSet<User> Users { get; set; }
        public DbSet<Wedding> Weddings { get; set; }

        public DbSet<WeddingTask> WeddingTasks { get; set; }

        public DbSet<WeddingUser> WeddingUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        modelBuilder.Entity<WeddingTask>()
            .HasOne(t => t.Wedding)
            .WithMany(w => w.WeddingTasks)
            .HasForeignKey(t => t.WeddingId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<WeddingUser>()
            .HasKey(wu => new { wu.UserId, wu.WeddingId });


        modelBuilder.Entity<WeddingUser>()
            .HasOne(wu => wu.User)
            .WithMany(u => u.WeddingUsers)
            .HasForeignKey(wu => wu.UserId);

        modelBuilder.Entity<WeddingUser>()
            .HasOne(wu => wu.Wedding)
            .WithMany(w => w.WeddingUsers)
            .HasForeignKey(wu => wu.WeddingId);

        }
    }

    