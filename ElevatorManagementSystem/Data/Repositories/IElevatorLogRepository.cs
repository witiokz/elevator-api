using ElevatorManagementSystem.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElevatorManagementSystem.Data.Repositories
{
    public interface IElevatorLogRepository
    {
        Task<IList<ElevatorLog>> GetAll();

        Task Save(ElevatorLog elevatorLog);
    }
}