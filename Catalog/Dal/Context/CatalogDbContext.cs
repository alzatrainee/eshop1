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

        public CatalogDbContext(DbContextOptions<CatalogDbContext> options, IOptions<CatalogOptions> options2) : base(options)
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
        public DbSet<Prod_col> prod_col { get; set; }
        public DbSet<Prod_si> Prod_si { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<Firm> Firm { get; set; }
        public DbSet<Cat_sub> Cat_sub { get; set; }
        public DbSet<Product_cat> Product_cat { get; set; }
        //public DbSet<Cat_sub> Cat_sub { get; set; }






        /***************************************************************/
        /*      VAZBY ENTIT */
        /***************************************************************/
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //   builder.Entity<Colour>().ToTable("Colour");
            builder.Entity<Prod_col>().HasKey(c => new { c.id_pr, c.rgb });
            builder.Entity<Category>().HasKey(c => c.id_cat);
            builder.Entity<Product>().HasKey(c => c.id_pr);
            builder.Entity<Size>().HasKey(c => c.id_si);
            builder.Entity<Prod_si>().HasKey(c => new { c.id_pr, c.id_si }); // builder pro tabulku Entity.Prod_si
            builder.Entity<Image>().HasKey(c => c.id_im);
            builder.Entity<Firm>().HasKey(c => c.id_fir);
            builder.Entity<Category>().HasKey(c => c.id_cat);
            builder.Entity<Cat_sub>().HasKey(c => c.id_cs);
            builder.Entity<Product_cat>().HasKey(c => new { c.id_cs, c.id_pr });

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
