using ElevatorManagementSystem.Attributes;
using ElevatorManagementSystem.Data;
using ElevatorManagementSystem.Data.Repositories;
using ElevatorManagementSystem.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace ElevatorManagementSystem
{
    public class Startup
    {
        const string Version = "v1";
        const string Name = "Elevator Management System API";

        public static IConfiguration StaticConfig { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            StaticConfig = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(Version, new OpenApiInfo { Title = Name, Version = Version });
            });


            services.AddScoped<IElevatorRepository, ElevatorRepository>();
            services.AddScoped<IElevatorLogRepository, ElevatorLogRepository>();

            services.AddScoped<IElevatorService, ElevatorService>();

            services.AddDbContext<ElevatorManagementSystemContext>(options =>
                   options.UseInMemoryDatabase(databaseName: "ElevatorManagementSystemDb"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{Version}/swagger.json", $"{Name} {Version?.ToUpper()}");

            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
