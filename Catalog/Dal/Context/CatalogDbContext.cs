using Catalog.Configuration;
using Catalog.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Catalog.Dal.Context
{
    public class CatalogDbContext : DbContext
    {

        /***************************************************************/
        /*      NASTAVENI DB - KONFIGURACE, OPTIONS */
        /***************************************************************/

        private readonly CatalogOptions _options2;

        public CatalogDbContext(DbContextOptions<CatalogDbContext> options, IOptions<CatalogOptions> options2) :base(options)
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
        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Colour> Colour { get; set; }
        public DbSet<Size> Size { get; set; }






        /***************************************************************/
        /*      VAZBY ENTIT */
        /***************************************************************/
        protected override void OnModelCreating(ModelBuilder builder)
        {
         //   builder.Entity<Colour>().ToTable("Colour");

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
