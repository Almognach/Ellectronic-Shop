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
using Electro_Shop.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace Electro_Shop
{
    public class Startup
    {

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("RequireAdministratorRole",
            //        policy => policy.RequireUserName("Admin@ElectroShop.co.il"));
            //});
            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = "739483181872-9pqf68eioph3gk2khvghsbcrc9na1qd7.apps.googleusercontent.com";
                    options.ClientSecret = "eEwSlQuXGP3THT0feuuDSOav";

                    //options.ClientId = Configuration["App:GoogleClientId"];
                    //options.ClientSecret = Configuration["App:GoogleClientSecret"];
                })

                //.AddFacebook(option =>
                //{
                //    option.AppId = Configuration["App:FacebookClientId"];
                //    option.ClientSecret = Configuration["App:FacebookClientSecret"];
                //})

            //.AddFacebook(option =>
            //{
            //    option.AppId = Configuration["App:FacebookClientId"];
            //    option.ClientSecret = Configuration["App:FacebookClientSecret"];
            //});

                ;

            services.AddMvc();
            

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
            services.AddDbContext<ContactUsContext>(options =>
                  options.UseSqlServer(Configuration.GetConnectionString("ContactUsSubmitContext")));
            services.AddDbContext<ShoppingCartContext>(options =>
             options.UseSqlServer(Configuration.GetConnectionString("ShoppingCartContext")));
            services.AddDbContext<OrdersContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("OrdersContext")));
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
