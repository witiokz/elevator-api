using ElevatorManagementSystem.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;

namespace ElevatorManagementSystem.Data
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new ElevatorManagementSystemContext(
                serviceProvider.GetRequiredService<DbContextOptions<ElevatorManagementSystemContext>>());

            if (context.Elevators.Any())
            {
                return;   // Data was already seeded
            }

            for (var i = 1; i <= GetElevatorCount(); i++)
            {
                context.Elevators.Add(new Elevator
                {
                    Id = i,
                    CurrentFloor = 1

                });
            }

            context.SaveChanges();
        }

        public static int GetElevatorCount()
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                            .AddEnvironmentVariables();

            var configuration = builder.Build();

            int.TryParse(configuration["ElevatorConfiguration:FloorCount"], out int elevatorCount);

            return elevatorCount;
        }
    }
}
