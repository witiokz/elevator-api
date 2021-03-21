using AutoMapper;
using ElevatorManagementSystem.Data.Repositories;
using ElevatorManagementSystem.Domain.Dto;
using ElevatorManagementSystem.Domain.Enums;
using ElevatorManagementSystem.Domain.ViewModel;
using ElevatorManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ElevatorManagementSystem.Services
{
    public class ElevatorService : IElevatorService
    {
        private const int DoorOperationTimeInMiSec = 2000;
        private const int ElevatorOperationTimeInMiSec = 1000;

        private readonly IMapper _mapper;
        private readonly IElevatorRepository _elevatorRepository;
        private readonly IElevatorLogRepository _elevatorLogRepository;

        public ElevatorService(IMapper mapper, IElevatorRepository elevatorRepository, IElevatorLogRepository elevatorLogRepository)
        {
            _mapper = mapper;
            _elevatorRepository = elevatorRepository;
            _elevatorLogRepository = elevatorLogRepository;
        }

        public async Task<IList<ElevatorLogDto>> GetAllElevatorLogs()
        {
            var dbElevatorLogs = await _elevatorLogRepository.GetAll();
            return _mapper.Map<List<ElevatorLogDto>>(dbElevatorLogs);
        }

        public async Task<ElevatorDto> GetElevatorStatus(int id)
        {
            var elevator = await _elevatorRepository.GetById(id);
            return _mapper.Map<ElevatorDto>(elevator);
        }

        public async Task CallElevator(CallElevatorDto callElevatorDto)
        {
            var nearestElevator = await GetNeariestElevator(callElevatorDto.DestinationFloor);

            if (nearestElevator != null)
            {
                await MoveElevator(nearestElevator, callElevatorDto.DestinationFloor);
            }
        }

        private async Task MoveElevator(Elevator elevator, int destinationFloor)
        {
            var direction = GetDirection(elevator.CurrentFloor, destinationFloor);

            elevator.Direction = direction;
            await UpdateElevatorData(elevator);
            await SaveLog(elevator, ElevatorAction.Open);

            Thread.Sleep(DoorOperationTimeInMiSec);

            await SaveLog(elevator, ElevatorAction.Close);

            var numberOfFloorsToMove = Math.Abs(elevator.CurrentFloor - destinationFloor);

            await Move(elevator, direction, numberOfFloorsToMove);

            elevator.Direction = ElevatorDirection.None;
            await UpdateElevatorData(elevator);
            await SaveLog(elevator, ElevatorAction.Open);

            Thread.Sleep(DoorOperationTimeInMiSec);

            await SaveLog(elevator, ElevatorAction.Close);
        }

        private ElevatorDirection GetDirection(int sourceFloorNumber, int destinationFloolNumber)
        {
            return (sourceFloorNumber < destinationFloolNumber) ? ElevatorDirection.Up : ElevatorDirection.Down;
        }

        private async Task<Elevator> GetNeariestElevator(int destinationFloor)
        {
            return await GetClosestStoppedElevator(destinationFloor) ?? await GetNearestMovingElevator(destinationFloor) ?? await GetNearestStoppedElevator(destinationFloor);
        }

        private async Task<Elevator> GetClosestStoppedElevator(int destinationFloor)
        {
            return await _elevatorRepository.Get(i => i.Direction == ElevatorDirection.None && i.CurrentFloor == destinationFloor);
        }

        private async Task<Elevator> GetNearestMovingElevator(int destinationFloor)
        {
            return await _elevatorRepository.Get(i => (i.Direction == ElevatorDirection.Up && i.CurrentFloor <= destinationFloor)
                                       || (i.Direction == ElevatorDirection.Down && i.CurrentFloor >= destinationFloor));
        }

        private async Task<Elevator> GetNearestStoppedElevator(int destinationFloor)
        {
            var firstUnoccupied = await _elevatorRepository.Get(i => i.Direction == ElevatorDirection.None);

            return (await _elevatorRepository.GetAll()).Aggregate(firstUnoccupied, (closest, elevator) =>
                Math.Abs(elevator.CurrentFloor - destinationFloor) < Math.Abs(closest.CurrentFloor - destinationFloor) ? elevator : closest
            );
        }

        private async Task Move(Elevator elevator, ElevatorDirection elevatorDirection, int numberOfFloorsToMove)
        {
            var floor = elevator.CurrentFloor;

            for (var i = 0; i < numberOfFloorsToMove; i++)
            {
                Thread.Sleep(ElevatorOperationTimeInMiSec);

                floor = elevatorDirection == ElevatorDirection.Up ? floor + 1 : floor - 1;
                elevator.CurrentFloor = floor;
                await SaveLog(elevator, ElevatorAction.Move);
            }
        }

        private async Task UpdateElevatorData(Elevator elevator)
        {
            await _elevatorRepository.Save(elevator);
        }

        private async Task SaveLog(Elevator elevator, ElevatorAction elevatorAction)
        {
            var logItem = new ElevatorLog
            {
                ElevatorId = elevator.Id,
                Action = elevatorAction,
                Updated = DateTime.UtcNow
            };

            await _elevatorLogRepository.Save(logItem);
        }
    }
}
