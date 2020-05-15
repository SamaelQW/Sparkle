using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Sparkle.Domain.Data;
using Sparkle.Domain.Services;
using System.Security.Principal;

namespace Sparkle
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
            services.AddMvc().AddRazorOptions(options =>
            {
                options.ViewLocationFormats.Add("/{0}.cshtml");
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddControllersWithViews();
            services.AddHttpContextAccessor();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/Account/Login");
                });

            services.AddTransient<IPrincipal>(provider => provider.GetService<IHttpContextAccessor>().HttpContext.User);

            /// My configuration
            services.Configure<SparkleDatabaseSettings>(
                Configuration.GetSection(nameof(SparkleDatabaseSettings)));

            services.AddTransient<ISparkleDatabaseSettings, SparkleDatabaseSettings>(
                sp => sp.GetRequiredService<IOptions<SparkleDatabaseSettings>>().Value);

            services.AddTransient<PostService>();
            services.AddTransient<UserService>();
            services.AddTransient<LikeService>();
            services.AddTransient<CommentService>();
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
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
