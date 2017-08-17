using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Module.Order.Configuration;
using Microsoft.Extensions.Options;
using Module.Order.Dal.Entities;

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

        public DbSet<Address> Address { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<Cart_pr> Cart_pr { get; set; }
        public DbSet<Cart_st> Cart_st { get; set; }
        public DbSet<Order.Dal.Entities.Order> Order { get; set; }
        public DbSet<Order_prod> Order_prod { get; set; }
        public DbSet<Status> Status { get; set; }





        /***************************************************************/
        /*      VAZBY ENTIT */
        /***************************************************************/


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }








        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            return base.SaveChanges();
        }
    }
}
