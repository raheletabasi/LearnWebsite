using LearnWebsite.Data.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnWebsite.Core.Services.Interfaces;
using LearnWebsite.Core.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using LearnWebsite.Core.Utility.Convertor;

namespace LearnWebsite.Web
{
    public class Startup
    {
        readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            #region ConnectionString
            services.AddDbContext<LearnWebsiteContext>(option =>
            {
                option.UseSqlServer(_configuration.GetConnectionString("LearnWebsiteConnectionString"));
            });
            #endregion

            #region IoC
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IViewRenderService, RenderViewToString>();
            #endregion

            #region Athentication
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                option.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(cookie =>
            {
                cookie.LoginPath = "/Login";
                cookie.LogoutPath = "/Logout";
                cookie.ExpireTimeSpan = TimeSpan.FromHours(168); // one Week
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                        name : "default",
                        pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
