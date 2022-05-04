﻿using LearnWebsite.Data.Entities.User;
using LearnWebsite.Data.Entities.CashWallet;
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

        #region user
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        #endregion

        #region cashWallet
        public DbSet<CashType> CashTypes { get; set; }
        public DbSet<CashWallet> CashWallets { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>().HasKey(e => new { e.RoleId, e.UserId });
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CashType>().HasData(
                new CashType { CashTypeId = 1, TypeTitle = "واریز" },
                new CashType { CashTypeId = 2, TypeTitle = "برداشت"});
        }
    }
}
