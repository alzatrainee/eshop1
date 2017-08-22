using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Module.Order.Configuration;
using Microsoft.Extensions.Options;
using Module.Order.Dal.Entities;
using Catalog.Dal.Entities;
using Alza.Module.UserProfile.Dal.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Module.Order.Dal.Context
{
    public class OrderDbContext : DbContext
    {
        /***************************************************************/
        /*      NASTAVENI DB - KONFIGURACE, OPTIONS */
        /***************************************************************/

        private readonly OrderOptions _options2;

        public OrderDbContext(DbContextOptions<OrderDbContext> options, IOptions<OrderOptions> options2) : base(options)
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

        //public DbSet<Address> Address { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<Cart_pr> Cart_pr { get; set; }
        //public DbSet<Cart_st> Cart_st { get; set; }
        //public DbSet<Order.Dal.Entities.Order> Order { get; set; }
        //public DbSet<Order_prod> Order_prod { get; set; }
        //public DbSet<Status> Status { get; set; }





        /***************************************************************/
        /*      VAZBY ENTIT */
        /***************************************************************/


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Cart>().ToTable("Cart");
            builder.Entity<Cart>().Property(k => k.id_user);
            //builder.Entity<Cart>().Property(k => k.id_car);
            builder.Entity<Cart>().Property(k => k.id_st);
            builder.Entity<Cart>().HasKey(k => k.id_car);

            builder.Entity<Cart_pr>().HasKey(k => new { k.id_car, k.id_pr });
            builder.Entity<Cart_st>().HasKey(k => k.id_st);
            builder.Entity<User>().HasKey(k => k.id_user);


            //builder.Entity<Cart>().Property(c => c.id_user);

            base.OnModelCreating(builder);
        }








        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            return base.SaveChanges();
        }
    }
}
