using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Lab10Authentication.Models;
using Lab10Authentication.Models.Entities;

namespace Lab10Authentication.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>()
            .HasOne(p => p.Profile)
            .WithOne(u => u.User)
            .HasForeignKey<Profile>(u => u.ApplicationUserId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Profile>()
                .HasAlternateKey(c => c.UserName)
                .HasName("AlternateKey_UserName");

            builder.Entity<Item>()
               .HasAlternateKey(c => c.Name)
               .HasName("AlternateKey_Name");
        }
    }
}
