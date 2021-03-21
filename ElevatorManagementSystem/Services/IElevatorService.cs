using ElevatorManagementSystem.Domain.Dto;
using ElevatorManagementSystem.Domain.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElevatorManagementSystem.Services
{
    public interface IElevatorService
    {
        Task<IList<ElevatorLogDto>> GetAllElevatorLogs();

        Task<ElevatorDto> GetElevatorStatus(int id);

        Task CallElevator(CallElevatorDto elevatorViewModel);
    }
}