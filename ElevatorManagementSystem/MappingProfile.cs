using AutoMapper;
using ElevatorManagementSystem.Domain.Dto;
using ElevatorManagementSystem.Model;

namespace ElevatorManagementSystem
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ElevatorLog, ElevatorLogDto>();

            CreateMap<Elevator, ElevatorDto>();
        }
    }
}
