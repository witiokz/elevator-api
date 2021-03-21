using ElevatorManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElevatorManagementSystem.Data.Repositories
{
    public class ElevatorRepository : IElevatorRepository
    {
        private readonly ElevatorManagementSystemContext _context;

        public ElevatorRepository(ElevatorManagementSystemContext context)
        {
            _context = context;
        }

        public async Task<IList<Elevator>> GetAll()
        {
            return await Task.Run(() => _context.Elevators.ToList());
        }

        public async Task<Elevator> GetById(int id)
        {
            return await _context.Elevators.FindAsync(id);
        }

        public async Task<Elevator> Get(Func<Elevator, bool> filter)
        {
            return await Task.Run(() => _context.Elevators.FirstOrDefault(filter)); 
        }

        public async Task Save(Elevator elevator)
        {
            var dbElevator = await _context.Elevators.FindAsync(elevator.Id);

            if (dbElevator != null)
            {
                _context.Elevators.Update(elevator);
                _context.SaveChanges();
            }
        }
    }
}
