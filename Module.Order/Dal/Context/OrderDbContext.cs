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
