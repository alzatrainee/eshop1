using Alza.Core.Module.Abstraction;
using Catalog.Business;
//using Catalog.Business;
using Catalog.Configuration;
using Catalog.Dal.Context;
using Catalog.Dal.Entities;
using Catalog.Dal.Repository;
using Catalog.Dal.Repository.Abstraction;
using Catalog.Dal.Repository.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddModuleCatalog(this IServiceCollection services, Action<CatalogOptions> setupAction)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            if (setupAction == null)
            {
                throw new ArgumentNullException(nameof(setupAction));
            }



            //string connectionString = @"Server=DESKTOP-LCV6O88\SQLEXPRESS;Database=AlzaLegoDatabase;User Id=sa;Password=master";
            //services.AddDbContext<EFLocalizationDbContext>(options => options.UseSqlServer(connectionString));




            //registruje nastaveni modulu
            services.Configure(setupAction);

            //connectionString si vezme sam DbContext z IOptions<>
            services.AddDbContext<CatalogDbContext>();


            //REPOSITORY

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IColourRepository, ColourRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ISizeRepository, SizeRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();

            services.AddTransient<Iprod_colRepository, Prod_colRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IColourRepository, ColourRepository>();
            //services.AddScoped<ICategoryRepository, CategoryRepository>();

            //services.AddScoped<IMediaRepository, MediaRepository>();


            //SERVICES - zapouzdreni vsechn repositories pod jeden objekt
            //Tyto services pak budou pouzivat ostatni tridy/objetky
            services.AddScoped<CatalogService, CatalogService>();




            return services;
        }
    }
}
