using Levavishwam_Backend.Models;
using Microsoft.EntityFrameworkCore;
<<<<<<< Updated upstream
<<<<<<< Updated upstream
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Levavishwam_Backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

       
        public DbSet<User> Users { get; set; }
=======
using System;
using System.Collections.Generic;

namespace Levavishwam_Backend.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(Microsoft.EntityFrameworkCore.DbContextOptions<AppDBContext> options) : base(options) { }
>>>>>>> Stashed changes
=======
using System;
using System.Collections.Generic;

namespace Levavishwam_Backend.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(Microsoft.EntityFrameworkCore.DbContextOptions<AppDBContext> options) : base(options) { }
>>>>>>> Stashed changes

        public DbSet<CarouselImage> Carousel { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Download> Downloads { get; set; }
        public DbSet<CommitteeMember> Committee { get; set; }
        public DbSet<Information> Information { get; set; }
<<<<<<< Updated upstream
<<<<<<< Updated upstream

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // USER TABLE CONFIGURATION
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

            
        }
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
    }
}
