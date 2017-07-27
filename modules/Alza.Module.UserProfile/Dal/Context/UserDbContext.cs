using Alza.Module.UserProfile.Configuration;
using Alza.Module.UserProfile.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Alza.Core.Identity.Dal.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alza.Module.UserProfile.Dal.Context
{
    public class UserDbContext : DbContext
    {
        // private readonly AlzaUserProfileOptions _options2;

        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {

        }




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DEVSQL_STAZ\\DEV_STAZ;Database=group1;Trusted_Connection=True;");
        }

        /***************************************************************/
        /*      ENTITY */
        /***************************************************************/

        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>().ToTable("User");
            builder.Entity<User>().Property(m => m.id_user);
            builder.Entity<User>().Property(m => m.name).IsRequired();
            builder.Entity<User>().Property(m => m.surname).IsRequired();
            //   base.OnModelCreating(builder);
            //   builder.Entity<User>().Property(m => m.DateOfBirth).IsRequired();

        }

        /*public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            return base.SaveChanges();
        }*/

    }
}
