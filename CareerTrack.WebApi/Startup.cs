using CareerTrack.Application.Articles.Queries.GetArticles;
using CareerTrack.Domain.Entities;
using CareerTrack.Infrastructure;
using CareerTrack.Persistance;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

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
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            services.AddMediatR(typeof(GetArticlesListQueryHandler).GetTypeInfo().Assembly);

            // Add DbContext using SQL Server Provider
            services.AddDbContext<CareerTrackDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DatabaseConnection")
                , x => x.MigrationsAssembly("CareerTrack.Migrations")
                ));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<CareerTrackDbContext>()
                .AddDefaultTokenProviders();

            //services
            //    .AddMvc(options => options.Filters.Add(typeof(CustomExceptionFilterAttribute)))
            //    .SetCompatibilityVersion(CompatibilityVersion.Ver)
            //    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateUserCommandValidator>());

            var _configuration = new Configuration();

            //add JWT here

            services.Add(new ServiceDescriptor(typeof(Common.IConfiguration), typeof(Configuration), ServiceLifetime.Singleton));
            services.AddControllers();
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
    }
}
