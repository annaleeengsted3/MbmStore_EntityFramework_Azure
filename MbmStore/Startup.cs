using System;
using MbmStore.Data;
using MbmStore.Models.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace MbmStore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddControllersWithViews();
            services.AddMemoryCache();
            services.AddSession();

            services.AddDbContext<MbmStoreContext>(options => options.UseSqlServer(Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING")));
            services.AddScoped<IBookRepository, EFBookRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                      name: "areas",
                      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                //category buttons' first match- controller and action defaults match (and are omitted in the url), values for category and page are provided
                endpoints.MapControllerRoute(
                name: null,
                pattern: "Catalogue/{category}/Page{page:int}",
                defaults: new
                {
                    controller = "Catalogue",
                    action = "Index" 
                });


                endpoints.MapControllerRoute(
                name: null,
                pattern: "Page{page:int}",
                defaults: new
                {
                    controller = "Catalogue",
                    action = "Index",
                    productPage = 1
                });

                endpoints.MapControllerRoute(
                                name: null,
                pattern: "Catalogue/{category}",
                defaults: new
                {
                    controller = "Catalogue",
                    action = "Index",
                    productPage = 1
                });


                //if no segments are in the incoming link (just localhost:1234 for example) call Index in CatalogueController
                endpoints.MapControllerRoute(
                                name: null,
                pattern: "",
                defaults: new
                {
                    controller = "Catalogue",
                    action = "Index",
                    productPage = 1
                });

                endpoints.MapControllerRoute(
                name: "pagination",
                pattern: "Catalogue/Page{page}",
                defaults: new { Controller = "Catalogue", action = "Index" });
                
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}