using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RazorApp.TH.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Services;

namespace RazorAppTH
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
            //services.AddSession((e) =>
            //{
            //    e.Cookie = new Microsoft.AspNetCore.Http.CookieBuilder
            //    {
            //        HttpOnly = true,
            //        IsEssential = true,
            //        Path = "./",
            //        SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict
            //    };
            //    e.IdleTimeout = System.TimeSpan.FromSeconds(1000 * 60);
            //    e.IOTimeout = TimeSpan.FromSeconds(1000 * 60);

            //});

            services.AddHttpContextAccessor();
            services.AddTransient<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IRazorRenderService, RazorRenderService>();     
            services.AddRazorPages();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);//We set Time here 
                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.None;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.IsEssential = true;
            });            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();
            app.UseMiddleware<RequestResponseMiddleware>();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
           
          

        }
    }


    //services.AddScoped<ISessionHelper, clsSessionHelper>();
    
}
