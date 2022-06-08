using LearnWebsite.Data.Entities.User;
using LearnWebsite.Data.Entities.CashWallet;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnWebsite.Data.Entities.Permission;
using LearnWebsite.Data.Entities.Course;

namespace LearnWebsite.Data.Contexts
{
    public class LearnWebsiteContext : DbContext
    {
        public LearnWebsiteContext(DbContextOptions<LearnWebsiteContext> options):base(options)
        {

        }

        #region user
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        #endregion

        #region cashWallet
        public DbSet<CashType> CashTypes { get; set; }
        public DbSet<CashWallet> CashWallets { get; set; }
        #endregion

        #region Permission
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        #endregion

        #region Course
        public DbSet<CourseGroup> CourseGroups { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDelete);
            modelBuilder.Entity<Role>().HasQueryFilter(r => !r.IsDelete);
            modelBuilder.Entity<CourseGroup>().HasQueryFilter(cg => !cg.IsDelete);

            modelBuilder.Entity<UserRole>().HasKey(e => new { e.RoleId, e.UserId });
            base.OnModelCreating(modelBuilder);

        }
    }
}
