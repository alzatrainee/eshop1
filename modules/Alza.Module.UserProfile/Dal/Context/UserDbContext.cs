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
        private readonly AlzaUserProfileOptions _options2;

        public UserDbContext(DbContextOptions<UserDbContext> options, IOptions<AlzaUserProfileOptions> options2) : base(options)
        {
            if (options2 == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            _options2 = options2.Value;
        }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_options2.connectionString);
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
            builder.Entity<User>().Property(m => m.mobile);
            //   base.OnModelCreating(builder);
            //   builder.Entity<User>().Property(m => m.DateOfBirth).IsRequired();

        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            return base.SaveChanges();
        }

    }
}
