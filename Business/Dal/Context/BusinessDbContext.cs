using Module.Business.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Module.Business.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Module.Business.Dal.Entity;

namespace Module.Business.Dal.Context
{
    public class BusinessDbContext : DbContext
    {

        /***************************************************************/
        /*      NASTAVENI DB - KONFIGURACE, OPTIONS */
        /***************************************************************/

        private readonly BusinessOptions _options2;

        public BusinessDbContext(DbContextOptions<BusinessDbContext> options, IOptions<BusinessOptions> options2) : base(options)
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
        public DbSet<Cart_pr> Cart_pr { get; set; }
        public DbSet<Order_prod> Order_prod { get; set; }







        /***************************************************************/
        /*      VAZBY ENTIT */
        /***************************************************************/
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Cart_pr>().HasKey(c => new { c.id_pr, c.id_car });

            /* Order_prod */
            builder.Entity<Order_prod>().ToTable("Order_prod");
            builder.Entity<Order_prod>().Property(p => p.id_ord);
            builder.Entity<Order_prod>().Property(p => p.id_pr);
            builder.Entity<Order_prod>().Property(p => p.amount);
            builder.Entity<Order_prod>().HasKey(p => new { p.id_ord, p.id_pr });

            base.OnModelCreating(builder);
        }




        /***************************************************************/
        /*    OVERRIDE  METHODS */
        /***************************************************************/
        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            return base.SaveChanges();
        }




    }
}
