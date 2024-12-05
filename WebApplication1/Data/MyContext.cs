using JWTdemo.Models;
using Microsoft.EntityFrameworkCore;

namespace JWTdemo.Data
{
    public class MyContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }


        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                      .HasDefaultValueSql("NEWSEQUENTIALID()");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.RoleId); 
                entity.Property(e => e.RoleId)
                      .HasDefaultValueSql("NEWSEQUENTIALID()");
            });

            var adminRoleId = Guid.NewGuid();
            var userRoleId = Guid.NewGuid();

            // Default roles data
            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = adminRoleId, RoleName = "Admin" },
                new Role { RoleId = userRoleId, RoleName = "User" }
            );

            // Seeding Users
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = Guid.NewGuid(),
                    UserName = "admin",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"), // Replace with a securely hashed password
                    RoleId = adminRoleId
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    UserName = "user",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("abcdef"), // Replace with a securely hashed password
                    RoleId = userRoleId
                }
            );

        }

    }
}
