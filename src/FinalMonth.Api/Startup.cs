using FinalMonth.Api.Common;
using FinalMonth.Api.ServiceExtensions;
using FinalMonth.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Identity;

namespace FinalMonth.Api
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
            services.AddDbContext<FinalMonthDataContext>(config =>
            {
                config.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), options =>
                 {
                 });
            });
            services.AddIdentity<ShinetechUser, IdentityRole>(setup =>
            {
                setup.Password.RequireDigit = false;
                setup.Password.RequireLowercase = false;
                setup.Password.RequireNonAlphanumeric = false;
                setup.Password.RequiredLength = 4;
            })
            .AddEntityFrameworkStores<FinalMonthDataContext>()
            .AddDefaultTokenProviders();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FinalMonth.Api", Version = "v1" });
            });

            services.AddSingleton<AppSettings>(new AppSettings(Configuration));

            //add jwt authentication
            services.AddAuthenticationJwtSetup(Configuration);

            // add cookie authentication
            services.AddAuthentication().AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, configure =>

            {
                configure.Cookie.Name = "shinetech";
            });

            // add identity cookie authentication
            services.ConfigureApplicationCookie(config =>
            {
            });

            var multiSchemePolicy = new AuthorizationPolicyBuilder(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    JwtBearerDefaults.AuthenticationScheme,
                    IdentityConstants.ApplicationScheme)
                .RequireAuthenticatedUser()
                .Build();

            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = multiSchemePolicy;
                options.AddPolicy("Admin", policy => policy.RequireRole("admin"));
                options.AddPolicy("Technology", policy => policy.RequireRole("technology"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FinalMonth.Api v1"));
            }

            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowAnyOrigin();
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            // who are you
            app.UseAuthentication();

            // what are you
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
