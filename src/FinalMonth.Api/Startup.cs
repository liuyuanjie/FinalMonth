using System.Collections.Generic;
using System.Reflection;
using System.Security.Claims;
using FinalMonth.Api.Behaviors;
using FinalMonth.Api.CustomMiddlewares;
using FinalMonth.Api.Filters;
using FinalMonth.Api.Identity.AuthenticationSchemes;
using FinalMonth.Api.Identity.AuthorizationRequirements;
using FinalMonth.Api.NotificationMessage;
using FinalMonth.Api.ServiceExtensions;
using FinalMonth.Api.Utils;
using FinalMonth.Application.Command;
using FinalMonth.Application.Extensions;
using FinalMonth.Application.Interfaces;
using FinalMonth.Application.Repository;
using FinalMonth.Domain;
using FinalMonth.Infrastructure.Dapper;
using FinalMonth.Infrastructure.Data;
using FluentValidation;
using MediatR;
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
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Internal;

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
            services.AddDbContext<FinalMonthIDbContext>(config =>
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
            .AddEntityFrameworkStores<FinalMonthIDbContext>()
            .AddDefaultTokenProviders();

            services.AddControllers(configure =>
            {
                configure.Filters.Add(typeof(CustomTestActionFilter));
            });
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

            services.AddHttpContextAccessor();

            // add identity cookie authentication
            services.ConfigureApplicationCookie(config =>
            {
            });

            // global authorization supports three authentications
            var multiSchemePolicy = new AuthorizationPolicyBuilder(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    JwtBearerDefaults.AuthenticationScheme,
                    ShinetechAuthenticationDefaults.AuthenticationScheme,
                    IdentityConstants.ApplicationScheme)
                .RequireAuthenticatedUser()
                .RequireRole("test", "develop") // the user should have the test and develop roles.
                .Build();

            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = multiSchemePolicy;
                options.AddPolicy("Admin", policy =>
                {
                    policy.AuthenticationSchemes.Add(CookieAuthenticationDefaults.AuthenticationScheme);
                    policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                    policy.AuthenticationSchemes.Add(IdentityConstants.ApplicationScheme);
                    policy.AuthenticationSchemes.Add(ShinetechAuthenticationDefaults.AuthenticationScheme);
                    policy.RequireRole("admin");
                });
                options.AddPolicy("develop", policy => policy.RequireRole("develop"));

                options.AddPolicy("AtLeast21", policy =>
                    policy.Requirements.Add(new MinimumAgeRequirement(21)));
            });

            services.AddAuthentication(ShinetechAuthenticationDefaults.AuthenticationScheme)
                .AddScheme<ShinetechAuthOptions, ShinetechAuthenticationHandler>(ShinetechAuthenticationDefaults.AuthenticationScheme,
                    o =>
                    {
                        o.JwtKey = Configuration.GetValue<string>("Jwt:Key");
                        o.JwtIssuer = Configuration.GetValue<string>("Jwt:Issuer");
                    });

            services.AddValidatorsFromAssembly(typeof(Startup).Assembly);

            services.AddSignalR().AddStackExchangeRedis(Configuration.GetValue<string>("Redis:ConnectionString"), options =>
            {
                options.Configuration.ChannelPrefix = "FinalMonthApp";
                options.Configuration.DefaultDatabase = 5;
            });

            services.AddScoped<INotificationMessageHandler, NotificationMessageHandler>();

            services.AddCap(options =>
            {
                options.UseEntityFramework<FinalMonthIDbContext>();
                options.UseRabbitMQ(options =>
                {
                    Configuration.GetSection("RabbitMQ").Bind(options);
                });
                options.ConsumerThreadCount = 10;
            });

            services.AddScoped<IMSSqlConnection, MSSqlConnection>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddScoped<IFinalMonthIDbContextProvider, FinalMonthIDbContext>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<INotificationMessageQuery, NotificationMessageQuery>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddApplication();

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = Configuration.GetValue<string>("Redis:ConnectionString");
                options.InstanceName = nameof(FinalMonth);
            });
            //services.AddDistributedRedisCache(options =>
            //{
            //    options.Configuration = Configuration.GetValue<string>("Redis:ConnectionString");
            //});

            services.AddMemoryCache(options =>
            {
            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCustomExceptionMiddleware();
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

            app.UseCustomJwtMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<NotificationMessageHub>("/notificationmessagehub");
            });
        }
    }
}
