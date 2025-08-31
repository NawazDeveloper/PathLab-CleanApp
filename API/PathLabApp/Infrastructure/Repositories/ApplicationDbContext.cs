using Microsoft.EntityFrameworkCore;
using pathLab.Domain.Entities;

namespace pathLab.Infrastructure.Repositories
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Doctor> Doctors => Set<Doctor>();
        public DbSet<Address> Addresses => Set<Address>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<UserRole> UserRoles => Set<UserRole>();
        public DbSet<Gender> Genders => Set<Gender>();
        public DbSet<Designation> Designations => Set<Designation>();
        public DbSet<CbcTest> CbcTest => Set<CbcTest>();
        protected override void OnModelCreating(ModelBuilder model)
        {
            base.OnModelCreating(model);

            // Unique Email
            model.Entity<User>().HasIndex(u => u.Email).IsUnique();

            // User : Doctor = 1:1
            model.Entity<User>()
                 .HasOne(u => u.Doctor)
                 .WithOne(d => d.User)
                 .HasForeignKey<Doctor>(d => d.UserId)
                 .OnDelete(DeleteBehavior.Cascade);

            // User : Address = 1:N
            model.Entity<User>()
                 .HasMany(u => u.Addresses)
                 .WithOne(a => a.User)
                 .HasForeignKey(a => a.UserId)
                 .OnDelete(DeleteBehavior.Cascade);

            // UserRole composite key
            model.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId });

            model.Entity<UserRole>()
                 .HasOne(ur => ur.User)
                 .WithMany(u => u.UserRoles)
                 .HasForeignKey(ur => ur.UserId);

            model.Entity<UserRole>()
                 .HasOne(ur => ur.Role)
                 .WithMany(r => r.UserRoles)
                 .HasForeignKey(ur => ur.RoleId);


        }
    }
}
