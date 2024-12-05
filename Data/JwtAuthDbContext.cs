using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jwtAuth2._0.Models;
using Microsoft.EntityFrameworkCore;

namespace jwtAuth2._0.Data
{
    public class JwtAuthDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        public JwtAuthDbContext(DbContextOptions<JwtAuthDbContext> options) : base (options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        modelBuilder.Entity<UserRole>()
            .HasKey(ur => new { ur.UserId, ur.RoleId });

        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.UserId);

        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.RoleId);
        }
    }
}