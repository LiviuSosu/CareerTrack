using CareerTrack.Application.Authorizations;
using CareerTrack.Application.Handlers;
using CareerTrack.Common;
using CareerTrack.Domain.Entities;
using CareerTrack.Infrastructure;
using CareerTrack.Persistance;
using CareerTrack.Persistance.Repository;
using CareerTrack.Services.SendGrid;
using CareerTrack.Services.TokenManager;
using CareerTrack.WebApi.Filters;
using CareerTrack.WebApi.HealthChecks;
using FluentValidation.AspNetCore;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Reflection;
using System.Text;

namespace CareerTrack.WebApi
{
    public class Startup
    {
        public Startup(Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public Microsoft.Extensions.Configuration.IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddTransient<ITokenManager, TokenManager>();
            services.AddTransient<ITokenManager, TokenManager>();
            // services.AddTransient<TokenManagerMiddleware>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            // services.AddDistributedRedisCache(r => { r.Configuration = "localhost"; });

            services.AddMediatR(typeof(BaseHandler<,>).GetTypeInfo().Assembly);

            services.AddDbContext<CareerTrackDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DatabaseConnection")
                , migrations => migrations.MigrationsAssembly("CareerTrack.Migrations")));

            services.AddIdentityCore<User>()
        .AddEntityFrameworkStores<CareerTrackDbContext>();

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<CareerTrackDbContext>()
                .AddDefaultTokenProviders();

            services
                .AddMvc(options => options.Filters.Add(typeof(CustomExceptionFilterAttribute)))
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<BaseValidator<object>>());

            var _configuration = new Configuration();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
               .AddJwtBearer(options =>
               {
                   options.SaveToken = true;
                   options.RequireHttpsMetadata = false;
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidAudience = _configuration.JWTConfiguration.JwtAudience,
                       ValidIssuer = _configuration.JWTConfiguration.JwtIssuer,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.JWTConfiguration.JwtSecretKey)),
                       ClockSkew = TimeSpan.Zero
                   };
               });

            services.AddSingleton<IAuthorizationHandler, AdminRoleAuthorizationHandler>();
            services.Add(new ServiceDescriptor(typeof(Common.IConfiguration), typeof(Configuration), ServiceLifetime.Singleton));
            services.Add(new ServiceDescriptor(typeof(ILogger), typeof(Logger), ServiceLifetime.Singleton));
            services.AddTransient<IEmailSender>(service => new EmailSender(""));

            AddAuthentications(services);

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.Configure<AuthMessageSenderOptions>(Configuration);

            services.AddHealthChecks()
           .AddCheck(
               "SqlDb-check",
               new SqlConnectionHealthCheck(Configuration.GetConnectionString("DatabaseConnection")),
               HealthStatus.Unhealthy,
               new string[] { "orderingdb" });

            services.AddHealthChecks()
        .AddCheck(
            "RedisDB-check",
            new RedisHealthCheck("localhost"),
            HealthStatus.Unhealthy,
             new string[] { "redis test" });

            services.Configure<DataProtectionTokenProviderOptions>(o => o.TokenLifespan = TimeSpan.FromHours(3));

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3000")
                                .WithMethods("PUT", "DELETE", "GET", "POST", "OPTIONS")
                                .WithHeaders("Content-Type", "Authorization");
                    });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            SeedDatabase.Initialize(app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope().ServiceProvider);

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/hc");
            });

            //app.UseMiddleware<TokenManagerMiddleware>();
        }

        public void AddAuthentications(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("IsAdmin",
                    policy => policy.AddAuthenticationSchemes("Bearer")
                        .RequireAuthenticatedUser()
                         .RequireRole("Admin")
                        .Build()
                    );

                options.AddPolicy("IsStdUser",
                    policy => policy.AddAuthenticationSchemes("Bearer")
                        .RequireAuthenticatedUser()
                        .RequireRole("Admin", "StandardUser")
                        .Build()
                    );
            });
        }
    }
}
