using ImpInfApi.Repository;
using ImpInfApi.Utils;
using ImpInfCommon.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace ImpInfApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var appSettings = Configuration.Get<AppSettings>();

            services.AddSingleton(appSettings);

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("DefaultConnection"), ServerVersion.AutoDetect(Configuration.GetConnectionString("DefaultConnection")));
            });

            services.AddControllers();
            services.AddCors();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1.0.1",
                    Title = "ImpInfApi"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {{
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },Array.Empty<string>()
                }});
            });


            //DI
            services.AddTransient<BaseCrudRepository<News>>();
            services.AddTransient<BaseCrudRepository<User>>();
            services.AddTransient<BaseCrudRepository<Day>, DaysRepository>();
            services.AddTransient<BaseCrudRepository<Lesson>>();
            services.AddTransient<BaseCrudRepository<Note>>();
        }

        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppSettings appSettings)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ImpInfApi"));
            app.UseDeveloperExceptionPage();

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials());


            var serviceScopeForPermision = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            app.UseMiddleware<CheckPermisionMidleware>(appSettings, serviceScopeForPermision.ServiceProvider.GetService<BaseCrudRepository<User>>());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            // DB initial
            var dbContext = serviceScope.ServiceProvider.GetService<AppDbContext>();
            dbContext.Database.Migrate();
            var startDbData = UtilsFunctions.GetInitiallQuery();
            if (!string.IsNullOrEmpty(startDbData)) await dbContext.Database.ExecuteSqlRawAsync(UtilsFunctions.GetInitiallQuery());
        }
    }
}
