using MediatR;
using MediatRIdentity.DataAccess;
using MediatRIdentity.DataAccess.Entities;
using MediatRIdentity.Services.Abstract;
using MediatRIdentity.Services.Concrete;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MediatRIdentity {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddHttpContextAccessor();

            services.AddControllersWithViews();
            services.AddMediatR(typeof(Startup));
            services.AddTransient<IAuthService, AuthService>();
            services.AddDbContext<AuthDbContext>(
                opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<User, Role>(config => {
                    config.Password.RequiredLength = 4;
                    config.Password.RequireDigit = false;
                    config.Password.RequireLowercase = false;
                    config.Password.RequireUppercase = false;
                    config.Password.RequireNonAlphanumeric = false;
                }).AddEntityFrameworkStores<AuthDbContext>()
                .AddDefaultTokenProviders();
            services.ConfigureApplicationCookie(opts => {
                opts.LoginPath = "/Auth/SignIn";
                opts.AccessDeniedPath = "/Auth/SignIn";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseExceptionHandler("/Home/Error");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Auth}/{action=SignIn}/{id?}");
            });
        }
    }
}