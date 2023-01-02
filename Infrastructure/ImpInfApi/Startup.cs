using ImpInfApi.Hubs;
using ImpInfApi.Models;
using ImpInfApi.Repository;
using ImpInfApi.Services;
using ImpInfApi.Utils;
using ImpInfCommon.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using ImpInfApi.Middlewares;

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
            }, contextLifetime: ServiceLifetime.Transient);

            services.AddControllers();
            services.AddCors();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1.0.1",
                    Title = "ImpInfApi"
                });

                //options.AddSignalRSwaggerGen(ssgOptions => ssgOptions.ScanAssemblies(typeof(ImpInfCommon.Interfaces.INotificationsService).Assembly));
                options.AddSignalRSwaggerGen();
            });

            services.AddSignalR();

            //DI
            services.AddTransient<NewsRepository>();
            services.AddTransient<BaseCrudRepository<User>>();
            services.AddTransient<BaseCrudRepository<Day>, DaysRepository>();
            services.AddTransient<BaseCrudRepository<Lesson>>();
            services.AddTransient<BaseCrudRepository<Note>>();
            services.AddTransient<BaseCrudRepository<LessonsAndTimes>>();

            services.AddSingleton<NotificationsService>();

            RegistratePaths(services);
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


            var serviceScopeForPermission = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            app.UseMiddleware<CheckPermisionMiddleware>(appSettings, serviceScopeForPermission.ServiceProvider.GetService<BaseCrudRepository<User>>());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<NotificationsHub>("/api/hubs/NotificationsService");
            });

            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            // DB initial
            var dbContext = serviceScope.ServiceProvider.GetService<AppDbContext>();
            dbContext.Database.Migrate();
            var startDbData = UtilsFunctions.GetInitiallQuery();
            if (!string.IsNullOrEmpty(startDbData)) await dbContext.Database.ExecuteSqlRawAsync(UtilsFunctions.GetInitiallQuery());
        }

        private void RegistratePaths(IServiceCollection sc)
        {
            List<AvailablePath> availablePaths = new()
            {
                new AvailablePath("/api/Account", HttpMethod.Post),
                new AvailablePath("/api/Account/CheckToken/...", HttpMethod.Get)
            };

            sc.AddSingleton(availablePaths);
        }
    }
}
