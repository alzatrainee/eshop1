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
        
        //public DbSet<Cart_st> Cart_st { get; set; }
        public DbSet<NewOrder> NewOrder { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Shipping> Shipping { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Method> Method { get; set; }
        public DbSet<Payment> Payment { get; set; }





        /***************************************************************/
        /*      VAZBY ENTIT */
        /***************************************************************/


        protected override void OnModelCreating(ModelBuilder builder)
        {
            /* Cart */
            builder.Entity<Cart>().ToTable("Cart");
            builder.Entity<Cart>().Property(k => k.id_user);
            //builder.Entity<Cart>().Property(k => k.id_car);
            builder.Entity<Cart>().Property(k => k.id_st);
            builder.Entity<Cart>().HasKey(k => k.id_car);


            /* NewOrder */
            builder.Entity<NewOrder>().ToTable("NewOrder");
            //builder.Entity<NewOrder>().Property(p => p.id_us);
            builder.Entity<NewOrder>().Property(p => p.id_st);
            builder.Entity<NewOrder>().Property(p => p.id_ad);
            builder.Entity<NewOrder>().Property(p => p.id_sh);
            builder.Entity<NewOrder>().Property(p => p.id_pay);
            //builder.Entity<NewOrder>().HasKey(p => new { p.id_st, p.id_ad, p.id_fad, p.id_pay, p.id_sh });


            /* Payment */
            builder.Entity<Payment>().ToTable("Payment");
            builder.Entity<Payment>().Property(p => p.id_meth);
            builder.Entity<Payment>().Property(p => p.id_st);
            builder.Entity<Payment>().Property(p => p.price);
            builder.Entity<Payment>().HasKey(p => new { p.id_meth, p.id_st });


            /* Address */
            builder.Entity<Address>().ToTable("Address");
            builder.Entity<Address>().Property(p => p.street);
            builder.Entity<Address>().Property(p => p.city);
            builder.Entity<Address>().Property(p => p.house_number);
            builder.Entity<Address>().Property(p => p.post_code);


            /* User */
            builder.Entity<User>().HasKey(k => k.id_user);
            

            base.OnModelCreating(builder);
        }
        
        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            return base.SaveChanges();
        }
    }
}
