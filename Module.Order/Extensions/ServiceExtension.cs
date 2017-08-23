using Alza.Core.Module.Abstraction;
using Module.Order.Business;
using Module.Order.Configuration;
using Module.Order.Dal.Context;
using Module.Order.Dal.Repository.Abstraction;
using Module.Order.Dal.Repository.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddModuleOrder(this IServiceCollection services, Action<OrderOptions> setupAction)
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
            services.AddDbContext<OrderDbContext>();


            //REPOSITORY
            services.AddScoped<ICartRepository, CartRepository>();
            
            //services.AddScoped<IMediaRepository, MediaRepository>();
            services.AddTransient<IShippingRepository, ShippingRepository>();
            services.AddTransient<IOrderRepository, Order_prodRepository>();

            //SERVICES - zapouzdreni vsechn repositories pod jeden objekt
            //Tyto services pak budou pouzivat ostatni tridy/objetky
            services.AddScoped<OrderService, OrderService>();
            



            return services;
        }
    }
}
