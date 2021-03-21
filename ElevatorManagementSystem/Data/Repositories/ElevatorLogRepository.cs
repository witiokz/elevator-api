using ElevatorManagementSystem.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElevatorManagementSystem.Data.Repositories
{
    public class ElevatorLogRepository : IElevatorLogRepository
    {
        private readonly ElevatorManagementSystemContext _context;

        public ElevatorLogRepository(ElevatorManagementSystemContext context)
        {
            _context = context;
        }

        public async Task<IList<ElevatorLog>> GetAll()
        {
            return await Task.Run(() => _context.ElevatorLogs.ToList());
        }

        public async Task Save(ElevatorLog elevatorLog)
        {
            _context.ElevatorLogs.Add(elevatorLog);
            await _context.SaveChangesAsync();
        }
    }
}
