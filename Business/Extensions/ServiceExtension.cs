using Alza.Core.Module.Abstraction;
using Module.Business.Business;
using Module.Business.Configuration;
using Module.Business.Dal.Context;
using Module.Business.Dal.Repository.Abstraction;
using Module.Business.Dal.Repository.Implementation;
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
        public static IServiceCollection AddModuleBusiness(this IServiceCollection services, Action<BusinessOptions> setupAction)
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
            services.AddDbContext<BusinessDbContext>();


            //REPOSITORY
            services.AddScoped<ICart_prRepository, Cart_prRepository>();
            services.AddTransient<IOrder_prodRepository, Module.Business.Dal.Repository.Implementation.Order_prodRepository>();
            services.AddScoped<IComment_LikeRepository, Comment_LikeRepository>();
            services.AddScoped<IProduct_LikeRepository, Product_LikeRepository>();

            //services.AddScoped<IMediaRepository, MediaRepository>();


            //SERVICES - zapouzdreni vsechn repositories pod jeden objekt
            //Tyto services pak budou pouzivat ostatni tridy/objetky
            services.AddScoped<BusinessService, BusinessService>();
            //services.AddScoped<TemplateService, TemplateService>();
            



            return services;
        }
    }
}
