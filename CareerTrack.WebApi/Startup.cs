using CareerTrack.Application.Articles.Commands.Create;
using CareerTrack.Application.Articles.Queries.GetArticle;
using CareerTrack.Application.Articles.Queries.GetArticles;
using CareerTrack.Application.Authorizations;
using CareerTrack.Common;
using CareerTrack.Domain.Entities;
using CareerTrack.Infrastructure;
using CareerTrack.Persistance;
using CareerTrack.Persistance.Repository;
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
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddMediatR(typeof(GetArticlesListQueryHandler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(GetArticleQueryHandler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(CreateArticleCommandHandler).GetTypeInfo().Assembly);

            // Add DbContext using SQL Server Provider
            services.AddDbContext<CareerTrackDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DatabaseConnection")
                , x => x.MigrationsAssembly("CareerTrack.Migrations")
                ));

    

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<CareerTrackDbContext>()
                .AddDefaultTokenProviders();

            services
                .AddMvc(options => options.Filters.Add(typeof(CustomExceptionFilterAttribute)))
                //.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateUserCommandValidator>())
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateArticleCommandValidator>())
                ;

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
                       ValidAudience = _configuration.JwtAudience,
                       ValidIssuer = _configuration.JwtIssuer,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.JwtSecretKey))
                   };
               });

            services.AddSingleton<IAuthorizationHandler, AdminRoleAuthorizationHandler>();
            services.Add(new ServiceDescriptor(typeof(Common.IConfiguration), typeof(Configuration), ServiceLifetime.Singleton));
            services.Add(new ServiceDescriptor(typeof(ILogger), typeof(Logger), ServiceLifetime.Singleton));

            AddAuthentications(services);

            // Customise default API behavour
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            SeedDatabase.Initialize(app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope().ServiceProvider);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
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

                options.AddPolicy("IsStdUser",
                    policy => policy.AddAuthenticationSchemes("Bearer")
                        .RequireAuthenticatedUser()
                        .AddRequirements(new ClaimRequirement("StdUser"))
                        .Build()
                    );
            });
        }
    }
}
