using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Alza.Module.UserProfile.Dal.Context;
using Pernicek.Models;
using Alza.Module.UserProfile.Dal.Repository;
using Alza.Module.UserProfile.Dal.Repository.Abstraction;
using Catalog.Dal.Context;

namespace Pernicek
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see https://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add functionality to inject IOptions<T>
            services.AddOptions();


            //ALZA CORE - IDENTITY
            services.AddAlzaCoreIdentity(o => o.connectionString = Configuration.GetSection("ConnectionStrings:AlzaLego.Core.IdentityConnection").Value, Configuration);


            //ALZA MODULES - CATALOG
            services.AddModuleCatalog(o => o.connectionString = Configuration.GetSection("ConnectionStrings:AlzaLego.Module.CatalogConnection").Value);

            services.AddScoped<IUserRepository, UserProfileRepository>();
            services.AddAlzaModuleUserProfile(o => o.connectionString = Configuration.GetSection("ConnectionStrings:AlzaLego.Module.CatalogConnection").Value);

            services.AddDbContext<UserDbContext>(options => options.UseSqlServer("ConnectionStrings: AlzaLego.Module.UserProfileConnection"));

            services.AddDbContext<CatalogDbContext>(options => options.UseSqlServer("ConnectionStrings: AlzaLego.Module.UserProfileConnection"));


            // Add framework services.
            services.AddMvc()
                .AddViewLocalization()
                .AddDataAnnotationsLocalization();

            // Adds a default in-memory implementation of IDistributedCache.
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.CookieHttpOnly = true;
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseSession();
            loggerFactory.AddConsole();

            //Environment differents
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }




            app.UseStaticFiles();



            //ALZA CORE - IDENTITY
            app.UseAlzaCoreIdentity();





            //Routing
            app.UseMvc(routes =>
            {

                //default route
                routes.MapRoute(
                    "Default",
                    "{controller}/{action}/{id?}",
                    new { controller = "Home", action = "Index", id = "" }
                );

            });
        }
    }
}
