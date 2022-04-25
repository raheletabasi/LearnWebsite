using LearnWebsite.Data.Entities.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWebsite.Data.Contexts
{
    public class LearnWebsiteContext : DbContext
    {
        public LearnWebsiteContext(DbContextOptions<LearnWebsiteContext> options):base(options)
        {

        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>().HasKey(e => new { e.RoleId, e.UserId });
            base.OnModelCreating(modelBuilder);
        }
    }
}
