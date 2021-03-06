﻿using System;
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
using Module.Order.Dal.Context;
using PernicekWeb.Models.OrderViewModels;
using ReflectionIT.Mvc.Paging;

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


            //MODULE - CATALOG
            services.AddModuleCatalog(o => o.connectionString = Configuration.GetSection("ConnectionStrings:AlzaLego.Module.CatalogConnection").Value);

            services.AddScoped<IUserRepository, UserProfileRepository>();
            services.AddAlzaModuleUserProfile(o => o.connectionString = Configuration.GetSection("ConnectionStrings:AlzaLego.Module.CatalogConnection").Value);

            services.AddDbContext<UserDbContext>(options => options.UseSqlServer("ConnectionStrings: AlzaLego.Module.UserProfileConnection"));

            services.AddDbContext<CatalogDbContext>(options => options.UseSqlServer("ConnectionStrings: AlzaLego.Module.UserProfileConnection"));


            //MODULE - Order
            services.AddModuleOrder(o => o.connectionString = Configuration.GetSection("ConnectionStrings:Module.OrderConnection").Value);
            services.AddModuleBusiness(o => o.connectionString = Configuration.GetSection("ConnectionStrings:Module.BusinessConnection").Value);

            services.AddDbContext<OrderDbContext>(options => options.UseSqlServer("ConnectionStrings:Module.OrderConnection"));

            // this is in package Microsoft.AspNetCore.NodeServices 
            //services.AddNodeServices();

            // Add framework services.
            services.AddMvc()
                .AddViewLocalization()
                .AddDataAnnotationsLocalization();

            services.AddPaging();
            // Adds a default in-memory implementation of IDistributedCache.
            services.AddDistributedMemoryCache();

           


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
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
