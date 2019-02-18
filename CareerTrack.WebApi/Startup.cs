using System;
using System.Reflection;
using System.Text;
using CareerTrack.Application.Articles;
using CareerTrack.Application.Articles.Commands.Delete;
using CareerTrack.Application.Articles.Commands.Update;
using CareerTrack.Application.Articles.Queries.GetArticleDetail;
using CareerTrack.Application.Articles.Queries.GetArticles;
using CareerTrack.Application.Authorizations;
using CareerTrack.Application.Infrastructure;
using CareerTrack.Application.Interfaces;
using CareerTrack.Application.Users.Commands.CreateUser;
using CareerTrack.Application.Users.Commands.DeleteUser;
using CareerTrack.Application.Users.Commands.UpdateCustomer;
using CareerTrack.Application.Users.Queries.GetUserDetail;
using CareerTrack.Application.Users.Queries.GetUsersList;
using CareerTrack.Common;
using CareerTrack.Domain.Entities;
using CareerTrack.Infrastructure;
using CareerTrack.Persistance;
using CareerTrack.WebApi.Filters;
using FluentValidation.AspNetCore;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CareerTrack.WebApi
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
            // Add framework services.
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<IDateTime, MachineDateTime>();
            services.AddTransient<IServiceProvider, ServiceProvider>();

            // Add MediatR
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddMediatR(typeof(GetUsersListQueryHandler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(GetUserDetailQuery).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(CreateUserCommand).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(UpdateUserCommand).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(DeleteUserCommand).GetTypeInfo().Assembly);

            services.AddMediatR(typeof(CreateArticleCommand).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(UpdateArticleCommand).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(DeleteArticleCommand).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(GetArticlesListQueryHandler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(GetArticleDetailQueryHandler).GetTypeInfo().Assembly);

            // Add DbContext using SQL Server Provider
            services.AddDbContext<CareerTrackDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("CareerTrackConnection")
                , x => x.MigrationsAssembly("CareerTrack.Persistance.Migrations")));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<CareerTrackDbContext>()
                .AddDefaultTokenProviders();

            services
                .AddMvc(options => options.Filters.Add(typeof(CustomExceptionFilterAttribute)))
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateUserCommandValidator>());

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
                    ValidAudience = Common.Configuration.Audience,
                    ValidIssuer = Common.Configuration.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Common.Configuration.SecretKey))
                };
            });

            services.AddSingleton<IAuthorizationHandler, AdminRoleAuthorizationHandler>();

            AddAuthentications(services);

            // Customise default API behavour
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            //app.UseSwaggerUi3(settings =>
            //{
            //    settings.Path = "/api";
            //    settings.DocumentPath = "/api/specification.json";
            //});
            SeedDatabase.Initialize(app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope().ServiceProvider);

            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                }
            });
        }

        public void AddAuthentications(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("IsAdmin",
                    policy => policy.AddAuthenticationSchemes("Bearer")
                        .RequireAuthenticatedUser()
                        .AddRequirements(new ClaimRequirement("Admin"))
                        .Build()
                    );
            });
        }
    }
}
