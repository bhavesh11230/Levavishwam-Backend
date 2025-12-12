
using Levavishwam_Backend.Models;
using Microsoft.EntityFrameworkCore;

using System;

namespace Levavishwam_Backend.Data
{
    public class AppDbContext : DbContext
    {
        internal object UserProfiles;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // TABLES
        public DbSet<User> Users { get; set; }
        public DbSet<Menu> Menus { get; set; }

        
        // HOME MODULE 
        public DbSet<CarouselImage> Carousel { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Download> Downloads { get; set; }
        public DbSet<CommitteeMember> Committee { get; set; }
        public DbSet<Information> Information { get; set; }


        // Profile MODULE 
        public DbSet<UserProfile> UserProfile { get; set; }

        // MODEL CONFIG 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");

                entity.HasKey(u => u.UserId);

                entity.Property(u => u.Email)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasIndex(u => u.Email)
                    .IsUnique(); 

                entity.Property(u => u.PasswordHash)
                    .IsRequired();

                entity.Property(u => u.Role)
                    .HasDefaultValue("User")
                    .HasMaxLength(50);

                entity.Property(u => u.Status)
                    .HasDefaultValue("Pending")  
                    .HasMaxLength(50);

                entity.Property(u => u.CreatedAt)
                    .HasDefaultValueSql("GETUTCDATE()");

                entity.Property(u => u.UpdatedAt)
                    .IsRequired(false);
            });

           
            modelBuilder.Entity<Menu>(entity =>
            {
                entity.ToTable("Menus");

                entity.HasKey(m => m.Id);

                entity.Property(m => m.Title)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(m => m.Path)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(m => m.OrderNo)
                    .IsRequired();

                entity.Property(m => m.IsActive)
                    .HasDefaultValue(true);

                entity.Property(m => m.IsAdminOnly)
                    .HasDefaultValue(false);
            });

            // USERPROFILE TABLE CONFIGURATION
            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.ToTable("UserProfile");

                entity.HasKey(p => p.UserProfileId);

                entity.Property(p => p.UserId).IsRequired();

                entity.Property(p => p.Mobile).HasMaxLength(20);
                entity.Property(p => p.Address).HasMaxLength(500);
                entity.Property(p => p.DOB).HasColumnType("date");
                entity.Property(p => p.Gender).HasMaxLength(20);
                entity.Property(p => p.CommunityInfo).HasColumnType("nvarchar(max)");
                entity.Property(p => p.ProfilePhotoPath).HasMaxLength(500);
            });

            modelBuilder.Entity<User>()
                .HasOne(u => u.UserProfile)
                .WithOne(p => p.User)
                .HasForeignKey<UserProfile>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}