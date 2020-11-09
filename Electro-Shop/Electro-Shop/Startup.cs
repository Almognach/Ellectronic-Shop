using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Electro_Shop.Data;

namespace Electro_Shop
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = "739483181872-c1halhl6d1gl3424aseh9m0jfjedb3if.apps.googleusercontent.com";
                    options.ClientSecret = "uP8CW3CH5c-xT0TMtZnsH2Z_";
                });

                //.AddFacebook(option =>
                //{
                //    option.AppId = Configuration["App:FacebookClientId"];
                //    option.ClientSecret = Configuration["App:FacebookClientSecret"];
                //});


                

            services.AddRazorPages();
            services.AddControllersWithViews();

            services.AddDbContext<CategoryContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("CategoryContext")));

            services.AddDbContext<ProductContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("ProductContext")));

            services.AddDbContext<SupplierContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("SupplierContext")));
            services.AddDbContext<BranchContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("BranchesContext")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
