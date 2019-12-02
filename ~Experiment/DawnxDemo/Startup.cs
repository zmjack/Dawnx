using DawnxDemo.Data;
using DawnxTemplate.Authorizations;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DawnxDemo
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
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});

            services.RewriteAuthorizationProvider<CustomAuthorizationPolicyProvider>();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("DefaultConnection")));

            //services.AddIdentity<IdentityUser, IdentityRole>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>()
            //    .AddDefaultTokenProviders();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/WechatAccount/SignIn";
                    options.AccessDeniedPath = "/WechatAccount/Denied";
                })
                .AddCookie("Internal", options =>
                {
                    options.LoginPath = "/InternalAccount/SignIn";
                    options.AccessDeniedPath = "/InternalAccount/Denied";
                })
                .AddCookie("scheme1", options =>
                {
                    options.LoginPath = "/Home/SignInWechatHybrid";
                    options.AccessDeniedPath = "/InternalAccount/Denied";
                    options.ExpireTimeSpan = TimeSpan.FromSeconds(30);
                })
                .AddCookie("scheme2", options =>
                {
                    options.LoginPath = "/InternalAccount/SignIn";
                    options.AccessDeniedPath = "/InternalAccount/Denied";
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseErrorLogging<ErrorLoggingFileMiddleware>();

            app.UsePathBase(new PathString(Configuration["ASPNETCORE_APPL_PATH"]));
            app.UseStaticFiles();
            //app.UseHttpsRedirection();
            //app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "areas",
                  template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
                routes.MapRoute(
                  name: "default",
                  template: "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}
