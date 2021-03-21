using ElevatorManagementSystem.Domain.Dto;
using ElevatorManagementSystem.Domain.ViewModel;
using ElevatorManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElevatorManagementSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ElevatorController : ControllerBase
    {
        private readonly IElevatorService _elevatorService;

        public ElevatorController(IElevatorService elevatorService)
        {
            _elevatorService = elevatorService;
        }

        [HttpGet("{id}/status")]
        public async Task<ElevatorDto> GetElevatorStatus(int id)
        {
            return await _elevatorService.GetElevatorStatus(id);
        }

        [HttpGet("logs")]
        public async Task<IList<ElevatorLogDto>> GetElevatorLogs()
        {
            return await _elevatorService.GetAllElevatorLogs();
        }

        [HttpPost("call")]
        public async Task<IActionResult> CallElevator(CallElevatorDto callElevatorDto)
        {
            await _elevatorService.CallElevator(callElevatorDto);

            return Ok();
        }
    }
}