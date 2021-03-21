using ElevatorManagementSystem.Model;
using Microsoft.EntityFrameworkCore;

namespace ElevatorManagementSystem.Data
{
    public class ElevatorManagementSystemContext : DbContext
    {
        public ElevatorManagementSystemContext(DbContextOptions<ElevatorManagementSystemContext> options)
        : base(options) { }

        public DbSet<Elevator> Elevators { get; set; }
        public DbSet<ElevatorLog> ElevatorLogs { get; set; }
    }
}
