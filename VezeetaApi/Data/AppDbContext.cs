using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VezeetaApi.Models;

namespace VezeetaApi.Data
{
    public class AppDbContext:IdentityDbContext<ApplicationUser>
    {
        public AppDbContext() : base() { }
        
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<DiscountCode> DiscountCodes { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            string Admin_Id= "76f86073-b51c-47c4-b7fa-731628055ebb";

            // create an admin user
            ApplicationUser admin = new ApplicationUser
            {
                Id = Admin_Id,
                Email = "admin@gmail.com",
                NormalizedEmail = "admin@gmail.com".ToUpper(),
                EmailConfirmed = true,
                UserName = "admin",
                NormalizedUserName = "admin".ToUpper(),
                LockoutEnabled = true,
                Type = AccountType.Admin
            };
            var hasher = new PasswordHasher<ApplicationUser>();
            admin.PasswordHash = hasher.HashPassword(admin, password: "@Admin123");
            builder.Entity<ApplicationUser>().HasData(admin);
        }
    }
}
