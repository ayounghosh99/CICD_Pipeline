using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CICD_PIPELINE_AUTOMATION.Contracts;
using CICD_PIPELINE_AUTOMATION.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CICD_PIPELINE_AUTOMATION
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
            services.AddScoped<IUserContract, UserService>();
            services.AddScoped<IItemContract, ItemService>();
            services.AddScoped<IDatabaseContract, DatabaseService>();
            services.AddControllersWithViews();
            services.AddSingleton<IConInfo, Models.ConInfo>();
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                //pattern: "{controller=Home}/{action=Index_delete_}");
                    pattern: "{controller=Home}/{action=Login}/{id?}");
            });
        }
    }
}
