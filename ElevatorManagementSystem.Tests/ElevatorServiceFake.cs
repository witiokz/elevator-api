using ElevatorManagementSystem.Domain.Dto;
using ElevatorManagementSystem.Domain.ViewModel;
using ElevatorManagementSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElevatorManagementSystem.Tests
{
    public class ElevatorServiceFake : IElevatorService
    {
        private readonly IList<ElevatorDto> _elevators;
        private readonly IList<ElevatorLogDto> _elevatorsLogs;
        public ElevatorServiceFake()
        {
            _elevators = new List<ElevatorDto>()
            {
                new ElevatorDto{ Id = 1, CurrentFloor = 1 },
                new ElevatorDto{ Id = 2, CurrentFloor = 10, },
                new ElevatorDto{ Id = 3, CurrentFloor = 1 },
            };

            _elevatorsLogs = new List<ElevatorLogDto>()
            {
                new ElevatorLogDto{  Action = Domain.Enums.ElevatorAction.Close },
                new ElevatorLogDto{  Action = Domain.Enums.ElevatorAction.Move },
                new ElevatorLogDto{  Action = Domain.Enums.ElevatorAction.Open },
            };
        }

        public Task CallElevator(CallElevatorDto elevatorViewModel)
        {
            throw new NotImplementedException();
        }

        public Task<IList<ElevatorLogDto>> GetAllElevatorLogs()
        {
            return Task.FromResult(_elevatorsLogs);
        }

        public Task<ElevatorDto> GetElevatorStatus(int id)
        {
            return Task.FromResult(_elevators.FirstOrDefault(i => i.Id == 1));
        }
    }
}
